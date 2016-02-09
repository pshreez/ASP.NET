using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("TASK_ASSIGN")]
    public class Taskassign
	{
        [Key]
        public int ID { get; set; }
        public int  TASK_USER_ID { get; set; }
        public int TASK_ID_ASSIGNED { get; set; }
        public int PROJECT_ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime TASK_ASSIGN_DATE { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> TASK_UP_DATE { get; set; }

        public string TASK_STATUS { get; set; }
        public Nullable<int>  TASK_HOUR_CONSUMED { get; set; }


    }
}