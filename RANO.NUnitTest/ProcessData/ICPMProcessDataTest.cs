using NUnit.Framework;
using RANO.Model;
using RANO.ProcessData;
using ICMP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANO.NUnitTest.ProcessData
{
	internal class ICPMProcessDataTest
	{
		private ICPMProcessData processData;

		private List<Ticket> tickets;

		private ICPMServiceImp service;



		[SetUp]
		public void setUpScenarioICPM()
		{
			service = new ICPMServiceImp();
			processData = new ICPMProcessData();
			tickets = service.ObtenerTicketsDesdeJSON();
		}

		[Test]
        public void IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsyncTest()
        {
			setUpScenarioICPM();
			ICPMEntity entity = processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(tickets);
			double ICPMGeneral = entity.CalcularIndicadorIEPM();
		   Assert.AreEqual(ICPMGeneral,100);
           
        }
		[Test]
        public void IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOTAsyncTestPuerta()
        {
			setUpScenarioICPM();
			ICPMEntity entityPuerta = processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(tickets, "Puerta");
			double ICPMPuerta = entityPuerta.CalcularIndicadorIEPM();      
			Assert.AreEqual(ICPMPuerta,100);
		}
		[Test]
        public void IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOTAsyncTestITS()
        {
			setUpScenarioICPM();
			ICPMEntity entityITS = processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(tickets, "Componente ITS");
			double ICPMITS = entityITS.CalcularIndicadorIEPM();
			Console.WriteLine(ICPMITS);
			Assert.AreEqual(ICPMITS,0);
        }
		[Test]
        public void IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOTAsyncTestRFID()
        {
			setUpScenarioICPM();
			ICPMEntity entityRFID = processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(tickets, "Componente RFID");
			double ICPMRFID = entityRFID.CalcularIndicadorIEPM();
			Assert.AreEqual(ICPMRFID,0);
		}
		
	}
}
