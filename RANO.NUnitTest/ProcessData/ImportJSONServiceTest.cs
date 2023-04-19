using NUnit.Framework;
using RANO.Model;
using RANO.ProcessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RANO.Model.Ticket;

namespace RANO.NUnitTest.ProcessData
{
	internal class ImportJSONServiceTest
	{
		private ImportJSONService processData;

		private List<Ticket> tickets;



		[Test]
		public void getImportTicketTest()
		{
			processData = new ImportJSONService();
			tickets = processData.getExportTicketFromFile("C:\\Users\\juana\\Downloads\\ticket.json");
			Assert.IsNotEmpty(tickets);
		}
	}
}
