﻿using System;

namespace Common.ViewModels.RequestViewModel
{
    public class UserRequestViewModel
    {
        public string CNP { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
