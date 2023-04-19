using System.Text.Json.Nodes;

namespace RANO.Model
{
	public class IANOEntity
	{
		private double iano { get; set; }

		private double totalPuertas { get; set; }

		private double totalPuertasANIO { get; set; }

		private double suma_pano { get; set; }

		private JsonArray puertas { get; set; }

		public IANOEntity( double totalPuertas, double totalPuertasANIO, double suma_pano, JsonArray puertas)
		{
			iano = CalcularIndicadorIANO() ;
			this.totalPuertas = totalPuertas;
			this.totalPuertasANIO = totalPuertasANIO;
			this.suma_pano = suma_pano;
			this.puertas = puertas;
		}

		public JsonArray getPuertas()
		{
			return puertas;
		}
		public override string ToString()
		{
			return "TotalPuertas: " + totalPuertas + " TotalPuertasANIO: " + totalPuertasANIO + " SumaPano: " + suma_pano;
		}
		public double CalcularIndicadorIANO()
		{
			iano  = Convert.ToDouble(((totalPuertas - totalPuertasANIO) + suma_pano) / totalPuertas);
			return iano;
		}
	}
}
