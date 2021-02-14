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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleController()
        {
            EFGenericRepository<SecurityLoginsRolePoco> repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }
        [HttpGet]
        [Route("loginrole/{roleId}")]
        [ResponseType(typeof(SecurityLoginsRolePoco))]
        public ActionResult GetSecurityLoginsRole(Guid roleId)
        {
            SecurityLoginsRolePoco poco = _logic.Get(roleId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("loginrole")]
        [ResponseType(typeof(List<SecurityLoginsRolePoco>))]
        public ActionResult GetAllSecurityLoginsRole()
        {
            List<SecurityLoginsRolePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("loginrole")]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
        [Route("loginrole")]
        public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
        [Route("loginrole")]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
