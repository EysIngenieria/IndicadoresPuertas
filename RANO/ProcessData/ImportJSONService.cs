using Newtonsoft.Json;
using RANO.ProcessData.Interfaces;
using System.Collections.Generic;
using System.IO;
using RANO.Model;
using System.Net.Sockets;

namespace RANO.ProcessData
{
	public class ImportJSONService : ImportJSONInterface<Ticket>
	{
		private List<Ticket> Ticket;



		public List<Ticket> getExportTicketFromFile(string filePath)
		{
			 Ticket = new List<Ticket>();
			string json = File.ReadAllText(filePath);
			Console.WriteLine("String: "+json);
			Ticket = JsonConvert.DeserializeObject<List<Ticket>>(json);
			Console.WriteLine("Ticket: " + Ticket);
			return Ticket;
		}


	}

}