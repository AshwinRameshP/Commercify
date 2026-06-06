using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercify.Core.Models;

public class AppUser: IdentityUser,IEntity
{
    public AppUser(string email, string firstName, string lastName) : base(email)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public static class MaxLengths
    {
        public const int FirstName = 100;
        public const int LastName = 100;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
