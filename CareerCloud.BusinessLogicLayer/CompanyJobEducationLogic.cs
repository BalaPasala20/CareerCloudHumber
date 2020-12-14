using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repo) : base(repo)
        {

        }
        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyJobEducationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major) || poco.Major.Length<=2)
                {
                    exceptions.Add(new ValidationException(200, $"{poco.Id} - Major must be atleast 2 characters"));
                }
                if(poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, $"{poco.Id} - Importance cannot be less than 0"));
                }
                if(exceptions.Count >0)
                {
                    throw new AggregateException(exceptions);
                }

            }
        }
    }
}
