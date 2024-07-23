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
using System.Collections.Specialized;
using System.Threading.Tasks;
using Apache.NMS.EMS.Util;
using Apache.NMS.Policies;
using Apache.NMS.Util;

namespace Apache.NMS.EMS
{
	/// <summary>
	/// A Factory that can estbalish NMS connections to TIBCO
	/// </summary>
	public class ConnectionFactory : Apache.NMS.IConnectionFactory
	{
		public TIBCO.EMS.ConnectionFactory tibcoConnectionFactory;
		private Uri brokerUri;
		private string clientId;
		private Hashtable properties;
		private bool exceptionOnFTEvents = true;
		private bool exceptionOnFTSwitch = true;
		private int connAttemptCount = Int32.MaxValue;   // Infinite
		private int connAttemptDelay = 30000;            // 30 seconds
		private int connAttemptTimeout = 5000;           // 5 seconds
		private int reconnAttemptCount = Int32.MaxValue; // Infinite
		private int reconnAttemptDelay = 30000;          // 30 seconds
		private int reconnAttemptTimeout = 5000;         // 5 seconds

		private IRedeliveryPolicy redeliveryPolicy = new RedeliveryPolicy();

		public ConnectionFactory()
		{
			try
			{
				this.tibcoConnectionFactory = new TIBCO.EMS.ConnectionFactory();
				ConfigureConnectionFactory();
			}
			catch(Exception ex)
			{
				Apache.NMS.Tracer.DebugFormat("Exception instantiating TIBCO.EMS.ConnectionFactory: {0}", ex.Message);
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}

			VerifyConnectionFactory();
		}

		public ConnectionFactory(Uri serverUrl)
			: this(serverUrl, string.Empty)
		{
		}

		public ConnectionFactory(Uri serverUrl, string clientId)
			: this(serverUrl, clientId, null)
		{
		}

		public ConnectionFactory(string serverUrl)
			: this(new Uri(serverUrl))
		{
		}

		public ConnectionFactory(string serverUrl, string clientId)
			: this(new Uri(serverUrl), clientId)
		{
		}

		public ConnectionFactory(string serverUrl, string clientId, Hashtable properties)
			: this(new Uri(serverUrl), clientId, properties)
		{
		}

		public ConnectionFactory(Uri serverUrl, string clientId, Hashtable properties)
		{
			try
			{
				this.clientId = clientId;
				this.properties = properties;
				this.BrokerUri = serverUrl;
			}
			catch(Exception ex)
			{
				Apache.NMS.Tracer.DebugFormat("Exception instantiating TIBCO.EMS.ConnectionFactory: {0}", ex.Message);
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}

			VerifyConnectionFactory();
		}

		private void ConfigureConnectionFactory()
		{
			TIBCO.EMS.Tibems.SetExceptionOnFTEvents(this.ExceptionOnFTEvents);
			TIBCO.EMS.Tibems.SetExceptionOnFTSwitch(this.ExceptionOnFTSwitch);

			// Set the initial connection retry settings.
			this.tibcoConnectionFactory.SetConnAttemptCount(this.ConnAttemptCount);
			this.tibcoConnectionFactory.SetConnAttemptDelay(this.ConnAttemptDelay);
			this.tibcoConnectionFactory.SetConnAttemptTimeout(this.ConnAttemptTimeout);
			
			// Set the failover reconnect retry settings
			this.tibcoConnectionFactory.SetReconnAttemptCount(this.ReconnAttemptCount);
			this.tibcoConnectionFactory.SetReconnAttemptDelay(this.ReconnAttemptDelay);
			this.tibcoConnectionFactory.SetReconnAttemptTimeout(this.ReconnAttemptTimeout);
		}

		private void VerifyConnectionFactory()
		{
			if(null == this.tibcoConnectionFactory)
			{
				throw new Apache.NMS.NMSException("Error instantiating TIBCO connection factory object.");
			}
		}

		#region Connection Factory Properties (configure via URL parameters)

		public bool ExceptionOnFTEvents
		{
			get { return this.exceptionOnFTEvents; }
			set
			{
				this.exceptionOnFTEvents = value;
				TIBCO.EMS.Tibems.SetExceptionOnFTEvents(value);
			}
		}

		public bool ExceptionOnFTSwitch
		{
			get { return this.exceptionOnFTSwitch; }
			set
			{
				this.exceptionOnFTSwitch = value;
				TIBCO.EMS.Tibems.SetExceptionOnFTSwitch(value);
			}
		}

		public int ConnAttemptCount
		{
			get { return this.connAttemptCount; }
			set { this.connAttemptCount = value; }
		}

		public int ConnAttemptDelay
		{
			get { return this.connAttemptDelay; }
			set { this.connAttemptDelay = value; }
		}

		public int ConnAttemptTimeout
		{
			get { return this.connAttemptTimeout; }
			set { this.connAttemptTimeout = value; }
		}

		public int ReconnAttemptCount
		{
			get { return this.reconnAttemptCount; }
			set { this.reconnAttemptCount = value; }
		}

		public int ReconnAttemptDelay
		{
			get { return this.reconnAttemptDelay; }
			set { this.reconnAttemptDelay = value; }
		}

		public int ReconnAttemptTimeout
		{
			get { return this.reconnAttemptTimeout; }
			set { this.reconnAttemptTimeout = value; }
		}

		#endregion

		#region IConnectionFactory Members

		/// <summary>
		/// Creates a new connection to TIBCO.
		/// </summary>
		public Apache.NMS.IConnection CreateConnection()
		{
			Apache.NMS.IConnection connection = null;

			try
			{
				connection = EMSConvert.ToNMSConnection(this.tibcoConnectionFactory.CreateConnection());
				ConfigureConnection(connection);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}

			return connection;
		}

		/// <summary>
		/// Creates a new connection to TIBCO.
		/// </summary>
		public Apache.NMS.IConnection CreateConnection(string userName, string password)
		{
			Apache.NMS.IConnection connection = null;

			try
			{
				var tibcoConnection = this.tibcoConnectionFactory.CreateConnection(userName, password);
				connection = EMSConvert.ToNMSConnection(tibcoConnection);
				ConfigureConnection(connection);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}

			return connection;
		}

		public Task<IConnection> CreateConnectionAsync()
		{
			return Task.Run(CreateConnection);
		}

		public Task<IConnection> CreateConnectionAsync(string userName, string password)
		{
			return Task.Run(() => CreateConnection(userName, password));
		}

		public INMSContext CreateContext()
		{
			throw new NotImplementedException();
		}

		public INMSContext CreateContext(AcknowledgementMode acknowledgementMode)
		{
			throw new NotImplementedException();
		}

		public INMSContext CreateContext(string userName, string password)
		{
			throw new NotImplementedException();
		}

		public INMSContext CreateContext(string userName, string password, AcknowledgementMode acknowledgementMode)
		{
			throw new NotImplementedException();
		}

		public Task<INMSContext> CreateContextAsync()
		{
			throw new NotImplementedException();
		}

		public Task<INMSContext> CreateContextAsync(AcknowledgementMode acknowledgementMode)
		{
			throw new NotImplementedException();
		}

		public Task<INMSContext> CreateContextAsync(string userName, string password)
		{
			throw new NotImplementedException();
		}

		public Task<INMSContext> CreateContextAsync(string userName, string password, AcknowledgementMode acknowledgementMode)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Configure the newly created connection.
		/// </summary>
		/// <param name="connection"></param>
		private void ConfigureConnection(IConnection connection)
		{
			connection.RedeliveryPolicy = this.redeliveryPolicy.Clone() as IRedeliveryPolicy;
			connection.ConsumerTransformer = this.consumerTransformer;
			connection.ProducerTransformer = this.producerTransformer;
		}

		/// <summary>
		/// Get/or set the broker Uri.
		/// </summary>
		public Uri BrokerUri
		{
			get { return this.brokerUri; }
			set
			{
				try
				{
					// Create or Re-create the TIBCO connection factory.
					this.brokerUri = ParseUriProperties(value);
					if(null == this.brokerUri)
					{
						this.tibcoConnectionFactory = new TIBCO.EMS.ConnectionFactory();
					}
					else
					{
						string brokerPath = TrimEms(TrimParens(this.brokerUri.AbsoluteUri));

						if(null == this.clientId)
						{
							this.tibcoConnectionFactory = new TIBCO.EMS.ConnectionFactory(brokerPath);
						}
						else
						{
							if(null == this.properties)
							{
								this.tibcoConnectionFactory = new TIBCO.EMS.ConnectionFactory(brokerPath, this.clientId);
							}
							else
							{
								this.tibcoConnectionFactory = new TIBCO.EMS.ConnectionFactory(brokerPath, this.clientId, this.properties);
							}
						}
					}

					ConfigureConnectionFactory();
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		private Uri ParseUriProperties(Uri rawUri)
		{
			Tracer.InfoFormat("BrokerUri set = {0}", rawUri.OriginalString);
			Uri parsedUri = rawUri;

			if(!String.IsNullOrEmpty(rawUri.Query) && !rawUri.OriginalString.EndsWith(")"))
			{
				parsedUri = new Uri(rawUri.OriginalString);
				// Since the Uri class will return the end of a Query string found in a Composite
				// URI we must ensure that we trim that off before we proceed.
				string query = parsedUri.Query.Substring(parsedUri.Query.LastIndexOf(")") + 1);

				StringDictionary properties = URISupport.ParseQuery(query);

				StringDictionary connection = URISupport.ExtractProperties(properties, "connection.");
				StringDictionary nms = URISupport.ExtractProperties(properties, "nms.");

				IntrospectionSupport.SetProperties(this, connection, "connection.");
				IntrospectionSupport.SetProperties(this, nms, "nms.");

				parsedUri = URISupport.CreateRemainingUri(parsedUri, properties);
			}

			return parsedUri;
		}

		private string TrimParens(string stringWithParens)
		{
			return stringWithParens.TrimStart('(').TrimEnd(')').TrimEnd('/').Replace("ems:", "");
		}
		private string TrimEms(string stringWithParens)
		{
			var addr = stringWithParens.TrimEnd('/');
				if(addr.StartsWith("ems:"))
					addr = addr.Substring("ems:".Length);
				return addr;
		}

		/// <summary>
		/// Get/or set the redelivery policy that new IConnection objects are
		/// assigned upon creation.
		/// </summary>
		public IRedeliveryPolicy RedeliveryPolicy
		{
			get { return this.redeliveryPolicy; }
			set
			{
				if(value != null)
				{
					this.redeliveryPolicy = value;
				}
			}
		}

		private ConsumerTransformerDelegate consumerTransformer;
		/// <summary>
		/// A Delegate that is called each time a Message is dispatched to allow the client to do
		/// any necessary transformations on the received message before it is delivered.  The
		/// ConnectionFactory sets the provided delegate instance on each Connection instance that
		/// is created from this factory, each connection in turn passes the delegate along to each
		/// Session it creates which then passes that along to the Consumers it creates.
		/// </summary>
		public ConsumerTransformerDelegate ConsumerTransformer
		{
			get { return this.consumerTransformer; }
			set { this.consumerTransformer = value; }
		}

		private ProducerTransformerDelegate producerTransformer;
		/// <summary>
		/// A delegate that is called each time a Message is sent from this Producer which allows
		/// the application to perform any needed transformations on the Message before it is sent.
		/// The ConnectionFactory sets the provided delegate instance on each Connection instance that
		/// is created from this factory, each connection in turn passes the delegate along to each
		/// Session it creates which then passes that along to the Producers it creates.
		/// </summary>
		public ProducerTransformerDelegate ProducerTransformer
		{
			get { return this.producerTransformer; }
			set { this.producerTransformer = value; }
		}

		#endregion
	}
}
