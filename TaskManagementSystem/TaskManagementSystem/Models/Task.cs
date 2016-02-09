using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("TASK")]
    public class Task
    {
        [Key]
        public int TASK_ID { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public string TASK_NAME { get; set; }
        public int? TASK_USER_ID { get; set; }
        public int TASK_PROJECT_ID { get; set; }

        [DisplayName("TaskStart date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TASK_START_DATE { get; set; }

        [DisplayName("TaskEnd date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TASK_END_DATE { get; set; }
        public string TASK_PRIORITY { get; set; }
        public int TASK_ESTIMATED_HOUR { get; set; }
        public int? TASK_HOUR_CONSUMED { get; set; }
        public string TASK_STATUS { get; set; }
        public string PROJECT_PHASE { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? U_TASK_CREATE_DATE { get; set; }
    }
}