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
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            EFGenericRepository<SecurityLoginPoco> repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }
        [HttpGet]
        [Route("login/{loginId}")]
        [ResponseType(typeof(SecurityLoginPoco))]
        public ActionResult GetSecurityLogin(Guid loginId)
        {
            SecurityLoginPoco poco = _logic.Get(loginId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("login")]
        [ResponseType(typeof(List<SecurityLoginPoco>))]
        public ActionResult GetAllSecurityLogin()
        {
            List<SecurityLoginPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("login")]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
        [Route("login")]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
        [Route("login")]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
