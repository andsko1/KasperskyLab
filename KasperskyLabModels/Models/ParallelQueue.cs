using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KasperskyLabModels
{
	public class ParallelQueue<T>
	{
		private Queue<T> queue;
		Semaphore sem = new Semaphore(0, Int32.MaxValue);

		public ParallelQueue()
		{
				queue = new Queue<T>();
		}
		public T Pop()
		{
			sem.WaitOne();
			lock (queue)
			{
				return queue.Dequeue();
			}
		}

		public void Push(T element)
		{
			lock (queue)
			{
				queue.Enqueue(element);
			}
			sem.Release();
		}
	}
}
