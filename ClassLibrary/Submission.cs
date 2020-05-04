using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Your current age")]
        public int Age { get; set; }

        [Required]
        [Range(10000000, 99999999, ErrorMessage = "Must be 8 digits")]
        [Display(Name = "Valid serial number")]
        public int SerialNum { get; set; }

        public int Wins { get; set; }

        public Submission() { }

        public Submission(string _fullname, string _email, int _age, int _serialNum)
        {
            FullName = _fullname;
            Email = _email;
            Age = _age;
            SerialNum = _serialNum;
        }
    }
}
