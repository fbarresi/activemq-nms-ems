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
using System.Threading;
using System.Threading.Tasks;

namespace Apache.NMS.EMS
{
	class MessageProducer : Apache.NMS.IMessageProducer
	{
		protected readonly Apache.NMS.EMS.Session nmsSession;
		public TIBCO.EMS.MessageProducer tibcoMessageProducer;
		private TimeSpan requestTimeout = NMSConstants.defaultRequestTimeout;
		private bool closed = false;
		private bool disposed = false;

		public MessageProducer(Apache.NMS.EMS.Session session, TIBCO.EMS.MessageProducer producer)
		{
			this.nmsSession = session;
			this.tibcoMessageProducer = producer;
			this.RequestTimeout = session.RequestTimeout;
		}

		~MessageProducer()
		{
			Dispose(false);
		}

		private Apache.NMS.EMS.Message GetEMSMessage(Apache.NMS.IMessage message)
		{
			Apache.NMS.EMS.Message msg = (Apache.NMS.EMS.Message) message;

			if(this.ProducerTransformer != null)
			{
				IMessage transformed = this.ProducerTransformer(this.nmsSession, this, message);
				if(transformed != null)
				{
					msg = (Apache.NMS.EMS.Message) transformed;
				}
			}

			return msg;
		}

		#region IMessageProducer Members

		/// <summary>
		/// Sends the message to the default destination for this producer
		/// </summary>
		public void Send(Apache.NMS.IMessage message)
		{
			Apache.NMS.EMS.Message msg = GetEMSMessage(message);
			long timeToLive = (long) message.NMSTimeToLive.TotalMilliseconds;

			if(0 == timeToLive)
			{
				timeToLive = this.tibcoMessageProducer.TimeToLive;
			}

			try
			{
				msg.OnSend();
				this.tibcoMessageProducer.Send(
							msg.tibcoMessage,
							this.tibcoMessageProducer.MsgDeliveryMode,
							this.tibcoMessageProducer.Priority,
							timeToLive);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		/// <summary>
		/// Sends the message to the default destination with the explicit QoS configuration
		/// </summary>
		public void Send(Apache.NMS.IMessage message, MsgDeliveryMode deliveryMode, MsgPriority priority, TimeSpan timeToLive)
		{
			Apache.NMS.EMS.Message msg = GetEMSMessage(message);

			try
			{
				this.tibcoMessageProducer.Send(
							msg.tibcoMessage,
							EMSConvert.ToMessageDeliveryMode(deliveryMode),
							(int) priority,
							(long) timeToLive.TotalMilliseconds);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		/// <summary>
		/// Sends the message to the given destination
		/// </summary>
		public void Send(Apache.NMS.IDestination destination, Apache.NMS.IMessage message)
		{
			Apache.NMS.EMS.Destination dest = (Apache.NMS.EMS.Destination) destination;
			Apache.NMS.EMS.Message msg = GetEMSMessage(message);
			long timeToLive = (long) message.NMSTimeToLive.TotalMilliseconds;

			if(0 == timeToLive)
			{
				timeToLive = this.tibcoMessageProducer.TimeToLive;
			}

			try
			{
				this.tibcoMessageProducer.Send(
							dest.tibcoDestination,
							msg.tibcoMessage,
							this.tibcoMessageProducer.MsgDeliveryMode,
							this.tibcoMessageProducer.Priority,
							timeToLive);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		/// <summary>
		/// Sends the message to the given destination with the explicit QoS configuration
		/// </summary>
		public void Send(Apache.NMS.IDestination destination, Apache.NMS.IMessage message,
						MsgDeliveryMode deliveryMode, MsgPriority priority, TimeSpan timeToLive)
		{
			Apache.NMS.EMS.Destination dest = (Apache.NMS.EMS.Destination) destination;
			Apache.NMS.EMS.Message msg = GetEMSMessage(message);

			try
			{
				this.tibcoMessageProducer.Send(
							dest.tibcoDestination,
							msg.tibcoMessage,
							EMSConvert.ToMessageDeliveryMode(deliveryMode),
							(int) priority,
							(long) timeToLive.TotalMilliseconds);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public Task SendAsync(IMessage message)
		{
			return Task.Run(() => Send(message));
		}

		public Task SendAsync(IMessage message, MsgDeliveryMode deliveryMode, MsgPriority priority, TimeSpan timeToLive)
		{
			return Task.Run(() => Send(message, deliveryMode, priority, timeToLive));
		}

		public Task SendAsync(IDestination destination, IMessage message)
		{
			return Task.Run(() => Send(destination, message));
		}

		public Task SendAsync(IDestination destination, IMessage message, MsgDeliveryMode deliveryMode, MsgPriority priority,
			TimeSpan timeToLive)
		{
			return Task.Run(() => Send(destination, message, deliveryMode, priority, timeToLive));
		}

		private ProducerTransformerDelegate producerTransformer;
		public Task<IStreamMessage> CreateStreamMessageAsync()
		{
			return Task.Run(CreateStreamMessage);
		}

		/// <summary>
		/// A delegate that is called each time a Message is sent from this Producer which allows
		/// the application to perform any needed transformations on the Message before it is sent.
		/// </summary>
		public ProducerTransformerDelegate ProducerTransformer
		{
			get { return this.producerTransformer; }
			set { this.producerTransformer = value; }
		}

		public MsgDeliveryMode DeliveryMode
		{
			get { return EMSConvert.ToNMSMsgDeliveryMode(this.tibcoMessageProducer.MsgDeliveryMode); }
			set
			{
				try
				{
					this.tibcoMessageProducer.MsgDeliveryMode = EMSConvert.ToMessageDeliveryMode(value);
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public TimeSpan TimeToLive
		{
			get { return TimeSpan.FromMilliseconds(this.tibcoMessageProducer.TimeToLive); }
			set
			{
				try
				{
					this.tibcoMessageProducer.TimeToLive = (long) value.TotalMilliseconds;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		/// <summary>
		/// The default timeout for network requests.
		/// </summary>
		public TimeSpan RequestTimeout
		{
			get { return requestTimeout; }
			set { this.requestTimeout = value; }
		}

		public MsgPriority Priority
		{
			get { return (MsgPriority) this.tibcoMessageProducer.Priority; }
			set
			{
				try
				{
					this.tibcoMessageProducer.Priority = (int) value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public bool DisableMessageID
		{
			get { return this.tibcoMessageProducer.DisableMessageID; }
			set
			{
				try
				{
					this.tibcoMessageProducer.DisableMessageID = value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public bool DisableMessageTimestamp
		{
			get { return this.tibcoMessageProducer.DisableMessageTimestamp; }
			set
			{
				try
				{
					this.tibcoMessageProducer.DisableMessageTimestamp = value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public TimeSpan DeliveryDelay { get; set; }

		public Task CloseAsync()
		{
			return Task.Run(Close);
		}

		/// <summary>
		/// Creates a new message with an empty body
		/// </summary>
		public Apache.NMS.IMessage CreateMessage()
		{
			return this.nmsSession.CreateMessage();
		}

		public Task<IMessage> CreateMessageAsync()
		{
			return Task.Run(CreateMessage);
		}

		/// <summary>
		/// Creates a new text message with an empty body
		/// </summary>
		public Apache.NMS.ITextMessage CreateTextMessage()
		{
			return this.nmsSession.CreateTextMessage();
		}

		public Task<ITextMessage> CreateTextMessageAsync()
		{
			return Task.Run(CreateTextMessage);
		}

		/// <summary>
		/// Creates a new text message with the given body
		/// </summary>
		public Apache.NMS.ITextMessage CreateTextMessage(string text)
		{
			return this.nmsSession.CreateTextMessage(text);
		}

		public Task<ITextMessage> CreateTextMessageAsync(string text)
		{
			return Task.Run(CreateTextMessage);
		}

		/// <summary>
		/// Creates a new Map message which contains primitive key and value pairs
		/// </summary>
		public Apache.NMS.IMapMessage CreateMapMessage()
		{
			return this.nmsSession.CreateMapMessage();
		}

		public Task<IMapMessage> CreateMapMessageAsync()
		{
			return Task.Run(CreateMapMessage);
		}

		/// <summary>
		/// Creates a new Object message containing the given .NET object as the body
		/// </summary>
		public Apache.NMS.IObjectMessage CreateObjectMessage(object body)
		{
			return this.nmsSession.CreateObjectMessage(body);
		}

		public Task<IObjectMessage> CreateObjectMessageAsync(object body)
		{
			return Task.Run(() => CreateObjectMessage(body));
		}

		/// <summary>
		/// Creates a new binary message
		/// </summary>
		public Apache.NMS.IBytesMessage CreateBytesMessage()
		{
			return this.nmsSession.CreateBytesMessage();
		}

		public Task<IBytesMessage> CreateBytesMessageAsync()
		{
			return Task.Run(CreateBytesMessage);
		}

		/// <summary>
		/// Creates a new binary message with the given body
		/// </summary>
		public Apache.NMS.IBytesMessage CreateBytesMessage(byte[] body)
		{
			return this.nmsSession.CreateBytesMessage(body);
		}

		public Task<IBytesMessage> CreateBytesMessageAsync(byte[] body)
		{
			return Task.Run(() => CreateBytesMessage(body));
		}

		/// <summary>
		/// Creates a new stream message
		/// </summary>
		public Apache.NMS.IStreamMessage CreateStreamMessage()
		{
			return this.nmsSession.CreateStreamMessage();
		}

		#endregion

		#region IDisposable Members

		public void Close()
		{
			lock(this)
			{
				if(closed)
				{
					return;
				}

				try
				{
					if(!this.nmsSession.tibcoSession.IsClosed)
					{
						this.tibcoMessageProducer.Close();
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
		///<summary>
		///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
	}
}
