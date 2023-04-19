namespace RANO.Model.RANO
{
	public class RAIOEntity
	{
	
		private double TCI { get; set; }

		private double TAI { get; set; }

		private List<Ticket> TicketTCI { get; set; }
		private List<Ticket> TicketTCINoContratista { get; set; }
		private List<Ticket> TicketTAI { get; set; }

		public RAIOEntity(double tCI, double tAI, List<Ticket> TicketTCI, List<Ticket> TicketTAI)
		{
			TCI = tCI;
			TAI = tAI;
			this.TicketTCI = TicketTCI;
			this.TicketTAI = TicketTAI;
		}
		public RAIOEntity(double tCI, double tAI, List<Ticket> TicketTCI, List<Ticket> TicketTAI, List<Ticket> ticketTCINoContratista)
		{
			TCI = tCI;
			TAI = tAI;
			this.TicketTCI = TicketTCI;
			this.TicketTAI = TicketTAI;
			TicketTCINoContratista = ticketTCINoContratista;
		}

		public List<Ticket> getTCI()
		{
			return TicketTCI;

		}
		public List<Ticket> getTAI()
		{
			return TicketTAI;
		}
		public List<Ticket> getTCINoContratista()
		{
			return TicketTCINoContratista;
		}
		public double CacularIndicadorRAIO()
		{
			
			double RAIOGeneral = 0;
			if (TAI != 0)
			{
				RAIOGeneral = (TCI / TAI) * 100;
				
			}
			else
			{
				RAIOGeneral = 100;
			}
			Console.WriteLine(RAIOGeneral);
			return Math.Round(RAIOGeneral,1);
		}
		public override string ToString()
		{
			return $"TCI: {TCI}, TAI: {TAI}";
		}



	}
}
