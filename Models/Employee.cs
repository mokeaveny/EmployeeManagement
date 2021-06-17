using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}