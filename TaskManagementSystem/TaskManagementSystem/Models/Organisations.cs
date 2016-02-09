using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("ORGANISATIONS")]
    public class Organisations
    {
        [Key]
        public  int ORG_ID { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_DESCRIPTION { get; set; }
    }
}