using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasperskyLabModels
{
	public class QueueState<T>
	{
		public QueueAction Action { get; set; }

		public T Element { get; set; }

		public DateTime Date { get; set; }

		public int Id { get; set; }
	}
}
