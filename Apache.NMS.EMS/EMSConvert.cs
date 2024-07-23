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

namespace Apache.NMS.EMS
{
	public class EMSConvert
	{
		public static Apache.NMS.IConnection ToNMSConnection(TIBCO.EMS.Connection tibcoConnection)
		{
			return (null != tibcoConnection
							? new Apache.NMS.EMS.Connection(tibcoConnection)
							: null);
		}

		public static Apache.NMS.ISession ToNMSSession(TIBCO.EMS.Session tibcoSession)
		{
			return (null != tibcoSession
							? new Apache.NMS.EMS.Session(tibcoSession)
							: null);
		}

		public static Apache.NMS.IMessageProducer ToNMSMessageProducer(Apache.NMS.EMS.Session session,
					TIBCO.EMS.MessageProducer tibcoMessageProducer)
		{
			return (null != tibcoMessageProducer
							? new Apache.NMS.EMS.MessageProducer(session, tibcoMessageProducer)
							: null);
		}

		public static Apache.NMS.IMessageConsumer ToNMSMessageConsumer(Apache.NMS.EMS.Session session,
					TIBCO.EMS.MessageConsumer tibcoMessageConsumer)
		{
			return (null != tibcoMessageConsumer
							? new Apache.NMS.EMS.MessageConsumer(session, tibcoMessageConsumer)
							: null);
		}

		public static Apache.NMS.IQueueBrowser ToNMSQueueBrowser(TIBCO.EMS.QueueBrowser tibcoQueueBrowser)
		{
			return (null != tibcoQueueBrowser
							? new Apache.NMS.EMS.QueueBrowser(tibcoQueueBrowser)
							: null);
		}

		public static Apache.NMS.IQueue ToNMSQueue(TIBCO.EMS.Queue tibcoQueue)
		{
			return (null != tibcoQueue
							? new Apache.NMS.EMS.Queue(tibcoQueue)
							: null);
		}
		
		public static Apache.NMS.ITopic ToNMSTopic(TIBCO.EMS.Topic tibcoTopic)
		{
			return (null != tibcoTopic
							? new Apache.NMS.EMS.Topic(tibcoTopic)
							: null);
		}

		public static Apache.NMS.ITemporaryQueue ToNMSTemporaryQueue(
				TIBCO.EMS.TemporaryQueue tibcoTemporaryQueue)
		{
			return (null != tibcoTemporaryQueue
							? new Apache.NMS.EMS.TemporaryQueue(tibcoTemporaryQueue)
							: null);
		}

		public static Apache.NMS.ITemporaryTopic ToNMSTemporaryTopic(
				TIBCO.EMS.TemporaryTopic tibcoTemporaryTopic)
		{
			return (null != tibcoTemporaryTopic
							? new Apache.NMS.EMS.TemporaryTopic(tibcoTemporaryTopic)
							: null);
		}

		public static Apache.NMS.IDestination ToNMSDestination(TIBCO.EMS.Destination tibcoDestination)
		{
			if(tibcoDestination is TIBCO.EMS.Queue)
			{
				return ToNMSQueue((TIBCO.EMS.Queue) tibcoDestination);
			}

			if(tibcoDestination is TIBCO.EMS.Topic)
			{
				return ToNMSTopic((TIBCO.EMS.Topic) tibcoDestination);
			}

			if(tibcoDestination is TIBCO.EMS.TemporaryQueue)
			{
				return ToNMSTemporaryQueue((TIBCO.EMS.TemporaryQueue) tibcoDestination);
			}

			if(tibcoDestination is TIBCO.EMS.TemporaryTopic)
			{
				return ToNMSTemporaryTopic((TIBCO.EMS.TemporaryTopic) tibcoDestination);
			}

			return null;
		}

		public static TIBCO.EMS.Destination ToEMSDestination(Apache.NMS.IDestination nmsDestination)
		{
			if(nmsDestination is Apache.NMS.EMS.Destination)
			{
				return ((Apache.NMS.EMS.Destination) nmsDestination).tibcoDestination;
			}

			return null;
		}

		public static Apache.NMS.IMessage ToNMSMessage(TIBCO.EMS.Message tibcoMessage)
		{
			if(tibcoMessage is TIBCO.EMS.TextMessage)
			{
				return ToNMSTextMessage(tibcoMessage as TIBCO.EMS.TextMessage);
			}

			if(tibcoMessage is TIBCO.EMS.BytesMessage)
			{
				return ToNMSBytesMessage(tibcoMessage as TIBCO.EMS.BytesMessage);
			}

			if(tibcoMessage is TIBCO.EMS.StreamMessage)
			{
				return ToNMSStreamMessage(tibcoMessage as TIBCO.EMS.StreamMessage);
			}
			
			if(tibcoMessage is TIBCO.EMS.MapMessage)
			{
				return ToNMSMapMessage(tibcoMessage as TIBCO.EMS.MapMessage);
			}

			if(tibcoMessage is TIBCO.EMS.ObjectMessage)
			{
				return ToNMSObjectMessage(tibcoMessage as TIBCO.EMS.ObjectMessage);
			}

			return (null != tibcoMessage
							? new Apache.NMS.EMS.Message(tibcoMessage)
							: null);
		}

		public static Apache.NMS.ITextMessage ToNMSTextMessage(TIBCO.EMS.TextMessage tibcoTextMessage)
		{
			return (null != tibcoTextMessage
							? new Apache.NMS.EMS.TextMessage(tibcoTextMessage)
							: null);
		}

		public static Apache.NMS.IBytesMessage ToNMSBytesMessage(
				TIBCO.EMS.BytesMessage tibcoBytesMessage)
		{
			return (null != tibcoBytesMessage
							? new Apache.NMS.EMS.BytesMessage(tibcoBytesMessage)
							: null);
		}

		public static Apache.NMS.IStreamMessage ToNMSStreamMessage(TIBCO.EMS.StreamMessage tibcoStreamMessage)
		{
			return (null != tibcoStreamMessage
							? new Apache.NMS.EMS.StreamMessage(tibcoStreamMessage)
							: null);
		}

		public static Apache.NMS.IMapMessage ToNMSMapMessage(TIBCO.EMS.MapMessage tibcoMapMessage)
		{
			return (null != tibcoMapMessage
							? new Apache.NMS.EMS.MapMessage(tibcoMapMessage)
							: null);
		}

		public static Apache.NMS.IObjectMessage ToNMSObjectMessage(
				TIBCO.EMS.ObjectMessage tibcoObjectMessage)
		{
			return (null != tibcoObjectMessage
							? new Apache.NMS.EMS.ObjectMessage(tibcoObjectMessage)
							: null);
		}

		public static TIBCO.EMS.SessionMode ToSessionMode(Apache.NMS.AcknowledgementMode acknowledge)
		{
			TIBCO.EMS.SessionMode sessionMode = TIBCO.EMS.SessionMode.NoAcknowledge;

			switch(acknowledge)
			{
			case Apache.NMS.AcknowledgementMode.AutoAcknowledge:
				sessionMode = TIBCO.EMS.SessionMode.AutoAcknowledge;
				break;

			case Apache.NMS.AcknowledgementMode.ClientAcknowledge:
				sessionMode = TIBCO.EMS.SessionMode.ClientAcknowledge;
				break;

			case Apache.NMS.AcknowledgementMode.DupsOkAcknowledge:
				sessionMode = TIBCO.EMS.SessionMode.DupsOkAcknowledge;
				break;

			case Apache.NMS.AcknowledgementMode.Transactional:
				sessionMode = TIBCO.EMS.SessionMode.SessionTransacted;
				break;
			}

			return sessionMode;
		}

		public static Apache.NMS.AcknowledgementMode ToAcknowledgementMode(
				TIBCO.EMS.SessionMode sessionMode)
		{
			Apache.NMS.AcknowledgementMode acknowledge = Apache.NMS.AcknowledgementMode.AutoAcknowledge;

			switch(sessionMode)
			{
			case TIBCO.EMS.SessionMode.AutoAcknowledge:
				acknowledge = Apache.NMS.AcknowledgementMode.AutoAcknowledge;
				break;

			case TIBCO.EMS.SessionMode.ClientAcknowledge:
				acknowledge = Apache.NMS.AcknowledgementMode.ClientAcknowledge;
				break;

			case TIBCO.EMS.SessionMode.DupsOkAcknowledge:
				acknowledge = Apache.NMS.AcknowledgementMode.DupsOkAcknowledge;
				break;

			case TIBCO.EMS.SessionMode.SessionTransacted:
				acknowledge = Apache.NMS.AcknowledgementMode.Transactional;
				break;
			}

			return acknowledge;
		}

		public static Apache.NMS.IPrimitiveMap ToMessageProperties(TIBCO.EMS.Message tibcoMessage)
		{
			return (null != tibcoMessage
							? new Apache.NMS.EMS.MessageProperties(tibcoMessage)
							: null);
		}

		public static TIBCO.EMS.MessageDeliveryMode ToMessageDeliveryMode(MsgDeliveryMode deliveryMode)
		{
			if(MsgDeliveryMode.Persistent == deliveryMode)
			{
				return TIBCO.EMS.MessageDeliveryMode.Persistent;
			}
			
			if(MsgDeliveryMode.NonPersistent == deliveryMode)
			{
				return TIBCO.EMS.MessageDeliveryMode.NonPersistent;
			}

			// Hard cast it to the enumeration.
			return (TIBCO.EMS.MessageDeliveryMode) deliveryMode;
		}

		public static MsgDeliveryMode ToNMSMsgDeliveryMode(TIBCO.EMS.MessageDeliveryMode deliveryMode)
		{
			if(TIBCO.EMS.MessageDeliveryMode.Persistent == deliveryMode)
			{
				return MsgDeliveryMode.Persistent;
			}

			if(TIBCO.EMS.MessageDeliveryMode.NonPersistent == deliveryMode)
			{
				return MsgDeliveryMode.NonPersistent;
			}

			// Hard cast it to the enumeration.
			return (MsgDeliveryMode) deliveryMode;
		}


		#region Enumerable adapter

		private class EnumerableAdapter : IEnumerable
		{
			private readonly IEnumerator enumerator;
			public EnumerableAdapter(IEnumerator _enumerator)
			{
				this.enumerator = _enumerator;
			}

			public IEnumerator GetEnumerator()
			{
				return this.enumerator;
			}
		}

		public static IEnumerable ToEnumerable(IEnumerator enumerator)
		{
			return new EnumerableAdapter(enumerator);
		}

		#endregion
	}
}
