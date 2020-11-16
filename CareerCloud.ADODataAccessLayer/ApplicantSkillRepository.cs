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
    public class ApplicantSkillRepository : BaseAdo, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Skill]
                                       ,[Skill_Level]
                                       ,[Start_Month]
                                       ,[Start_Year]
                                       ,[End_Month]
                                       ,[End_Year])
                                 VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Skill
                                       ,@SkillLevel
                                       ,@StartMonth
                                       ,@StartYear
                                       ,@EndMonth
                                       ,@EndYear)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", item.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", item.EndYear);
                    
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT *
                                   FROM [dbo].[Applicant_Skills]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int counter = 0;
                ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
                while (rdr.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = (string)rdr[3];
                    poco.StartMonth = rdr.GetByte(4);
                    poco.StartYear = rdr.GetInt16(5);
                    poco.EndMonth = rdr.GetByte(6);
                    poco.EndYear = rdr.GetInt16(7);
                    poco.TimeStamp = (byte[])rdr[10];
                    pocos[counter] = poco;
                    counter++;
                }
                conn.Close();

                return pocos.Where(p => p != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                        WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE UPDATE [dbo].[Applicant_Skills]
                                       SET [Applicant] = @Applicant
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] = @Skill_Level
                                          ,[Start_Month] = @Start_Month
                                          ,[Start_Year] = @Start_Year
                                          ,[End_Month] = @End_Month
                                          ,[End_Year] = @End_Year
                                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", item.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", item.EndYear);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
