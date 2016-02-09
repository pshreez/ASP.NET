using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace TaskManagementSystem.Models
{
    [Table("FILEUPLOAD")]
    public class File
    {
        [Key]
        public int FILEUPLOAD_ID { get; set; }
        public string FILEUPLOAD_NAME { get; set; }
        public DateTime FILEUPLOAD_DATE { get; set; }

        public int D_THREAD_ROOT_ID { get; set; }
       
      
    }
}