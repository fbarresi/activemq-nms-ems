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
namespace Apache.NMS.EMS
{
	class StreamMessage : Apache.NMS.EMS.Message, Apache.NMS.IStreamMessage
	{
		public TIBCO.EMS.StreamMessage tibcoStreamMessage
		{
			get { return this.tibcoMessage as TIBCO.EMS.StreamMessage; }
			set { this.tibcoMessage = value; }
		}

		public StreamMessage(TIBCO.EMS.StreamMessage message)
			: base(message)
		{
		}

		#region IStreamMessage Members

		public bool ReadBoolean()
		{
			try
			{
				return this.tibcoStreamMessage.ReadBoolean();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return false;
			}
		}

		public byte ReadByte()
		{
			try
			{
				return this.tibcoStreamMessage.ReadByte();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public int ReadBytes(byte[] value)
		{
			try
			{
				return this.tibcoStreamMessage.ReadBytes(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public char ReadChar()
		{
			try
			{
				return this.tibcoStreamMessage.ReadChar();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return (char) 0;
			}
		}

		public double ReadDouble()
		{
			try
			{
				return this.tibcoStreamMessage.ReadDouble();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public short ReadInt16()
		{
			try
			{
				return this.tibcoStreamMessage.ReadShort();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public int ReadInt32()
		{
			try
			{
				return this.tibcoStreamMessage.ReadInt();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public long ReadInt64()
		{
			try
			{
				return this.tibcoStreamMessage.ReadLong();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public object ReadObject()
		{
			try
			{
				return this.tibcoStreamMessage.ReadObject();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public float ReadSingle()
		{
			try
			{
				return this.tibcoStreamMessage.ReadFloat();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return 0;
			}
		}

		public string ReadString()
		{
			try
			{
				return this.tibcoStreamMessage.ReadString();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
				return null;
			}
		}

		public void Reset()
		{
			try
			{
				this.tibcoStreamMessage.Reset();
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteBoolean(bool value)
		{
			try
			{
				this.tibcoStreamMessage.WriteBoolean(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteByte(byte value)
		{
			try
			{
				this.tibcoStreamMessage.WriteByte(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteBytes(byte[] value, int offset, int length)
		{
			try
			{
				this.tibcoStreamMessage.WriteBytes(value, offset, length);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteBytes(byte[] value)
		{
			try
			{
				this.tibcoStreamMessage.WriteBytes(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteChar(char value)
		{
			try
			{
				this.tibcoStreamMessage.WriteChar(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteDouble(double value)
		{
			try
			{
				this.tibcoStreamMessage.WriteDouble(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteInt16(short value)
		{
			try
			{
				this.tibcoStreamMessage.WriteShort(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteInt32(int value)
		{
			try
			{
				this.tibcoStreamMessage.WriteInt(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteInt64(long value)
		{
			try
			{
				this.tibcoStreamMessage.WriteLong(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteObject(object value)
		{
			try
			{
				this.tibcoStreamMessage.WriteObject(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteSingle(float value)
		{
			try
			{
				this.tibcoStreamMessage.WriteFloat(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		public void WriteString(string value)
		{
			try
			{
				this.tibcoStreamMessage.WriteString(value);
			}
			catch(Exception ex)
			{
				ExceptionUtil.WrapAndThrowNMSException(ex);
			}
		}

		#endregion
	}
}
