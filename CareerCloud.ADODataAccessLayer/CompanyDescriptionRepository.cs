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
    public class CompanyDescriptionRepository : BaseAdo, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                        ([Id]
                                        ,[Company]
                                        ,[LanguageID]
                                        ,[Company_Name]
                                        ,[Company_Description])
                                        VALUES
                                        (@Id
                                        ,@Company
                                        ,@LanguageID
                                        ,@Company_Name
                                        ,@Company_Description)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT *
                                   FROM [dbo].[Company_Descriptions]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
                while (rdr.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.LanguageId = rdr.GetString(2);
                    poco.CompanyName = rdr.GetString(3);
                    poco.TimeStamp = (byte[])rdr[4];
                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();

                return pocos.Where(p => p != null).ToList();
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                        WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                        SET [Company] = @Company
                                            ,[LanguageID] = @LanguageID
                                            ,[Company_Name] = @Company_Name
                                            ,[Company_Description] = @Company_Description
                                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
