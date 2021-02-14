using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.ADODataAccessLayer;
using System.Web.Http.Description;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            EFGenericRepository<CompanyLocationPoco> repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }
        [HttpGet]
        [Route("location/{locationId}")]
        [ResponseType(typeof(CompanyLocationPoco))]
        public ActionResult GetCompanyLocation(Guid locationId)
        {
            CompanyLocationPoco poco = _logic.Get(locationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("location")]
        [ResponseType(typeof(List<CompanyLocationPoco>))]
        public ActionResult GetAllCompanyLocation()
        {
            List<CompanyLocationPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            try
            {
                _logic.Add(pocos);
                return Ok();
            }
            catch (AggregateException ae)
            {
                return BadRequest(ae);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut]
        [Route("location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            try
            {
                _logic.Update(pocos);
                return Ok();
            }
            catch (AggregateException ae)
            {
                return BadRequest(ae);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        [Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
