using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.Models
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Department Department { get; set; }
    }
}