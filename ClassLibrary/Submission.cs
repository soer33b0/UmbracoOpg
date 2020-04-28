using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Submission
    {
        [Required]
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        
        [Key]
        [Required]
        [DisplayName("E-mail Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
       
        [Required]
        [DisplayName("Serial Number")]
        [Range(10000000, 99999999, ErrorMessage = "Must be 8 digits")]
        public int SerialNum { get; set; }

        public Submission() { }

        public Submission(string _firstname, string _lastname, string _email, int _serialNum)
        {
            FirstName = _firstname;
            LastName = _lastname;
            Email = _email;
            SerialNum = _serialNum;
        }
    }
}
