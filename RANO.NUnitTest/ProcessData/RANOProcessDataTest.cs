using NUnit.Framework;
using System.Text.Json;
using System.Collections.Generic;
using RANO.Constants;
using RANO.Model;
using RANO.Model.RANO;
using RANO.ProcessData;
using RANO.Services;
using RANO.TestData;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RANO.Model.Ticket;

namespace RANO.NUnitTest.ProcessData
{
    [TestFixture]
    internal class RANOProcessDataTest
    {
        private RANOProcessData processData;
        private List<Ticket> tickets;


		[OneTimeSetUp]
        public void setup1() {
            processData = new RANOProcessData();
			string json = DataTest._dataTicketsANIO;
			tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
		}
        [Test]
        public void RANO_GENERAL()
        {

            RANOEntity general  = processData.ObtenerRANOGeneral(tickets);
            double RanoGeneral = general.CalcularIndicadorRANO();
            Assert.AreEqual(RanoGeneral, 36.4);
        }
        [Test]
        public void RANO_CONTRATISTA()
        {
			RANOEntity generalContratista = processData.ObtenerRANOContratista(tickets); 
			double RanoGeneralContratista = Math.Round(generalContratista.CalcularIndicadorRANO(), 2);
			Assert.AreEqual(RanoGeneralContratista, 33,3);


		}
        [Test]
        public void RANO_NO_CONTRATISTA()
        {
			RANOEntity generalNoContratista = processData.ObtenerRANONoContratista(tickets);
			double RanoGeneralNoContratista = Math.Round(generalNoContratista.CalcularIndicadorRANO(), 1);
			Assert.AreEqual(RanoGeneralNoContratista,50);
		}
    }
}
