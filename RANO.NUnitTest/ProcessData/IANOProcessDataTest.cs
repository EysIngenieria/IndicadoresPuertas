using NUnit.Framework;
using RAIO.Services;
using RANO.Model;
using RANO.ProcessData;
using RANO.Services;
using RANO.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RANO.NUnitTest.ProcessData
{
	internal class IANOProcessDataTest
	{
		private IANOProcessData processData;
		private List<Ticket> tickets;

		[SetUp]
		public void setUpScenarioIAIO()
		{
			processData = new IANOProcessData();
			string json = DataTest._dataTicketsANIO;
			tickets = JsonSerializer.Deserialize<List<Ticket>>(json);


		}
		[Test]
		public void IANOGeneral()
		{
			setUpScenarioIAIO();
			IANOEntity entity = processData.OBtenerIndicadorIANO(tickets);
			double iaio = entity.CalcularIndicadorIANO();
			iaio = iaio * 100;
			double esperado = 99.771689497716892;
			Assert.AreEqual(esperado, iaio);

		}
		[Test]
		public void IANOContratista()
		{
			setUpScenarioIAIO();
			IANOEntity entityContratista = processData.ObtenerIndicadorIANOContratista(tickets);
			double ianoContratista = entityContratista.CalcularIndicadorIANO();
			ianoContratista= ianoContratista * 100;
			double esperado = 99.817351598173502;
			Assert.AreEqual(esperado, ianoContratista);

		}
		[Test]
		public void IANONoContratista()
		{
			setUpScenarioIAIO();
			IANOEntity entityNoContratista = processData.ObtenerIndicadorIANONoContratista(tickets);
			double ianoNoContratista = entityNoContratista.CalcularIndicadorIANO();
			ianoNoContratista = ianoNoContratista*100;
			double esperado = 99.954337899543376;
			Assert.AreEqual(esperado, ianoNoContratista);

		}
	}
}
