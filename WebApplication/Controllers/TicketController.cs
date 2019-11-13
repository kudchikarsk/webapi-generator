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
    public class TicketController : ApiController
    {
        protected readonly IRepository<Ticket> repository;
        protected readonly Mapper mapper;
        private string includes;
        public TicketController(IRepository<Ticket> repository, Mapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<IHttpActionResult> Get(int? page = null, int pageSize = 10, string orderBy = "Id", bool ascending = false, string query = null)
        {
            var likeExpression = string.Format("%{0}%", query);
            Expression<Func<Ticket, bool>> filter = e => query == null || query == "null";
            var result = await repository.CreatePagedResults(filter, page.Value, pageSize, orderBy, ascending, query);
            var mod = result.TotalNumberOfRecords % pageSize;
            var totalPageCount = (result.TotalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);
            var nextPageUrl = page == totalPageCount ? null : Url?.Link("DefaultApi", new
            {
            page = page + 1, pageSize, orderBy, ascending, query
            }

            );
            var pagedResult = new PagedResults<CompactTicket>{Results = mapper.Map<List<CompactTicket>>(result.Results), PageNumber = page.Value, PageSize = pageSize, ResultCount = result.Results.Count, TotalNumberOfPages = totalPageCount, TotalNumberOfRecords = result.TotalNumberOfRecords, NextPageUrl = nextPageUrl};
            return Ok(pagedResult);
        }

        [HttpGet]
        public virtual IHttpActionResult GetSuggestions(string searchTerm)
        {
            var likeExpression = string.Format("%{0}%", searchTerm);
            Expression<Func<Ticket, bool>> searchExpression = e => searchTerm == null || searchTerm == "null";
            var employees = repository.Get(searchExpression).Take(20).ToList();
            var employeesVM = mapper.Map<List<CompactTicket>>(employees);
            return Ok<IEnumerable<CompactTicket>>(employeesVM);
        }

        public virtual IHttpActionResult Get(long id)
        {
            var model = repository.Get(e => e.Id == id, includeProperties: includes).SingleOrDefault();
            var dto = mapper.Map<GetTicket>(model);
            return Ok<GetTicket>(dto);
        }

        public virtual IHttpActionResult Post([FromBody] CreateTicket value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ticket model;
            try
            {
                model = Ticket.Create(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            repository.Insert(model);
            var dto = mapper.Map<GetTicket>(model);
            return Created(nameof(Ticket) + "/" + dto.Id, dto);
        }

        public virtual IHttpActionResult Put(long id, [FromBody] UpdateTicket value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ticket model = repository.GetByID(id);
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
