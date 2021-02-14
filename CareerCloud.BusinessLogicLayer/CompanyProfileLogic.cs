using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repo) : base(repo)
        {

        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                //if(string.IsNullOrEmpty(poco.CompanyWebsite) && !poco.CompanyWebsite.EndsWith(".ca") || !poco.CompanyWebsite.EndsWith(".com") || !poco.CompanyWebsite.EndsWith(".biz"))
                //{
                //    exceptions.Add(new ValidationException(600, $"{poco.Id} - Invalid website"));
                //}
                if(!string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    if (poco.CompanyWebsite.EndsWith(".ca") || poco.CompanyWebsite.EndsWith(".com") || poco.CompanyWebsite.EndsWith(".biz"))
                    {
                    }
                    else
                    {
                        exceptions.Add(new ValidationException(600, $"{poco.Id} - Invalid website"));
                    }
                }
                else
                {
                    exceptions.Add(new ValidationException(600, $"{poco.Id} - Invalid website"));
                }
                //if(string.IsNullOrEmpty(poco.ContactPhone) || PhoneNumber.IsPhoneNbr(poco.ContactPhone))
                //{
                //    exceptions.Add(new ValidationException(601, $"{poco.Id} - Invalid phone number"));
                //}
                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber for CompanyProfile {poco.Id} is required"));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber for CompanyProfile  {poco.Id} is not in the required format."));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for CompanyProfile  {poco.Id} is not in the required format."));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for CompanyProfile  {poco.Id} is not in the required format."));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for CompanyProfile  {poco.Id} is not in the required format."));
                        }
                    }
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
    //public static class PhoneNumber
    //{
    //    public const string phoneregex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

    //    public static bool IsPhoneNbr(string number)
    //    {
    //        return Regex.IsMatch(number, phoneregex);
    //    }
    //}
}
