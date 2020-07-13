using System.Text;

namespace QuizApp.Application
{
	public class IdentityOptions
	{
		public bool Enabled { get; set; }
		public string Secret { get; set; }
		public string Issuer { get; set; }
		public int ExpirationSeconds { get; set; }
		public byte[] Key => Encoding.ASCII.GetBytes(Secret);
	}
}
