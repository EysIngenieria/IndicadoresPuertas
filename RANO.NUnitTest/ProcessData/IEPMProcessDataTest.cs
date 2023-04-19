using NUnit.Framework;
using RANO.Model;
using RANO.ProcessData;
using ICMP.Services;
using RANO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RANO.NUnitTest.ProcessData
{
	internal class IEPMProcessDataTest
	{
		private IEPMProcessData _processData;

		private IEPMServiceImp services;
		private List<Ticket> tickets;

		

		[SetUp]
		public void setUpScenarioIEPM()
		{
			services = new IEPMServiceImp();
			_processData = new IEPMProcessData();
			tickets = services.ObtenerTodosLosTicketsDesdeUnaFecha("2023-01-01","2023-03-02").Result;

		}
		[Test]
        public void IndicadorDeEfectividadDelPlanDeMantenimiento()
        {
			setUpScenarioIEPM();
			IEPMEntity entity = _processData.IndicadorDeEfectividadDelPlanDeMantenimiento(tickets);
			double icpm = entity.CalcularIndicadorIEPM();
			double esperado = 37.2;
			Assert.AreEqual(esperado, icpm);
			
           
        }
		[Test]
        public void IndicadorDeEfectividadDelPlanDeMantenimientoNoContratista()
        {
			setUpScenarioIEPM();
			IEPMEntity entityNoContratista = _processData.IndicadorDeEfectividadDelPlanDeMantenimientoNoContratista(tickets);
			double icpm = entityNoContratista.CalcularIndicadorIEPM();
			double esperado = 76.2;
			Assert.AreEqual(esperado, icpm);


		}
		[Test]
        public void IndicadorDeEfectividadDelPlanDeMantenimientoContratista()
        {
			setUpScenarioIEPM();
			IEPMEntity entityContratista = _processData.IndicadorDeEfectividadDelPlanDeMantenimientoContratista(tickets);
			double icpm = entityContratista.CalcularIndicadorIEPM();
			double esperado =42.1 ;
			Assert.AreEqual(esperado, icpm);


		}
	}
}
