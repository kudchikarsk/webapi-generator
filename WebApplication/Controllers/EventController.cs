using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class EventController : ApiController
    {
        protected readonly IRepository<Event> repository;
        protected readonly Mapper mapper;
        private string includes;
        public EventController(IRepository<Event> repository, Mapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<IHttpActionResult> Get(int? page = null, int pageSize = 10, string orderBy = "Id", bool ascending = false, string query = null)
        {
            var likeExpression = string.Format("%{0}%", query);
            Expression<Func<Event, bool>> filter = e => (query == null || query == "null" || DbFunctions.Like(e.Name, likeExpression));
            var result = await repository.CreatePagedResults(filter, page.Value, pageSize, orderBy, ascending, query);
            var mod = result.TotalNumberOfRecords % pageSize;
            var totalPageCount = (result.TotalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);
            var nextPageUrl = page == totalPageCount ? null : Url?.Link("DefaultApi", new
            {
            page = page + 1, pageSize, orderBy, ascending, query
            }

            );
            var pagedResult = new PagedResults<CompactEvent>{Results = mapper.Map<List<CompactEvent>>(result.Results), PageNumber = page.Value, PageSize = pageSize, ResultCount = result.Results.Count, TotalNumberOfPages = totalPageCount, TotalNumberOfRecords = result.TotalNumberOfRecords, NextPageUrl = nextPageUrl};
            return Ok(pagedResult);
        }

        [HttpGet]
        public virtual IHttpActionResult GetSuggestions(string searchTerm)
        {
            var likeExpression = string.Format("%{0}%", searchTerm);
            Expression<Func<Event, bool>> searchExpression = e => (searchTerm == null || searchTerm == "null" || DbFunctions.Like(e.Name, likeExpression));
            var employees = repository.Get(searchExpression).Take(20).ToList();
            var employeesVM = mapper.Map<List<CompactEvent>>(employees);
            return Ok<IEnumerable<CompactEvent>>(employeesVM);
        }

        public virtual IHttpActionResult Get(long id)
        {
            var model = repository.Get(e => e.Id == id, includeProperties: includes).SingleOrDefault();
            var dto = mapper.Map<GetEvent>(model);
            return Ok<GetEvent>(dto);
        }

        public virtual IHttpActionResult Post([FromBody] CreateEvent value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event model;
            try
            {
                model = Event.Create(value, User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            repository.Insert(model);
            var dto = mapper.Map<GetEvent>(model);
            return Created(nameof(Event) + "/" + dto.Id, dto);
        }

        public virtual IHttpActionResult Put(long id, [FromBody] UpdateEvent value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Event model = repository.GetByID(id);
            try
            {
                model.Update(value, User.Identity.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            repository.Update(model);
            return Content(HttpStatusCode.NoContent, "");
        }

        public virtual IHttpActionResult Delete(long id)
        {
            var model = repository.GetByID(id);
            if (model == null)
                return NotFound();
            repository.Delete(model);
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
