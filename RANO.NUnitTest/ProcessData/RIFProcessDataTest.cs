using RANO.Model;
using RANO.ProcessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RANO.NUnitTest.ProcessData
{
	internal class RIFProcessDataTest
	{
		private const string PATH = "C:\\Users\\juana\\Downloads\\ticket.json";
		private List<Ticket> tickets;

		private IRFEntity entity;

		

		private ImportJSONService importJSONService;
		public void setUpScenarioRIF()
		{
			importJSONService = new ImportJSONService();
			tickets = importJSONService.getExportTicketFromFile(PATH);

		}

		private void loadDataTest()
		{
			
		}

		public void CalcularIndicadorRIFEsperado()
		{
			double indicadorEsperado = 98.83;

			double indicadorDado = 0; 

		}
	}
}
