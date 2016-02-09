using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    [Table("DISCUSSION")]
    public class Discuss
    {
        [Key]
        public int DISCUSSION_ID{ get; set; }
        public string DISCUSSION_TEXT { get; set; }
        public int DISCUSSION_USER_ID { get; set; }
        public DateTime DISCUSSION_POST_DATE { get; set; }

        public string DISCUSS_TITLE { get; set; }
         

    }
}