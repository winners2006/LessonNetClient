using LessonNet;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace LessonNetClient
{
	public class Program
	{
		public static void Main(string[] args)
		{
			UdpClient(args[0], args[1]);
		}

		public static void UdpClient(string from, string ip) 
		{
			string messageText;
			
			UdpClient udp = new UdpClient();
			IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

			while (true)
			{
				do
				{
					//Console.Clear();
					Console.WriteLine("Введите сообщение: ");
					messageText = Console.ReadLine();

				} while (string.IsNullOrEmpty(messageText));

				Messenge messenge = new Messenge() { Text = messageText, DateTime = DateTime.Now, NicNameFrom = from, NicNameTo = "Server" };
				string json = messenge.SerializeMessToJson();
				byte[] data = Encoding.UTF8.GetBytes(json);
				udp.Send(data, data.Length, iPEndPoint);

				// Получение подтверждения от сервера
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
				byte[] confirmationBuffer = udp.Receive(ref remoteEP);
				string confirmationMessage = Encoding.UTF8.GetString(confirmationBuffer);
				Console.WriteLine($"Сервер ответил: {confirmationMessage}");
			}
		}
	}
}
