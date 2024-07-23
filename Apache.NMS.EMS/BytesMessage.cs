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

namespace Apache.NMS.EMS
{
	class BytesMessage : Apache.NMS.EMS.Message, Apache.NMS.IBytesMessage
	{
		public TIBCO.EMS.BytesMessage tibcoBytesMessage
		{
			get { return this.tibcoMessage as TIBCO.EMS.BytesMessage; }
			set { this.tibcoMessage = value; }
		}

		public BytesMessage(TIBCO.EMS.BytesMessage message)
			: base(message)
		{
		}

		#region IBytesMessage Members

		public byte[] Content
		{
			get
			{
				int contentLength = (int) this.tibcoBytesMessage.BodyLength;
				byte[] msgContent = new byte[contentLength];

				this.tibcoBytesMessage.Reset();
				this.tibcoBytesMessage.ReadBytes(msgContent, contentLength);
				return msgContent;
			}

			set
			{
				this.ReadOnlyBody = false;
				this.tibcoBytesMessage.ClearBody();
				this.tibcoBytesMessage.WriteBytes(value, 0, value.Length);
			}
		}

		public long BodyLength
		{
			get { return this.tibcoBytesMessage.BodyLength; }
		}

		public bool ReadBoolean()
		{
			return this.tibcoBytesMessage.ReadBoolean();
		}

		public byte ReadByte()
		{
			return (byte) this.tibcoBytesMessage.ReadByte();
		}

		public int ReadBytes(byte[] value, int length)
		{
			return this.tibcoBytesMessage.ReadBytes(value, length);
		}

		public int ReadBytes(byte[] value)
		{
			return this.tibcoBytesMessage.ReadBytes(value);
		}

		public char ReadChar()
		{
			return this.tibcoBytesMessage.ReadChar();
		}

		public double ReadDouble()
		{
			return this.tibcoBytesMessage.ReadDouble();
		}

		public short ReadInt16()
		{
			return this.tibcoBytesMessage.ReadShort();
		}

		public int ReadInt32()
		{
			return this.tibcoBytesMessage.ReadInt();
		}

		public long ReadInt64()
		{
			return this.tibcoBytesMessage.ReadLong();
		}

		public float ReadSingle()
		{
			return this.tibcoBytesMessage.ReadFloat();
		}

		public string ReadString()
		{
			return this.tibcoBytesMessage.ReadUTF();
		}

		public void Reset()
		{
			this.tibcoBytesMessage.Reset();
		}

		public void WriteBoolean(bool value)
		{
			this.tibcoBytesMessage.WriteBoolean(value);
		}

		public void WriteByte(byte value)
		{
			this.tibcoBytesMessage.WriteByte(value);
		}

		public void WriteBytes(byte[] value, int offset, int length)
		{
			this.tibcoBytesMessage.WriteBytes(value, offset, length);
		}

		public void WriteBytes(byte[] value)
		{
			this.tibcoBytesMessage.WriteBytes(value);
		}

		public void WriteChar(char value)
		{
			this.tibcoBytesMessage.WriteChar(value);
		}

		public void WriteDouble(double value)
		{
			this.tibcoBytesMessage.WriteDouble(value);
		}

		public void WriteInt16(short value)
		{
			this.tibcoBytesMessage.WriteShort(value);
		}

		public void WriteInt32(int value)
		{
			this.tibcoBytesMessage.WriteInt(value);
		}

		public void WriteInt64(long value)
		{
			this.tibcoBytesMessage.WriteLong(value);
		}

		public void WriteObject(object value)
		{
			this.tibcoBytesMessage.WriteObject(value);
		}

		public void WriteSingle(float value)
		{
			this.tibcoBytesMessage.WriteFloat(value);
		}

		public void WriteString(string value)
		{
			this.tibcoBytesMessage.WriteUTF(value);
		}

		#endregion
	}
}
