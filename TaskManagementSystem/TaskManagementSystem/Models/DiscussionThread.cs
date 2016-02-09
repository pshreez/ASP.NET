using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
  
    [Table("DISCUSSION_THREADS")]
    public class DiscussionThread
    {
        [Key]
        public int D_THREAD_ID { get; set; }
        public string  D_THREAD_TEXT { get; set; }
        public Nullable<int>  D_THREAD_USER_ID { get; set; }

        public Nullable<DateTime> D_THREAD_POST_DATE { get; set; }
        public int D_THREAD_ROOT_ID { get; set; }

      
    }
}