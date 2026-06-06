using Microsoft.AspNetCore.Identity;

namespace Commercify.Core.Models;

public class AppUser: IdentityUser,IEntity
{
    public AppUser(string email, string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public static class MaxLengths
    {
        public const int FirstName = 100;
        public const int LastName = 100;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
