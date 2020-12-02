﻿using System.ComponentModel.DataAnnotations;

namespace MovieShop.Core.Models.Requests
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}