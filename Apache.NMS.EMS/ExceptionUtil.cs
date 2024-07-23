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
using System.Collections.Generic;
using System.Text;

namespace Apache.NMS.EMS
{
	class ExceptionUtil
	{
		/// <summary>
		/// Wrap the provider specific exception inside an NMS exception to more tightly
		/// integrate the provider extensions into the NMS API.
		/// </summary>
		/// <param name="ex"></param>
		public static void WrapAndThrowNMSException(Exception ex)
		{
			if(ex is Apache.NMS.NMSException)
			{
				// Already derived from NMSException
				throw ex;
			}

			if(ex is TIBCO.EMS.AuthenticationException)
			{
				TIBCO.EMS.AuthenticationException tibcoex = ex as TIBCO.EMS.AuthenticationException;
				throw new Apache.NMS.NMSSecurityException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.CannotProceedException)
			{
				TIBCO.EMS.CannotProceedException tibcoex = ex as TIBCO.EMS.CannotProceedException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.ConfigurationException)
			{
				TIBCO.EMS.ConfigurationException tibcoex = ex as TIBCO.EMS.ConfigurationException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.InvalidNameException)
			{
				TIBCO.EMS.InvalidNameException tibcoex = ex as TIBCO.EMS.InvalidNameException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.NameNotFoundException)
			{
				TIBCO.EMS.NameNotFoundException tibcoex = ex as TIBCO.EMS.NameNotFoundException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.NamingException)
			{
				TIBCO.EMS.NamingException tibcoex = ex as TIBCO.EMS.NamingException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.CommunicationException)
			{
				TIBCO.EMS.CommunicationException tibcoex = ex as TIBCO.EMS.CommunicationException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.ServiceUnavailableException)
			{
				TIBCO.EMS.ServiceUnavailableException tibcoex = ex as TIBCO.EMS.ServiceUnavailableException;
				throw new Apache.NMS.NMSConnectionException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.IllegalStateException)
			{
				TIBCO.EMS.IllegalStateException tibcoex = ex as TIBCO.EMS.IllegalStateException;
				throw new Apache.NMS.IllegalStateException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.InvalidClientIDException)
			{
				TIBCO.EMS.InvalidClientIDException tibcoex = ex as TIBCO.EMS.InvalidClientIDException;
				throw new Apache.NMS.InvalidClientIDException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.InvalidDestinationException)
			{
				TIBCO.EMS.InvalidDestinationException tibcoex = ex as TIBCO.EMS.InvalidDestinationException;
				throw new Apache.NMS.InvalidDestinationException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.InvalidSelectorException)
			{
				TIBCO.EMS.InvalidSelectorException tibcoex = ex as TIBCO.EMS.InvalidSelectorException;
				throw new Apache.NMS.InvalidSelectorException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.MessageEOFException)
			{
				TIBCO.EMS.MessageEOFException tibcoex = ex as TIBCO.EMS.MessageEOFException;
				throw new Apache.NMS.MessageEOFException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.MessageFormatException)
			{
				TIBCO.EMS.MessageFormatException tibcoex = ex as TIBCO.EMS.MessageFormatException;
				throw new Apache.NMS.MessageFormatException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.MessageNotReadableException)
			{
				TIBCO.EMS.MessageNotReadableException tibcoex = ex as TIBCO.EMS.MessageNotReadableException;
				throw new Apache.NMS.MessageNotReadableException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.MessageNotWriteableException)
			{
				TIBCO.EMS.MessageNotWriteableException tibcoex = ex as TIBCO.EMS.MessageNotWriteableException;
				throw new Apache.NMS.MessageNotWriteableException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.ResourceAllocationException)
			{
				TIBCO.EMS.ResourceAllocationException tibcoex = ex as TIBCO.EMS.ResourceAllocationException;
				throw new Apache.NMS.ResourceAllocationException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.SecurityException)
			{
				TIBCO.EMS.SecurityException tibcoex = ex as TIBCO.EMS.SecurityException;
				throw new Apache.NMS.NMSSecurityException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.TransactionInProgressException)
			{
				TIBCO.EMS.TransactionInProgressException tibcoex = ex as TIBCO.EMS.TransactionInProgressException;
				throw new Apache.NMS.TransactionInProgressException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.TransactionRolledBackException)
			{
				TIBCO.EMS.TransactionRolledBackException tibcoex = ex as TIBCO.EMS.TransactionRolledBackException;
				throw new Apache.NMS.TransactionRolledBackException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			if(ex is TIBCO.EMS.EMSException)
			{
				TIBCO.EMS.EMSException tibcoex = ex as TIBCO.EMS.EMSException;
				throw new Apache.NMS.NMSException(tibcoex.Message, tibcoex.ErrorCode, tibcoex);
			}

			// Not an EMS exception that should be wrapped.
			throw ex;
		}
	}
}
