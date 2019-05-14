using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.ResponseViewModel
{
    public class UserResponseViewModel
    {
        public string Id { get; set; }

        public string CNP { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool IsAdmin { get; set; }

        public double Balance { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
