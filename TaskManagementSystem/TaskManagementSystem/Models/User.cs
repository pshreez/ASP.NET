using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{

    [Table("USERS")]   // map user model class to the database table tblUSERS using "Table" attribute
    public class User
    {
        
        [Key]
        public int U_ID { get; set; }
        public string U_FIRST_NAME { get ; set; }
        public string U_LAST_NAME { get; set; }
        public string U_LOGIN_NAME { get; set; }
        public string U_PASSWORD { get; set; }
        public string U_EMAIL { get; set; }
        public string U_POSITION { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime>  U_REGISTER_DATE { get; set; }




    }
 }