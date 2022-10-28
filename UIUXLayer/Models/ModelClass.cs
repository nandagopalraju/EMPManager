using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIUXLayer.Models
{
    public class ModelClass
    {

        [Required(ErrorMessage = "Title is  Required")]
        public string? title { get; set; }

        [Required(ErrorMessage = "FirstName is  Required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "First name should be between 3 and 30 characters")]
        public string? firstName { get; set; }


        [Required(ErrorMessage = "LastName is  Required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last name should be between 3 and 30 characters")]
        public string? lastName { get; set; }


        [Required(ErrorMessage = "Gender is  Required")]
        public string? gender { get; set; }

        [Key]
        [Required(ErrorMessage = " Username is  Required")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Username should be atleast 8 characters")]
        public string? username { get; set; }


        [Required(ErrorMessage = "Password is  Required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password should be minimum 7 characters")]
        public string? password { get; set; }



        public string? emailId { get; set; }



        public string? mobile { get; set; }


        [Required(ErrorMessage = "Address is  Required")]
        public string? address { get; set; }


        [Required(ErrorMessage = "Country is  Required")]
        public string? country { get; set; }


        [Required(ErrorMessage = "Salary is  Required")]
        [Range(10000, 2500000)]
        public int salary { get; set; }

        [Required(ErrorMessage = "Designation is  Required")]
        public string? designation { get; set; }

        public string? imagePath { get; set; }
    }
}
