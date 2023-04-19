namespace RANO.Model
{
    public class Evento
    {
        public string versionTrama { get; set; }
        public string idRegistro { get; set; }
        public string idOperador { get; set; }
        public string idEstacion { get; set; }
        public string idVagon { get; set; }
        public string idPuerta { get; set; }
        public string codigoPuerta { get; set; }
        public string fechaHoraLecturaDato { get; set; }
        public string fechaHoraEnvioDato { get; set; }
        public string tipoTrama { get; set; }
        public string tramaRetransmitida { get; set; }
        public string codigoEvento { get; set; }
        public int numeroEventoBusEstacion { get; set; }
        public bool estadoAperturaCierrePuertas { get; set; }

    }
}
