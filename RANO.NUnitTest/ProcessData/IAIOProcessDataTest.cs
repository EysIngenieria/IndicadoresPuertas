using NUnit.Framework;
using RAIO.Services;
using RANO.Model;
using RANO.ProcessData;
using RANO.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RANO.NUnitTest.ProcessData
{



	internal class IAIOProcessDataTest
    {
		private IAIOProcessData processData;
		private List<Ticket> tickets;

		[SetUp]
		public void setUpScenarioIAIO()
		{

			processData = new IAIOProcessData();
			string json = DataTest._dataTicketsAIO;
			tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
		}
		[Test]
		public void IAIOGeneral()
		{
			setUpScenarioIAIO();
			IAIOEntity iaioGEneral = processData.ObtenerIAIOGeneral(tickets);
			double iaio = iaioGEneral.CalcularIndicadorIAIO();
			double esperado = 92.465753424657535;
			Assert.AreEqual(esperado, iaio);

		}
		[Test]
		public void IAIOContratista()
		{
			setUpScenarioIAIO();
			IAIOEntity iaioContratista = processData.ObtenerIAIOContratista(tickets);
			double iaio = iaioContratista.CalcularIndicadorIAIO();
			double esperado = 98.6986301369863;
			Assert.AreEqual(esperado, iaio);

		}
		[Test]
		public void IAIONoContratista()
		{
			setUpScenarioIAIO();
			IAIOEntity entitiyNoContratista = processData.ObtenerIAIONoContratista(tickets);
			double iaio = entitiyNoContratista.CalcularIndicadorIAIO();
			double esperado = 99.93150684931507;
			Assert.AreEqual(esperado, iaio);

		}
	}
}
