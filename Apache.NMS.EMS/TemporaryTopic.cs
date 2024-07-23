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
	class TemporaryTopic : Apache.NMS.EMS.Topic, Apache.NMS.ITemporaryTopic
	{
		public TIBCO.EMS.TemporaryTopic tibcoTemporaryTopic
		{
			get { return this.tibcoDestination as TIBCO.EMS.TemporaryTopic; }
			set { this.tibcoDestination = value; }
		}

		public TemporaryTopic(TIBCO.EMS.TemporaryTopic temporaryTopic)
			: base(temporaryTopic)
		{
		}

        #region IDestination Members

		public new Apache.NMS.DestinationType DestinationType
		{
			get { return Apache.NMS.DestinationType.TemporaryTopic; }
		}

		public new bool IsTopic
		{
			get { return true; }
		}

		public new bool IsQueue
		{
			get { return false; }
		}

		public new bool IsTemporary
		{
			get { return true; }
		}

		#endregion

		#region ITemporaryTopic Members

		public void Delete()
		{
			this.tibcoTemporaryTopic.Delete();
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
			return "temp-topic://" + TopicName;
		}
	}
}
