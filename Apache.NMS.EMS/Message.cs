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
using Apache.NMS.Util;

namespace Apache.NMS.EMS
{
	class Message : Apache.NMS.IMessage
	{
		private Apache.NMS.IPrimitiveMap properties = null;
		private Apache.NMS.Util.MessagePropertyIntercepter propertyHelper;
		private bool readOnlyMsgProperties = false;
		private bool readOnlyMsgBody = false;

		public TIBCO.EMS.Message tibcoMessage;

		public Message(TIBCO.EMS.Message message)
		{
			this.tibcoMessage = message;
		}

		#region IMessage Members

		/// <summary>
		/// If using client acknowledgement mode on the session then this method will acknowledge that the
		/// message has been processed correctly.
		/// </summary>
		public void Acknowledge()
		{
			try
			{
				this.tibcoMessage.Acknowledge();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public Task AcknowledgeAsync()
		{
			return Task.Run(Acknowledge);
		}

		/// <summary>
		/// Clears out the message body. Clearing a message's body does not clear its header
		/// values or property entries.
		///
		/// If this message body was read-only, calling this method leaves the message body in
		/// the same state as an empty body in a newly created message.
		/// </summary>
		public void ClearBody()
		{
			try
			{
				this.ReadOnlyBody = false;
				this.tibcoMessage.ClearBody();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		/// <summary>
		/// Clears a message's properties.
		///
		/// The message's header fields and body are not cleared.
		/// </summary>
		public void ClearProperties()
		{
			try
			{
				this.ReadOnlyProperties = false;
				this.tibcoMessage.ClearProperties();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public T Body<T>()
		{
			throw new NotImplementedException();
		}

		public bool IsBodyAssignableTo(Type type)
		{
			throw new NotImplementedException();
		}

		public virtual bool ReadOnlyBody
		{
			get { return this.readOnlyMsgBody; }
			set { this.readOnlyMsgBody = value; }
		}

		public virtual bool ReadOnlyProperties
		{
			get { return this.readOnlyMsgProperties; }

			set
			{
				if(this.propertyHelper != null)
				{
					this.propertyHelper.ReadOnly = value;
				}
				this.readOnlyMsgProperties = value;
			}
		}

		/// <summary>
		/// Provides access to the message properties (headers)
		/// </summary>
		public Apache.NMS.IPrimitiveMap Properties
		{
			get
			{
				if(null == properties)
				{
					properties = EMSConvert.ToMessageProperties(this.tibcoMessage);
					propertyHelper = new Apache.NMS.Util.MessagePropertyIntercepter(this, properties, this.ReadOnlyProperties) { AllowByteArrays = false };

					// Since JMS doesn't define a Byte array interface for properties we
					// disable them here to prevent sending invalid data to the broker.
				}

				return propertyHelper;
			}
		}

		/// <summary>
		/// The correlation ID used to correlate messages from conversations or long running business processes
		/// </summary>
		public string NMSCorrelationID
		{
			get
			{
				try
				{
					return this.tibcoMessage.CorrelationID;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
					return null;
				}
			}
			set
			{
				try
				{
					this.tibcoMessage.CorrelationID = value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		/// <summary>
		/// The destination of the message
		/// </summary>
		public Apache.NMS.IDestination NMSDestination
		{
			get { return EMSConvert.ToNMSDestination(this.tibcoMessage.Destination); }
			set
			{
				try
				{
					this.tibcoMessage.Destination = EMSConvert.ToEMSDestination(value);
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		protected TimeSpan timeToLive;

		/// <summary>
		/// The amount of time that this message is valid for.  null If this
		/// message does not expire.
		/// </summary>
		public TimeSpan NMSTimeToLive
		{
			get { return this.timeToLive; }
			set { this.timeToLive = value; }
		}

		/// <summary>
		/// The message ID which is set by the provider
		/// </summary>
		public string NMSMessageId
		{
			get { return this.tibcoMessage.MessageID; }
			set { this.tibcoMessage.MessageID = value; }
		}

		/// <summary>
		/// Whether or not this message is persistent
		/// </summary>
		public MsgDeliveryMode NMSDeliveryMode
		{
			get { return EMSConvert.ToNMSMsgDeliveryMode(this.tibcoMessage.MsgDeliveryMode); }
			set
			{
				try
				{
					this.tibcoMessage.MsgDeliveryMode = EMSConvert.ToMessageDeliveryMode(value);
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		/// <summary>
		/// The Priority on this message
		/// </summary>
		public MsgPriority NMSPriority
		{
			get { return (MsgPriority) this.tibcoMessage.Priority; }
			set
			{
				try
				{
					this.tibcoMessage.Priority = (int) value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		/// <summary>
		/// Returns true if this message has been redelivered to this or another consumer before being acknowledged successfully.
		/// </summary>
		public bool NMSRedelivered
		{
			get { return this.tibcoMessage.Redelivered; }
			set { this.tibcoMessage.Redelivered = value; }
		}

		/// <summary>
		/// The destination that the consumer of this message should send replies to
		/// </summary>
		public Apache.NMS.IDestination NMSReplyTo
		{
			get { return EMSConvert.ToNMSDestination(this.tibcoMessage.ReplyTo); }
			set
			{
				try
				{
					this.tibcoMessage.ReplyTo = EMSConvert.ToEMSDestination(value);
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		/// <summary>
		/// The timestamp of when the message was pubished in UTC time.  If the publisher disables setting 
		/// the timestamp on the message, the time will be set to the start of the UNIX epoch (1970-01-01 00:00:00).
		/// </summary>
		public DateTime NMSTimestamp
		{
			get { return DateUtils.ToDateTime(this.tibcoMessage.Timestamp); }
			set { this.tibcoMessage.Timestamp = DateUtils.ToJavaTime(value); }
		}

		/// <summary>
		/// The type name of this message
		/// </summary>
		public string NMSType
		{
			get { return this.tibcoMessage.MsgType; }
			set
			{
				try
				{
					this.tibcoMessage.MsgType = value;
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public DateTime NMSDeliveryTime { get; set; }

		#endregion

		public virtual void OnSend()
		{
			this.ReadOnlyProperties = true;
			this.ReadOnlyBody = true;
		}

		public virtual void OnMessageRollback()
		{
		}
	}
}
