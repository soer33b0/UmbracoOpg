using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class Submission
    {
        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; }
        
        [Key]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [Range(18, 120, ErrorMessage ="Contestants must be over 18 years old")]
        public int Age { get; set; }

        [Required]
        [Range(10000000, 99999999, ErrorMessage = "Must be 8 digits")]
        public int SerialNum { get; set; }

        public int Entries { get; set; }

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
