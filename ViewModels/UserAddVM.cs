﻿using Microsoft.VisualBasic;
using System;

namespace ZwajApp.API.ViewModels{
    public class UserAddVM

    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string KnownAs { get; set; }
        public string Interests { get; set; }
        public string Photos { get; set; }
        public string Country { get; set; }
        public string Introduction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public string LookingFor { get; set; }
    
    }
}
