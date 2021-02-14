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
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionController()
        {
            EFGenericRepository<CompanyDescriptionPoco> repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }
        [HttpGet]
        [Route("description/{companyDescriptionId}")]
        [ResponseType(typeof(CompanyDescriptionPoco))]
        public ActionResult GetCompanyDescription(Guid companyDescriptionId)
        {
            CompanyDescriptionPoco poco = _logic.Get(companyDescriptionId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("description")]
        [ResponseType(typeof(List<CompanyDescriptionPoco>))]
        public ActionResult GetAllCompanyDescription()
        {
            List<CompanyDescriptionPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("description")]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
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
        [Route("description")]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
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
        [Route("description")]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
