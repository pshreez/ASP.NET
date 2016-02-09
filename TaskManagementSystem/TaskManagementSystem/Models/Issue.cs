using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("ISSUES")]
    public class Issue
    {
        [Key]
        public int ISSUE_ID { get; set; }
        public string ISSUE_DESCRIPTION { get; set; }
        public int ISSUE_REPORTER_ID { get; set; }

        public int TASK_ID { get; set; }

      
    }
}