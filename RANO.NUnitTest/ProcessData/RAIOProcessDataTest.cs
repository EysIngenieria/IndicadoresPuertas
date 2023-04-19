using System.Text.Json;
using NUnit.Framework;
using RAIO.Services;
using RANO.Model;
using RANO.Model.RANO;
using RANO.ProcessData;
using RANO.Services;
using RANO.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RANO.Model.Ticket;

namespace RANO.NUnitTest.ProcessData
{
	[TestFixture]
	internal class RAIOProcessDataTest
	{

		private RAIOProcessData processData;

        private RAIOService service;

		private List<Ticket> tickets;

		[SetUp]

		public void setUpScenario()
		{
			service = new RAIOService();
			processData = new RAIOProcessData();
			string json = DataTest._dataTicketsAIO;
			tickets = JsonSerializer.Deserialize<List<Ticket>>(json);


		}
		[Test]
        public void RAIO_GENERAL()
        {
			setUpScenario();
			RAIOEntity entityGeneral = processData.ObtenerRAIOGeneral(tickets);
			double RAIOGeneral = entityGeneral.CacularIndicadorRAIO();
			Assert.AreEqual(RAIOGeneral, 37.5);

		}
		[Test]
        public void RAIO_CONTRATISTA()
        {
			setUpScenario();
			RAIOEntity entityContratista = processData.ObtenerRAIOContratista(tickets);
			double RAIOContratista = entityContratista.CacularIndicadorRAIO();
			Assert.AreEqual(Math.Round(RAIOContratista,2), 42.9);

		}
		[Test]
        public void RAIO_NO_CONTRATISTA()
        {
			setUpScenario();
			RAIOEntity entityContratista = processData.ObtenerRAIONoContratista(tickets);
			double RAIONoContratista = Math.Round(entityContratista.CacularIndicadorRAIO(),1);
			Assert.AreEqual(RAIONoContratista, 0);
		}


	}
}
