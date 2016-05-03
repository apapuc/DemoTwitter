﻿using System;
using System.ComponentModel.DataAnnotations;
using DemoTwitter.Models;


namespace DemoTwitter.Models
{

    public class LoginUserModel
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter your username")]
        [StringLength(24, MinimumLength = 4, ErrorMessage = "Username must have from 4 to 24 characters")]
        public string Username { get; set; }



        [StringLength(24, MinimumLength = 4, ErrorMessage = "Password must have from 4 to 24 characters")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }

    }
}