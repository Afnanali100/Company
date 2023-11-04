using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class ResetPasswordViewModel
	{

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "New Password Is Required !!")]

		public string NewPassword { get; set; }


		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Confirm Password Doesn't Match Password")]
		[Required(ErrorMessage = " Confirm Password Is Required !!")]

		public string ConfirmPassword { get; set; }
	}
}
