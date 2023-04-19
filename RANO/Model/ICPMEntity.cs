namespace RANO.Model
{
	public class ICPMEntity
	{
		private double icpm { get; set; }

		private int TAP { get; set; }

		private int APE { get; set; }

		private List<Ticket> TicketTAP { get; set; }

		private List<Ticket> TicketAPE { get; set; }

		public ICPMEntity(int tAP, int aPE, List<Ticket> ticketTAP, List<Ticket> ticketAPE)
		{
			TAP = tAP;
			APE = aPE;
			TicketTAP = ticketTAP;
			TicketAPE = ticketAPE;
		}

		public double CalcularIndicadorIEPM()
		{
			double icpmCalculado = 0;
			if(TAP> 0) {
			icpmCalculado = APE/TAP;
			}if(icpmCalculado != 0)
			{
				return icpmCalculado*100;
			}
			else
			{
				return 0;
			}

		}
	}
}
