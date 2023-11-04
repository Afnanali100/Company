using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name Is Required !!")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required !!")]

		public string LName { get; set; }

		[Required(ErrorMessage = "Email Is Required !!")]
		[EmailAddress(ErrorMessage ="Invalid Email ")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required !!")]

		public string Password { get; set; }


		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Confirm Password Doesn't Match Password")]
		[Required(ErrorMessage = " Confirm Password Is Required !!")]

		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = " U Must Confirm Our Ploicies !!")]

		public bool IsAgree { get; set; }


	
	}
}
