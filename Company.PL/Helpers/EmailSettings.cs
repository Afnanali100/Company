using Company.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("afnan1000ali@gmail.com", "hhwevazexenjcszz");
			client.Send("afnan1000ali@gmail.com", email.To, email.Subject, email.Body); 
		}
	}
}
