using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
    {
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repo) : base (repo)
        {
                    
        }
        public override void Add(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyJobSkillPoco poco in pocos)
            {
                if(poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(400, $"{poco.Id} - Importance cannot be less than 0"));
                }
                if(exceptions.Count >0)
                {
                    throw new AggregateException(exceptions);
                }

            }
        }
    }
}
