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
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            EFGenericRepository<CompanyProfilePoco> repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }
        [HttpGet]
        [Route("location/{locationId}")]
        [ResponseType(typeof(CompanyProfilePoco))]
        public ActionResult GetCompanyProfile(Guid locationId)
        {
            CompanyProfilePoco poco = _logic.Get(locationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("location")]
        [ResponseType(typeof(List<CompanyProfilePoco>))]
        public ActionResult GetAllCompanyProfile()
        {
            List<CompanyProfilePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("location")]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
