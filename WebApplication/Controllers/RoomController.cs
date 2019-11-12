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
    public class RoomController : ApiController
    {
        protected readonly IRepository<Room> repository;
        protected readonly Mapper mapper;
        private string includes;
        public RoomController(IRepository<Room> repository, Mapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<IHttpActionResult> Get(int? page = null, int pageSize = 10, string orderBy = "Id", bool ascending = false, string query = null)
        {
            var likeExpression = string.Format("%{0}%", query);
            Expression<Func<Room, bool>> filter = e => query == null || query == "null";
            var result = await repository.CreatePagedResults(filter, page.Value, pageSize, orderBy, ascending, query);
            var mod = result.TotalNumberOfRecords % pageSize;
            var totalPageCount = (result.TotalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);
            var nextPageUrl = page == totalPageCount ? null : Url?.Link("DefaultApi", new
            {
            page = page + 1, pageSize, orderBy, ascending, query
            }

            );
            var pagedResult = new PagedResults<CompactRoom>{Results = mapper.Map<List<CompactRoom>>(result.Results), PageNumber = page.Value, PageSize = pageSize, ResultCount = result.Results.Count, TotalNumberOfPages = totalPageCount, TotalNumberOfRecords = result.TotalNumberOfRecords, NextPageUrl = nextPageUrl};
            return Ok(pagedResult);
        }

        [HttpGet]
        public virtual IHttpActionResult GetSuggestions(string searchTerm)
        {
            var likeExpression = string.Format("%{0}%", searchTerm);
            Expression<Func<Room, bool>> searchExpression = e => searchTerm == null || searchTerm == "null";
            var employees = repository.Get(searchExpression).Take(20).ToList();
            var employeesVM = mapper.Map<List<CompactRoom>>(employees);
            return Ok<IEnumerable<CompactRoom>>(employeesVM);
        }

        public virtual IHttpActionResult Get(long id)
        {
            var model = repository.Get(e => e.Id == id, includeProperties: includes).SingleOrDefault();
            var dto = mapper.Map<GetRoom>(model);
            return Ok<GetRoom>(dto);
        }

        public virtual IHttpActionResult Post([FromBody] CreateRoom value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room model;
            try
            {
                model = Room.Create(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            repository.Insert(model);
            var dto = mapper.Map<GetRoom>(model);
            return Created(nameof(Room) + "/" + dto.Id, dto);
        }

        public virtual IHttpActionResult Put(long id, [FromBody] UpdateRoom value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room model = repository.GetByID(id);
            try
            {
                model.Update(value);
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
