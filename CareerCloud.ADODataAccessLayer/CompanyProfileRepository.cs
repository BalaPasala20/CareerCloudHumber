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
    public class CompanyProfileRepository : BaseAdo, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id]
                                       ,[Registration_Date]
                                       ,[Company_Website]
                                       ,[Contact_Phone]
                                       ,[Contact_Name]
                                       ,[Company_Logo])
                                 VALUES
                                       (@Id
                                       ,@RegistrationDate
                                       ,@CompanyWebsite
                                       ,@ContactPhone
                                       ,@ContactName
                                       ,@CompanyLogo)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", item.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT *
                                   FROM [dbo].[Company_Profiles]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[1000];
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = rdr.IsDBNull(2) ? (string)null : rdr.GetString(2); 
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = rdr.IsDBNull(4) ? (string)null : rdr.GetString(4); 
                    poco.CompanyLogo = rdr.IsDBNull(5) ? (byte[])null :(byte[]) rdr[5]; ;
                    poco.TimeStamp = (byte[])rdr[6];
                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();

                return pocos.Where(p => p != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                        WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                        SET [Registration_Date] = @RegistrationDate
                                            ,[Company_Website] = @CompanyWebsite
                                            ,[Contact_Phone] = @ContactPhone
                                            ,[Contact_Name] = @ContactName
                                            ,[Company_Logo] = @CompanyLogo
                                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", item.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
