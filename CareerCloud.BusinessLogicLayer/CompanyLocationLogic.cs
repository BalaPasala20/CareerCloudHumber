using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repo) : base(repo)
        {

        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, $"{poco.Id} - CountryCode cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501, $"{poco.Id} - Province cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502, $"{poco.Id} - Street cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503, $"{poco.Id} - City cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, $"{poco.Id} - PostalCode cannot be empty"));
                }
                if(exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
