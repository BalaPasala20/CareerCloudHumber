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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleController()
        {
            EFGenericRepository<SecurityRolePoco> repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }
        [HttpGet]
        [Route("role/{roleId}")]
        [ResponseType(typeof(SecurityRolePoco))]
        public ActionResult GetSecurityRole(Guid roleId)
        {
            SecurityRolePoco poco = _logic.Get(roleId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }
        [HttpGet]
        [Route("role")]
        [ResponseType(typeof(List<SecurityRolePoco>))]
        public ActionResult GetAllSecurityRole()
        {
            List<SecurityRolePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                return NotFound();
            }
            return Ok(pocos);
        }
        [HttpPost]
        [Route("role")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
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
        [Route("role")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
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
        [Route("role")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}
