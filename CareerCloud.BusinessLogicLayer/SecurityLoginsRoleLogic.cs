using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginsRoleLogic : BaseLogic<SecurityLoginsRolePoco>
    {
        public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> repo) : base(repo)
        {

        }
        //public override void Add(SecurityLoginsRolePoco[] pocos)
        //{
        //    base.Add(pocos);
        //}
        //public override void Update(SecurityLoginsRolePoco[] pocos)
        //{
        //    base.Update(pocos);
        //}
    }
}
