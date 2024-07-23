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

using System.Threading.Tasks;

namespace Apache.NMS.EMS
{
	class TemporaryQueue : Apache.NMS.EMS.Queue, Apache.NMS.ITemporaryQueue
	{
		public TIBCO.EMS.TemporaryQueue tibcoTemporaryQueue
		{
			get { return this.tibcoDestination as TIBCO.EMS.TemporaryQueue; }
			set { this.tibcoDestination = value; }
		}

		public TemporaryQueue(TIBCO.EMS.TemporaryQueue temporaryQueue)
			: base(temporaryQueue)
		{
		}

		#region IDestination Members

		public new Apache.NMS.DestinationType DestinationType
		{
			get { return Apache.NMS.DestinationType.TemporaryQueue; }
		}

		public new bool IsTopic
		{
			get { return false; }
		}

		public new bool IsQueue
		{
			get { return true; }
		}

		public new bool IsTemporary
		{
			get { return true; }
		}

		#endregion

		#region ITemporaryQueue Members

		public void Delete()
		{
			this.tibcoTemporaryQueue.Delete();
		}

		public Task DeleteAsync()
		{
			return Task.Run(Delete);
		}

		#endregion

		/// <summary>
		/// </summary>
		/// <returns>string representation of this instance</returns>
		public override System.String ToString()
		{
			return "temp-queue://" + QueueName;
		}
	}
}
