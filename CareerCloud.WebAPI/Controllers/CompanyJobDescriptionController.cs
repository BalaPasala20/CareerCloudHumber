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
    public class CompanyJobDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionController()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }
        [HttpGet]
        [Route("description/{companyJobId}")]
        [ResponseType(typeof(CompanyJobDescriptionPoco))]
        public ActionResult GetCompanyJobDescription(Guid companyJobId)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(companyJobId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("description")]
        [ResponseType(typeof(List<CompanyJobDescriptionPoco>))]
        public ActionResult GetAllCompanyJobDescription()
        {
            List<CompanyJobDescriptionPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("description")]
        public ActionResult PostCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
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
        public ActionResult PutCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
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
        public ActionResult DeleteCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
