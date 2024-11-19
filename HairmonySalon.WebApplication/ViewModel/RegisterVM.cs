﻿using System.ComponentModel.DataAnnotations;

namespace HairHarmonySalon.ViewModel
{
	public class RegisterVM
	{
			[Required]
			[EmailAddress]
			public string Email { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			[Required]
			public string Name { get; set; }

			[Required]
			public string UserType { get; set; }
	}
	}

