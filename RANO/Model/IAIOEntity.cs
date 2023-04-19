using RANO.ProcessData.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace RANO.Model
{
	public class IAIOEntity
	{
		public IAIOEntity(double totalPuertas, double totalPuertasANIO, double suma_pano, JsonArray puertas)
		{
			iaio = CalcularIndicadorIAIO();
			this.totalPuertas = totalPuertas;
			this.totalPuertasANIO = totalPuertasANIO;
			this.suma_pano = suma_pano;
			this.puertas = puertas;
		}

		private double iaio { get; set; }

		private double totalPuertas { get; set; }

		private double totalPuertasANIO { get; set; }

		private double suma_pano { get; set; }

		private JsonArray puertas { get; set; }

		public double CalcularIndicadorIAIO()
		{
			iaio = Convert.ToDouble(((totalPuertas - totalPuertasANIO)*100 + suma_pano) / totalPuertas);
			return iaio;
		}
		public override string ToString()
		{
			return "TotalPuertas: " + totalPuertas + " TotalPuertasANIO: " + totalPuertasANIO + " SumaPano: " + suma_pano;
		}
		public JsonArray getPuertas()
		{
			return puertas;
		}

	}
}
