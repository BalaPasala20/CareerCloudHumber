using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository : BaseAdo, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                       ([Id]
                                       ,[Login]
                                       ,[Current_Salary]
                                       ,[Current_Rate]
                                       ,[Currency]
                                       ,[Country_Code]
                                       ,[State_Province_Code]
                                       ,[Street_Address]
                                       ,[City_Town]
                                       ,[Zip_Postal_Code])
                                       VALUES
                                       (@Id
                                       ,@Login
                                       ,@CurrentSalary
                                       ,@CurrentRate
                                       ,@Currency
                                       ,@CountryCode
                                       ,@StateProvinceCode
                                       ,@StreetAddress
                                       ,@CityTown
                                       ,@ZipPostalCode)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@CountryCode", item.Country);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", item.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", item.Street);
                    cmd.Parameters.AddWithValue("@CityTown", item.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", item.PostalCode);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT *
                                   FROM [dbo].[Applicant_Job_Applications]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
                while (rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.CurrentSalary = (decimal?)rdr.GetSqlValue(2);
                    poco.CurrentRate = (decimal?)rdr[3];
                    poco.Currency = rdr.GetString(4);
                    poco.Country = rdr.GetString(5);
                    poco.Province = rdr.GetString(6);
                    poco.Street = rdr.GetString(7);
                    poco.City = rdr.GetString(8);
                    poco.PostalCode = rdr.GetString(9);
                    poco.TimeStamp = (byte[])rdr[10];
                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();

                return pocos.Where(p => p != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                        WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                       SET [Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Job] = @Job
                                          ,[Application_Date] = @ApplicationDate
                                       WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
