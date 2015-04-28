using System;
using System.Threading;

namespace ENTech.Store.Infrastructure.Cache
{
	public class LockRetryPolicy
	{
		private const int RetryCount = 5;
		private const int ThreadDefaultDelay = 20;

		public void Handle(Action function)
		{
			var counter = RetryCount;
			while (counter-- > 0)
			{
				try
				{
					function.Invoke();
					return;
				}
				catch // (DataCacheException ex)
				{
					if (counter == 0)
						throw;

					Thread.Sleep(ThreadDefaultDelay);
				}
			}
		}
	}
}