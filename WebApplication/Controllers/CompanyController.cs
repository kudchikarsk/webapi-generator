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
    public class CompanyController : ApiController
    {
        protected readonly IRepository<Company> repository;
        protected readonly Mapper mapper;
        private string includes;
        public CompanyController(IRepository<Company> repository, Mapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<IHttpActionResult> Get(int? page = null, int pageSize = 10, string orderBy = "Id", bool ascending = false, string query = null)
        {
            var likeExpression = string.Format("%{0}%", query);
            Expression<Func<Company, bool>> filter = e => query == null || query == "null";
            var result = await repository.CreatePagedResults(filter, page.Value, pageSize, orderBy, ascending, query);
            var mod = result.TotalNumberOfRecords % pageSize;
            var totalPageCount = (result.TotalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);
            var nextPageUrl = page == totalPageCount ? null : Url?.Link("DefaultApi", new
            {
            page = page + 1, pageSize, orderBy, ascending, query
            }

            );
            var pagedResult = new PagedResults<CompactCompany>{Results = mapper.Map<List<CompactCompany>>(result.Results), PageNumber = page.Value, PageSize = pageSize, ResultCount = result.Results.Count, TotalNumberOfPages = totalPageCount, TotalNumberOfRecords = result.TotalNumberOfRecords, NextPageUrl = nextPageUrl};
            return Ok(pagedResult);
        }

        [HttpGet]
        public virtual IHttpActionResult GetSuggestions(string searchTerm)
        {
            var likeExpression = string.Format("%{0}%", searchTerm);
            Expression<Func<Company, bool>> searchExpression = e => searchTerm == null || searchTerm == "null";
            var employees = repository.Get(searchExpression).Take(20).ToList();
            var employeesVM = mapper.Map<List<CompactCompany>>(employees);
            return Ok<IEnumerable<CompactCompany>>(employeesVM);
        }

        public virtual IHttpActionResult Get(long id)
        {
            var model = repository.Get(e => e.Id == id, includeProperties: includes).SingleOrDefault();
            var dto = mapper.Map<GetCompany>(model);
            return Ok<GetCompany>(dto);
        }

        public virtual IHttpActionResult Post([FromBody] CreateCompany value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company model;
            try
            {
                model = Company.Create(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            repository.Insert(model);
            var dto = mapper.Map<GetCompany>(model);
            return Created(nameof(Company) + "/" + dto.Id, dto);
        }

        public virtual IHttpActionResult Put(long id, [FromBody] UpdateCompany value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company model = repository.GetByID(id);
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
