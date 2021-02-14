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
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            EFGenericRepository<CompanyJobPoco> repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }
        [HttpGet]
        [Route("job/{companyJobId}")]
        [ResponseType(typeof(CompanyJobPoco))]
        public ActionResult GetCompanyJob(Guid companyJobId)
        {
            CompanyJobPoco poco = _logic.Get(companyJobId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("job")]
        [ResponseType(typeof(List<CompanyJobPoco>))]
        public ActionResult GetAllCompanyJob()
        {
            List<CompanyJobPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
        [Route("job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
        [Route("job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
