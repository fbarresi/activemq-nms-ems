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
	class MapMessage : Apache.NMS.EMS.Message, Apache.NMS.IMapMessage, Apache.NMS.IPrimitiveMap
	{
		public TIBCO.EMS.MapMessage tibcoMapMessage
		{
			get { return this.tibcoMessage as TIBCO.EMS.MapMessage; }
			set { this.tibcoMessage = value; }
		}

		public MapMessage(TIBCO.EMS.MapMessage message)
			: base(message)
		{
		}

		#region IMapMessage Members

		public Apache.NMS.IPrimitiveMap Body
		{
			get { return this; }
		}

		#endregion

		#region IPrimitiveMap Members

		public void Clear()
		{
			try
			{
				this.ReadOnlyBody = false;
				this.tibcoMapMessage.ClearBody();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public bool Contains(object key)
		{
			try
			{
				return this.tibcoMapMessage.ItemExists(key.ToString());
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return false;
			}
		}

		public void Remove(object key)
		{
			try
			{
				// Best guess at equivalent implementation.
				this.tibcoMapMessage.SetObject(key.ToString(), null);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public int Count
		{
			get
			{
				int count = 0;

				try
				{
					ICollection mapNames = this.tibcoMapMessage.GetMapNames();

					if(null != mapNames)
					{
						count = mapNames.Count;
					}
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}

				return count;
			}
		}

		public ICollection Keys
		{
			get
			{
				ArrayList keys = new ArrayList();

				try
				{
					foreach(string itemName in this.tibcoMapMessage.GetMapNames())
					{
						keys.Add(itemName);
					}
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}

				return keys;
			}
		}

		public ICollection Values
		{
			get
			{
				ArrayList keys = new ArrayList();

				try
				{
					foreach(string itemName in this.tibcoMapMessage.GetMapNames())
					{
						keys.Add(this.tibcoMapMessage.GetObject(itemName));
					}
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}

				return keys;
			}
		}

		public object this[string key]
		{
			get
			{
				try
				{
					return this.tibcoMapMessage.GetObject(key);
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
					this.tibcoMapMessage.SetObject(key, value);
				}
				catch(Exception ex)
				{
					ExceptionUtil.WrapAndThrowNMSException(ex);
				}
			}
		}

		public string GetString(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetString(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return null;
			}
		}

		public void SetString(string key, string value)
		{
			try
			{
				this.tibcoMapMessage.SetString(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public bool GetBool(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetBoolean(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return false;
			}
		}

		public void SetBool(string key, bool value)
		{
			try
			{
				this.tibcoMapMessage.SetBoolean(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public byte GetByte(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetByte(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetByte(string key, byte value)
		{
			try
			{
				this.tibcoMapMessage.SetByte(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public char GetChar(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetChar(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return (char) 0;
			}
		}

		public void SetChar(string key, char value)
		{
			try
			{
				this.tibcoMapMessage.SetChar(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public short GetShort(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetShort(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetShort(string key, short value)
		{
			try
			{
				this.tibcoMapMessage.SetShort(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public int GetInt(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetInt(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetInt(string key, int value)
		{
			try
			{
				this.tibcoMapMessage.SetInt(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public long GetLong(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetLong(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetLong(string key, long value)
		{
			try
			{
				this.tibcoMapMessage.SetLong(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public float GetFloat(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetFloat(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetFloat(string key, float value)
		{
			try
			{
				this.tibcoMapMessage.SetFloat(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public double GetDouble(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetDouble(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public void SetDouble(string key, double value)
		{
			try
			{
				this.tibcoMapMessage.SetDouble(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public IList GetList(string key)
		{
			try
			{
				return (IList) this.tibcoMapMessage.GetObject(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return null;
			}
		}

		public void SetList(string key, IList list)
		{
			try
			{
				this.tibcoMapMessage.SetObject(key, list);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void SetBytes(string key, byte[] value)
		{
			try
			{
				this.tibcoMapMessage.SetBytes(key, value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void SetBytes(string key, byte[] value, int offset, int length)
		{
			try
			{
				this.tibcoMapMessage.SetBytes(key, value, offset, length);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public byte[] GetBytes(string key)
		{
			try
			{
				return this.tibcoMapMessage.GetBytes(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return null;
			}
		}

		public IDictionary GetDictionary(string key)
		{
			try
			{
				return (IDictionary) this.tibcoMapMessage.GetObject(key);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return null;
			}
		}

		public void SetDictionary(string key, IDictionary dictionary)
		{
			try
			{
				this.tibcoMapMessage.SetObject(key, dictionary);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		#endregion
	}
}
