/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Threading.Tasks;

namespace Apache.NMS.EMS
{
	public class QueueBrowser : Apache.NMS.IQueueBrowser
	{
		public TIBCO.EMS.QueueBrowser tibcoQueueBrowser;
		private bool closed = false;
		private bool disposed = false;

		public QueueBrowser(TIBCO.EMS.QueueBrowser queueBrowser)
		{
			this.tibcoQueueBrowser = queueBrowser;
		}

		~QueueBrowser()
		{
			Dispose(false);
		}

		#region IDisposable Members

		///<summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if(disposed)
			{
				return;
			}

			if(disposing)
			{
				// Dispose managed code here.
			}

			try
			{
				Close();
			}
			catch
			{
				// Ignore errors.
			}

			disposed = true;
		}

		#endregion

		public void  Close()
		{
			lock(this)
			{
				if(closed)
				{
					return;
				}

				try
				{
					this.tibcoQueueBrowser.Close();
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
				finally
				{
					closed = true;
				}
			}
		}

		public Task CloseAsync()
		{
			return Task.Run(Close);
		}

		public string MessageSelector
		{
			get { return this.tibcoQueueBrowser.MessageSelector; }
		}

		public IQueue Queue
		{
			get { return EMSConvert.ToNMSQueue(this.tibcoQueueBrowser.Queue); }
		}

		internal class Enumerator : IEnumerator
		{
			private IEnumerator innerEnumerator;

			public Enumerator(IEnumerator innerEnumerator)
			{
				this.innerEnumerator = innerEnumerator;
			}

			public object Current
			{
				get
				{
					return EMSConvert.ToNMSMessage((TIBCO.EMS.Message)this.innerEnumerator.Current);
				}
			}

			public bool MoveNext()
			{
				return this.innerEnumerator.MoveNext();
			}

			public void Reset()
			{
				this.innerEnumerator.Reset();
			}
		}

		public IEnumerator GetEnumerator()
		{
			return new Enumerator(this.tibcoQueueBrowser.GetEnumerator());
		}
	}
}
