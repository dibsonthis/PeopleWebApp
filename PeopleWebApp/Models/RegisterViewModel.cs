﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleWebApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(controller: "Account", action: "IsEmailAvailable")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
        ErrorMessage = "Password and confirmation password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
