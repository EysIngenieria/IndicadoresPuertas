using Microsoft.VisualStudio.TestTools.UnitTesting;
using RANO.Model;
using RANO.ProcessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANO.ProcessData.Tests
{
	[TestClass()]
	public class ImportJSONServiceTests
	{

		private ImportJSONService processData;

		private List<Ticket> Ticket;


		[TestMethod()]
		public void getExportTicketFromFileTest()
		{
			processData = new ImportJSONService();

			Ticket = processData.getExportTicketFromFile("C:\\Users\\juana\\Downloads\\Ticket.json");
			Assert.IsNotNull(Ticket);
		}
	}
}