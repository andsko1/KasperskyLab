using KasperskyLabModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KasperskyLabService
{
	public class ParallelQueueService<T>
	{
		private const int WaitHandleCount = 64;
		public List<QueueState<T>> PushAndPopTest(T[] pushArray, int popCount)
		{
			ParallelQueue<T> queue = new ParallelQueue<T>();
			int countWaiters = pushArray.Count() + popCount;
			ManualResetEvent signal = new ManualResetEvent(false);
			ConcurrentBag<QueueState<T>> concurrentStates = new ConcurrentBag<QueueState<T>>();
			for (int i = 0; i < pushArray.Length; i++)
			{
				int innerIndex = i;
				var task = ParallelPush(pushArray[innerIndex], queue, new AsyncCallback(x =>
				{
					if (EndPush(x))
						concurrentStates.Add(new QueueState<T> { Action = QueueAction.Push, Date = DateTime.Now, Element = pushArray[innerIndex] });
					else
						concurrentStates.Add(new QueueState<T>());
					if (Interlocked.Decrement(ref countWaiters) == 0)
					{
						signal.Set();
					}
				}));
			}
			for (int i = pushArray.Length; i < popCount + pushArray.Length; i++)
			{
				var task = ParallelPop(queue, new AsyncCallback(x =>
				{
					Tuple<T, bool> result = EndPop(x);
					if (result.Item2)
						concurrentStates.Add(new QueueState<T> { Action = QueueAction.Pop, Date = DateTime.Now, Element = result.Item1 });
					else
						concurrentStates.Add(new QueueState<T>());
					if (Interlocked.Decrement(ref countWaiters) == 0)
					{
						signal.Set();
					}
				}));
			}
			signal.WaitOne();
			return concurrentStates.ToList();
		}

		private IAsyncResult ParallelPush(T pushElement, ParallelQueue<T> queue, AsyncCallback asyncCallback)
		{
			return Task.Run(() => 
			{
				Thread.Sleep(3000);
				queue.Push(pushElement);
				return true;
			}).ContinueWith(x => asyncCallback(x));
		}

		private IAsyncResult ParallelPop(ParallelQueue<T> queue, AsyncCallback asyncCallback)
		{
			return Task.Run(() =>
			{
				return queue.Pop(); 
			}).ContinueWith(x => asyncCallback(x));
		}

		private bool EndPush(IAsyncResult asyncResult)
		{
			var objResult = (Task<bool>)asyncResult;
			if (objResult.Status == TaskStatus.RanToCompletion)
				return objResult.Result;
			else
			{
				return false;
			}
		}
		private Tuple<T, bool> EndPop(IAsyncResult asyncResult)
		{
			var objResult = (Task<T>)asyncResult;
			if (objResult.Status == TaskStatus.RanToCompletion)
				return new Tuple<T, bool>(objResult.Result, true);
			else
			{
				return new Tuple<T, bool>(default(T), false);
			}
		}
	}
}
