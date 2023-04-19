using RANO.Model;

namespace RANO.ProcessData
{
	public class ICOProcessData
	{
		List<EstacionEntity> estaciones;

		public double tiempoTotalPuertasFueraDeServicio()
		{
			double result = 0;
			foreach (var estacion in estaciones)
			{
				result += estacion.tiempoTotalPorPuertaFueraDeServicio();
			}
			return result;
		}

	}
}
