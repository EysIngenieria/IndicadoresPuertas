using System.Globalization;
using System.Text.Json.Serialization;

namespace RAIO.Model
{
    public class Tickets
    {
        public class Ticket
        {
            public string id_ticket { get; set; }
            public string idEstacion { get; set; }
            public string idVagon { get; set; }
            public string puerta { get; set; }
            public string componente { get; set; }
            public string identificacion { get; set; }
            public string tipo_mantenimiento { get; set; }
            public string nivel_falla { get; set; }
            public string icodigo_falla { get; set; }
            public string fecha_apertura { get; set; }
            public string fecha_cierre { get; set; }
            public string fecha_arribo_locacion { get; set; }
            public string componente_Parte { get; set; }
            public string tipo_reparacion { get; set; }
            public string tipo_ajuste_configuracion { get; set; }
            public string descripcion_reparacion { get; set; }
            public string diagnostico_causa { get; set; }
            public string estado_ticket { get; set; }
        }



    }
}
