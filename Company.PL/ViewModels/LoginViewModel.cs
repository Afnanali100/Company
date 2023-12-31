﻿using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required !!")]
		[EmailAddress(ErrorMessage = "Invalid Email ")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required !!")]

		public string Password { get; set; }

		public bool RememberMe { get; set; } = false;
	}
}
