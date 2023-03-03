using Newtonsoft.Json;
using NUnit.Framework;
using RANO.Model;
using RANO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RANO.Model.Tickets;

namespace RANO.NUnitTest.ServicesTest
{
    [TestFixture]
    internal class ProcessDataNUnitTest
    {
        private RANOProcessData processData;
        private List<Tickets.Ticket> tickets;


        [SetUp]
        public void setup1() {
            String json = "[\r\n  {\r\n    \"id_ticket\": \"TICKET-85\",\r\n    \"idEstacion\": \"9115\",\r\n    \"idVagon\": \"WA\",\r\n    \"puerta\": \"9115-WA-OR-3\",\r\n    \"componente\": \"Puerta\",\r\n    \"identificacion\": \"N1S-0050\",\r\n    \"tipo_mantenimiento\": \"Mantenimiento Correctivo\",\r\n    \"nivel_falla\": \"ANIO\",\r\n    \"codigo_falla\": \"No reproducción de mensajes de audio FLL-N-0001\",\r\n    \"fecha_apertura\": \"2023-03-01T11:46:26.298\",\r\n    \"fecha_cierre\": \"2023-03-01T11:52:44.946\",\r\n    \"fecha_arribo_locacion\": \"2023-03-01T11:46:37.334\",\r\n    \"componente_Parte\": null,\r\n    \"tipo_reparacion\": \"Ajuste\",\r\n    \"tipo_ajuste_configuracion\": \"Ajuste conexiones AJS-P-0006\",\r\n    \"descripcion_reparacion\": null,\r\n    \"diagnostico_causa\": \"A cargo del contratista\",\r\n    \"estado_ticket\": \"Cerrado\"\r\n  },\r\n  {\r\n    \"id_ticket\": \"TICKET-83\",\r\n    \"idEstacion\": \"9119\",\r\n    \"idVagon\": \"WB\",\r\n    \"puerta\": \"9119-WB-OR-1\",\r\n    \"componente\": \"Puerta\",\r\n    \"identificacion\": \"N1T-0035\",\r\n    \"tipo_mantenimiento\": \"Mantenimiento Correctivo\",\r\n    \"nivel_falla\": \"ANIO\",\r\n    \"codigo_falla\": \"La puerta tiene un componente suelto, pero no pone en riesgo a los usuarios FLL-N-0013\",\r\n    \"fecha_apertura\": \"2023-02-28T14:34:25.372\",\r\n    \"fecha_cierre\": \"2023-02-28T14:35:26.509\",\r\n    \"fecha_arribo_locacion\": \"2023-02-28T14:34:32.861\",\r\n    \"componente_Parte\": null,\r\n    \"tipo_reparacion\": \"Ajuste\",\r\n    \"tipo_ajuste_configuracion\": \"Otros ajustes de puertas AJS-P-0100\",\r\n    \"descripcion_reparacion\": null,\r\n    \"diagnostico_causa\": \"A cargo del contratista\",\r\n    \"estado_ticket\": \"Cerrado\"\r\n  },\r\n  {\r\n    \"id_ticket\": \"TICKET-81\",\r\n    \"idEstacion\": \"9115\",\r\n    \"idVagon\": \"WC\",\r\n    \"puerta\": \"9115-WC-OR-3\",\r\n    \"componente\": \"Puerta\",\r\n    \"identificacion\": \"N1S-0058\",\r\n    \"tipo_mantenimiento\": \"Mantenimiento Correctivo\",\r\n    \"nivel_falla\": \"ANIO\",\r\n    \"codigo_falla\": \"La puerta tiene un componente suelto, pero no pone en riesgo a los usuarios FLL-N-0013\",\r\n    \"fecha_apertura\": \"2023-02-28T09:00:09.276\",\r\n    \"fecha_cierre\": \"2023-03-01T11:30:39.595\",\r\n    \"fecha_arribo_locacion\": \"2023-02-28T09:00:23.676\",\r\n    \"componente_Parte\": null,\r\n    \"tipo_reparacion\": \"Ajuste\",\r\n    \"tipo_ajuste_configuracion\": \"Otros ajustes de puertas AJS-P-0100\",\r\n    \"descripcion_reparacion\": null,\r\n    \"diagnostico_causa\": \"A cargo del contratista\",\r\n    \"estado_ticket\": \"Cerrado\"\r\n  },\r\n  {\r\n    \"id_ticket\": \"TICKET-76\",\r\n    \"idEstacion\": \"9116\",\r\n    \"idVagon\": \"WA\",\r\n    \"puerta\": \"9116-WA-OR-5\",\r\n    \"componente\": \"Puerta\",\r\n    \"identificacion\": \"N1T-0045\",\r\n    \"tipo_mantenimiento\": \"Mantenimiento Correctivo\",\r\n    \"nivel_falla\": \"ANIO\",\r\n    \"codigo_falla\": \"No se logra comunicación Bluetooth FLL-N-0018\",\r\n    \"fecha_apertura\": \"2023-02-28T08:19:55.699\",\r\n    \"fecha_cierre\": \"2023-02-28T08:24:42.037\",\r\n    \"fecha_arribo_locacion\": \"2023-02-28T08:21:49.998\",\r\n    \"componente_Parte\": null,\r\n    \"tipo_reparacion\": \"Configuracion\",\r\n    \"tipo_ajuste_configuracion\": \"Otros ajustes ITS AJS-P-0100\",\r\n    \"descripcion_reparacion\": null,\r\n    \"diagnostico_causa\": null,\r\n    \"estado_ticket\": \"Cerrado\"\r\n  },\r\n  {\r\n    \"id_ticket\": \"TICKET-75\",\r\n    \"idEstacion\": \"9117\",\r\n    \"idVagon\": \"WB\",\r\n    \"puerta\": \"9117-WB-OR-1\",\r\n    \"componente\": \"Puerta\",\r\n    \"identificacion\": \"N1T-0021\",\r\n    \"tipo_mantenimiento\": \"Mantenimiento Correctivo\",\r\n    \"nivel_falla\": \"ANIO\",\r\n    \"codigo_falla\": \"La puerta intenta cerrar pero no logra el cierre total FLL-N-0009\",\r\n    \"fecha_apertura\": \"2023-02-27T18:06:08.046\",\r\n    \"fecha_cierre\": \"2023-02-27T18:07:20.68\",\r\n    \"fecha_arribo_locacion\": \"2023-02-27T18:06:17.873\",\r\n    \"componente_Parte\": null,\r\n    \"tipo_reparacion\": \"Ajuste\",\r\n    \"tipo_ajuste_configuracion\": \"Ajuste sensor final de carrera AJS-P-0002\",\r\n    \"descripcion_reparacion\": null,\r\n    \"diagnostico_causa\": \"A cargo del contratista\",\r\n    \"estado_ticket\": \"Cerrado\"\r\n  }\r\n]";
            tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            processData= new RANOProcessData();
        }


        [Test]
        public void ANIO_CERRADO_A_TIEMPO_TEST()
        {
            setup1();

            Assert.IsTrue(processData.ANIO_CERRADO_A_TIEMPO(tickets).Count == 5);
        }

    }
}
