using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repo) : base(repo)
        {

        }
        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
           foreach(ApplicantJobApplicationPoco poco in pocos)
            {
                if(poco.ApplicationDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(110, $"{poco.Id} - Application date cannot be greater than today"));
                }
                if(exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
            
        }
    }
}
