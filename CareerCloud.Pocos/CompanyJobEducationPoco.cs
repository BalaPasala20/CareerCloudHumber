using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Major { get; set; }
        public short Importance { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public CompanyJobPoco CompanyJobs { get; set; }
    }
}
