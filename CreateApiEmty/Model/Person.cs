using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateApiEmty.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNamber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
    }
}
