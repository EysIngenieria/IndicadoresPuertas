using System.Text;

namespace RANO.Model{
	public class RANOEntity
	{
		private double TCN { get; set; }

		private double TAN { get; set; }

		private List<Ticket> TicketTAN { get; set; }

		private List<Ticket> TicketTCN { get; set; }

		public RANOEntity(double tCN, double tAN,  List<Ticket> TicketTAN, List<Ticket> TicketTCN)
		{

			TCN = tCN;
			TAN = tAN;
			this.TicketTAN = TicketTAN;
			this.TicketTCN = TicketTCN;
		}
		public double CalcularIndicadorRANO()
		{
			double RANOGeneral;
			if (TAN != 0)
			{
				RANOGeneral = TCN / TAN * 100;
			}else
			{
				RANOGeneral = 100;
			}
			return Math.Round(RANOGeneral,1);
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("TCN: {0}", TCN);
			sb.AppendFormat(" TAN: {0}", TAN);
			return sb.ToString();
		}
		public List<Ticket> getTAN()
		{
			return TicketTAN;
		}

		public List<Ticket> getTCN()
		{
			return TicketTCN;
		}

	}
}
