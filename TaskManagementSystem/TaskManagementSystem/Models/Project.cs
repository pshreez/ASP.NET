using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("PROJECT")]
    public class Project
    {
        [Key]
        public int PROJECT_ID { get; set; }
        public string PROJECT_NAME { get; set; }

       
        public int PROJECT_MANAGER { get; set; }

        [DisplayName("ProjectStart date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PROJECT_START_DATE { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PROJECT_END_DATE { get; set; }
        public string PROJECT_STATUS { get; set; }
        public int ORG_ID { get; set; }
        public string PROJECT_DESCRIPTION { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> U_PROJECT_CREATE_DATE { get; set; }










    }

}