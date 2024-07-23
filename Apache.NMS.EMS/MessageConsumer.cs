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
using System.Threading.Tasks;

namespace Apache.NMS.EMS
{
	class MessageConsumer : Apache.NMS.IMessageConsumer
	{
		private readonly Apache.NMS.EMS.Dispatcher dispatcher = new Apache.NMS.EMS.Dispatcher();
		protected readonly Apache.NMS.EMS.Session nmsSession;
		public TIBCO.EMS.MessageConsumer tibcoMessageConsumer;
		private bool closed = false;
		private bool disposed = false;

		public MessageConsumer(Apache.NMS.EMS.Session session, TIBCO.EMS.MessageConsumer consumer)
		{
			this.nmsSession = session;
			this.tibcoMessageConsumer = consumer;
			this.tibcoMessageConsumer.MessageHandler += this.HandleTibcoMsg;
		}

		~MessageConsumer()
		{
			Dispose(false);
		}

		#region IMessageConsumer Members

		private ConsumerTransformerDelegate consumerTransformer;
		public Task CloseAsync()
		{
			return Task.Run(Close);
		}

		/// <summary>
		/// A Delegate that is called each time a Message is dispatched to allow the client to do
		/// any necessary transformations on the received message before it is delivered.
		/// </summary>
		public ConsumerTransformerDelegate ConsumerTransformer
		{
			get { return this.consumerTransformer; }
			set { this.consumerTransformer = value; }
		}

		/// <summary>
		/// Waits until a message is available and returns it
		/// </summary>
		public Apache.NMS.IMessage Receive()
		{
			return this.dispatcher.Dequeue();
		}

		public Task<IMessage> ReceiveAsync()
		{
			return Task.Run(Receive);
		}

		/// <summary>
		/// If a message is available within the timeout duration it is returned otherwise this method returns null
		/// </summary>
		public Apache.NMS.IMessage Receive(TimeSpan timeout)
		{
			return this.dispatcher.Dequeue(timeout);
		}

		public Task<IMessage> ReceiveAsync(TimeSpan timeout)
		{
			return Task.Run(() => Receive(timeout));
		}

		/// <summary>
		/// If a message is available immediately it is returned otherwise this method returns null
		/// </summary>
		public Apache.NMS.IMessage ReceiveNoWait()
		{
			return this.dispatcher.DequeueNoWait();
		}

		public string MessageSelector { get; }

		/// <summary>
		/// An asynchronous listener which can be used to consume messages asynchronously
		/// </summary>
		public event Apache.NMS.MessageListener Listener;

		public event AsyncMessageListener AsyncListener;

		/// <summary>
		/// Closes the message consumer. 
		/// </summary>
		/// <remarks>
		/// Clients should close message consumers them when they are not needed.
		/// This call blocks until a receive or message listener in progress has completed.
		/// A blocked message consumer receive call returns null when this message consumer is closed.
		/// </remarks>
		public void Close()
		{
			lock(this)
			{
				if(closed)
				{
					return;
				}
			}

			// wake up any pending dequeue() call on the dispatcher
			this.dispatcher.Close();

			lock(this)
			{
				try
				{
					if(!this.nmsSession.tibcoSession.IsClosed)
					{
						// JGomes: Calling the following message handler removal code creates an
						// instability in the TIBCO consumer and will fail to unregister a consumer.
						// This is a very odd bug, but it has been observed that unregistering the
						// message handler is not necessary and can be harmful.  When the unregister is
						// executed slowly step-by-step under a debugger, then it works correctly.  When
						// it is run full speed, it creates a race condition.  Therefore, the code is left
						// here in a commented out state to demonstrate what normal cleanup code should
						// look like, but don't uncomment it.

						// this.tibcoMessageConsumer.MessageHandler -= this.HandleTibcoMsg;
						this.tibcoMessageConsumer.Close();
					}
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

		#endregion

		#region IDisposable Members

		///<summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		///<filterpriority>2</filterpriority>
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

		private void HandleTibcoMsg(object sender, TIBCO.EMS.EMSMessageEventArgs arg)
		{
			Apache.NMS.IMessage message = EMSConvert.ToNMSMessage(arg.Message);

			if(null != message)
			{
				if(this.ConsumerTransformer != null)
				{
					IMessage newMessage = ConsumerTransformer(this.nmsSession, this, message);

					if(newMessage != null)
					{
						message = newMessage;
					}
				}

				if(Listener != null)
				{
					try
					{
						Listener(message);
					}
					catch(Exception ex)
					{
						Apache.NMS.Tracer.Debug("Error handling message: " + ex.Message);
					}
				}
				else
				{
					this.dispatcher.Enqueue(message);
				}
			}
		}
	}
}
