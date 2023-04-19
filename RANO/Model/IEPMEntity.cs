namespace RANO.Model
{
	public class IEPMEntity
	{
		public IEPMEntity(int aME, int aNP, List<Ticket> ticketsANP, List<Ticket> ticketsAME)
		{
			AME = aME;
			ANP = aNP;
			this.ticketsANP = ticketsANP;
			this.ticketsAME = ticketsAME;
		}
		public IEPMEntity(int aME, int aNP, List<Ticket> ticketsANP, List<Ticket> ticketsAME, List<Ticket> ticketsContratista)
		{
			AME = aME;
			ANP = aNP;
			this.ticketsANP = ticketsANP;
			this.ticketsAME = ticketsAME;
			this.ticketsContratista = ticketsContratista;
		}

		private int AME { get; set; }

		private int ANP { get; set; }

		private List<Ticket> ticketsANP { get; set; }

		private List<Ticket> ticketsAME { get; set; }

		private List<Ticket> ticketsContratista { get; set; }


		public double CalcularIndicadorIEPM()
		{
			double iempToPercentage;

			if (AME > 0)
			{
				double IndicadorDeEfectividadDelPlanDeMantenimiento = (Math.Abs(AME - ANP));
				IndicadorDeEfectividadDelPlanDeMantenimiento = IndicadorDeEfectividadDelPlanDeMantenimiento / AME;
				iempToPercentage = IndicadorDeEfectividadDelPlanDeMantenimiento*100;

			}
			else
			{
				iempToPercentage = 100;
			}

			return Math.Round(iempToPercentage, 1);
		}	
	}
}
