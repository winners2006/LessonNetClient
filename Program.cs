using System;
using System.Threading;
using System.Threading.Tasks;
using LessonNet;

namespace LessonNetClient
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var client = new NetMqClient();

			Console.WriteLine("Введите сообщение (или 'Exit' для завершения):");

			while (true)
			{
				string input = Console.ReadLine();
				if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase))
				{
					await client.SendMessageAsync(input, CancellationToken.None);
					break;
				}

				await client.SendMessageAsync(input, CancellationToken.None);
			}

			Console.WriteLine("Клиент завершил работу.");
		}
	}
}
