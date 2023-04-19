using NUnit.Framework;
using RANO.Model;

namespace RANO.NUnitTest.ProcessData
{



    internal class EstacionEntityTest
	{
		private EstacionEntity estacion;
		private List<Ticket> tickets;
		private List<Evento> eventos;

		[SetUp]
		public void setUpEstacion1() {
			setUpTickets1();
			setUpEventos1();

            estacion = new EstacionEntity(9115,34,tickets,eventos);
		
		}
        [SetUp]
        public void setUpEstacion2()
        {
            setUpTickets2();
            setUpEventos1();

            estacion = new EstacionEntity(9115, 34, tickets, eventos);

        }

        [SetUp]
        public void setUpEstacion3()
        {
            setUpTickets3();
            setUpEventos1();

            estacion = new EstacionEntity(9115, 34, tickets, eventos);

        }
        [SetUp]
        public void setUpEventos1() {
			eventos = new List<Evento>();

            Evento inicio_01_03 = new Evento();
			inicio_01_03.fechaHoraLecturaDato = "01/03/2023 04:00:00.000";
			inicio_01_03.idVagon = "INICIO-OP";

            Evento fin_02_03 = new Evento();
            fin_02_03.fechaHoraLecturaDato = "02/03/2023 00:30:00.000";
			fin_02_03.idVagon = "FIN-OP";

            eventos.Add(inicio_01_03);
			eventos.Add(fin_02_03);
        }

		[SetUp]
		public void setUpTickets1() {
			tickets = new List<Ticket>();
			Ticket t1 = new Ticket();
			t1.id_ticket = "TICKET-01";
			t1.id_estacion = "9115";
			t1.nivel_falla = "AIO";
			t1.fecha_apertura = "2023-03-01T02:00:00.000";
			t1.fecha_cierre = "2023-03-01T05:30:00.000";
			tickets.Add(t1);


        }

        [SetUp]
        public void setUpTickets2()
        {
            tickets = new List<Ticket>();
            Ticket t1 = new Ticket();
            t1.id_ticket = "TICKET-01";
            t1.id_estacion = "9115";
            t1.nivel_falla = "AIO";
            t1.fecha_apertura = "2023-03-01T08:00:00.000";
            t1.fecha_cierre = "2023-03-01T12:00:00.000";
            tickets.Add(t1);


        }
        [SetUp]
        public void setUpTickets3()
        {
            tickets = new List<Ticket>();
            Ticket t1 = new Ticket();
            t1.id_ticket = "TICKET-01";
            t1.id_estacion = "9115";
            t1.nivel_falla = "AIO";
            t1.fecha_apertura = "2023-03-01T18:00:00.000";
            t1.fecha_cierre = "2023-03-02T02:00:00.000";
            tickets.Add(t1);


        }

        [SetUp]
        public void setUpCEI() {
        
        }

        //------------------------------------------------------TEST------------------------------------------------

        [Test]
		public void tiempoTotalPorPuertaFueraDeServicioTest() {
			setUpEstacion1();

            double esperado = 1.5;

            Assert.AreEqual(esperado, estacion.tiempoTotalPorPuertaFueraDeServicio());

        }

        [Test]
        public void tiempoTotalPorPuertaFueraDeServicioTest2()
        {
            setUpEstacion2();

            double esperado = 4;

            Assert.AreEqual(esperado, estacion.tiempoTotalPorPuertaFueraDeServicio());

        }
        [Test]
        public void tiempoTotalPorPuertaFueraDeServicioTest3()
        {
            setUpEstacion3();

            double esperado = 6.5;

            Assert.AreEqual(esperado, estacion.tiempoTotalPorPuertaFueraDeServicio());

        }
        [Test]
        public void CEITest() 
        {
            setUpEstacion1();
            double cei = 1;
            Assert.AreEqual(cei,estacion.CEI());
            
        }

        [Test]
        public void CEFTest()
        {

        }

        [Test]
        public void TMRTest()
        {

        }
        [Test]
        public void TMETest()
        {

        }
        [Test]
        public void EAPTest()
        {

        }

        [Test]
        public void ECPTest()
        {

        }

        [Test]
        public void EABETest()
        {

        }
        [Test]
        public void ECBETest()
        {

        }
    }
}
