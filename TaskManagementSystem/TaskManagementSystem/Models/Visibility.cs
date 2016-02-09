using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("VISIBILITY")]
    public class Visibility
    {
        [Key]
        public int VISIBILITY_FILE_ID { get; set; }
        public string VISIBILITY_STATUS { get; set; }
        public int VISIBILITY_USER_ID { get; set; }
        public int VIEW_UPLOAD { get; set; }
       
    }
}