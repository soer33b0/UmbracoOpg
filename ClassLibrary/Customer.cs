using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class Customer
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Key]
        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        public int Entries { get; set; }

        public Customer() { }

        public Customer(string _firstName, string _lastName, string _email, int _age)
        {
            FirstName = _firstName;
            LastName = _lastName;
            Email = _email;
            Age = _age;
        }
    }
}
