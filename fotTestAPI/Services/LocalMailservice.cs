namespace fotTestAPI.Services
{
	public class LocalMailservice : IMailservice
	{

		private string mailTo { get; set; } = "bassel@gmail.com";
		private string mailFrom { get; set; } = "HR@reflection.com";


		public void send(string subject, string message)
		{
			Console.WriteLine($"mail from {mailFrom} to {mailTo} , " + $"with {nameof(LocalMailservice)}");
			Console.WriteLine($"Subject {subject} ");
			Console.WriteLine($"Message {message} ");

		}

	}
}
