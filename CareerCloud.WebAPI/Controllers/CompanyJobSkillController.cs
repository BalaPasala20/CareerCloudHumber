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
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            EFGenericRepository<CompanyJobSkillPoco> repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }
        [HttpGet]
        [Route("jobskill/{skillId}")]
        [ResponseType(typeof(CompanyJobSkillPoco))]
        public ActionResult GetCompanyJobSkill(Guid skillId)
        {
            CompanyJobSkillPoco poco = _logic.Get(skillId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("jobskill")]
        [ResponseType(typeof(List<CompanyJobSkillPoco>))]
        public ActionResult GetAllCompanyJobSkill()
        {
            List<CompanyJobSkillPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("jobskill")]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
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
        [Route("jobskill")]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
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
        [Route("jobskill")]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
