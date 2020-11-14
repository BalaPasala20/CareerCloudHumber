using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public bool IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

    }
}
