using System;
using System.IO;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
using System.Text;
using System.Globalization;

namespace Xabbo.Messages
{
    public class Packet : IPacket
    {
        public static readonly Type
            Byte = typeof(byte),
            Bool = typeof(bool),
            Short = typeof(short),
            Int = typeof(int),
            Float = typeof(float),
            Long = typeof(long),
            String = typeof(string),
            ByteArray = typeof(byte[]);

        public ReadOnlyMemory<byte> GetBuffer() => _buffer[0..Length];

        public void CopyTo(Span<byte> destination) => _buffer.Span[0..Length].CopyTo(destination);

        public ClientType Protocol { get; set; } = ClientType.Unknown;
        public Header Header { get; set; } = Header.Unknown;

        private int _position;
        public int Position
        {
            get => _position;
            set
            {
                if (value < 0 || value > Length)
                    throw new IndexOutOfRangeException();
                _position = value;
            }
        }

        public int Length { get; private set; }

        public int Available => Length - Position;

        private Memory<byte> _buffer;

        /// <summary>
        /// Constructs a new packet.
        /// </summary>
        public Packet()
            : this(ClientType.Unknown, Header.Unknown)
        { }

        /// <summary>
        /// Constructs a new packet with the specified header.
        /// </summary>
        public Packet(Header header)
            : this(ClientType.Unknown, header)
        { }

        /// <summary>
        /// Constructs a new packet with the specified protocol and header.
        /// </summary>
        public Packet(ClientType protocol, Header header)
        {
            Protocol = protocol;
            Header = header;
            _buffer = new byte[8];
        }

        /// <summary>
        /// Constructs a new packet with the specified header,
        /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
        /// </summary>
        public Packet(Header header, ReadOnlySpan<byte> data)
            : this(ClientType.Unknown, header, data.ToArray().AsMemory())
        { }

        /// <summary>
        /// Constructs a new packet with the specified header,
        /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
        /// </summary>
        public Packet(Header header, ReadOnlySequence<byte> data)
            : this(ClientType.Unknown, header, data)
        { }

        /// <summary>
        /// Constructs a new packet with the specified protocol and header,
        /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
        /// </summary>
        public Packet(ClientType protocol, Header header, ReadOnlySpan<byte> data)
            : this(protocol, header, data.ToArray().AsMemory())
        { }

        /// <summary>
        /// Constructs a new packet with the specified protocol and header,
        /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
        /// </summary>
        public Packet(ClientType protocol, Header header, ReadOnlySequence<byte> data)
            : this(protocol, header, data.ToArray().AsMemory())
        { }

        /// <summary>
        /// Constructs a new packet with the specified protocol and header,
        /// using the <see cref="Memory{T}"/> as its internal buffer.
        /// </summary>
        public Packet(ClientType protocol, Header header, Memory<byte> buffer)
        {
            Protocol = protocol;
            Header = header;
            _buffer = buffer;
            Length = buffer.Length;
        }

        /// <summary>
        /// Creates a copy of the specified original packet.
        /// </summary>
        public Packet Clone()
        {
            return new Packet(
                Protocol,
                Header,
                GetBuffer().Span
            );
        }
        IPacket IReadOnlyPacket.Clone() => Clone();

        private void Grow(int length) => GrowToSize(Position + length);

        private void GrowToSize(int minSize)
        {
            if (_buffer.Length < minSize)
            {
                int size = _buffer.Length;
                while (size < minSize)
                    size <<= 1;

                Memory<byte> oldMemory = _buffer;
                _buffer = new Memory<byte>(new byte[size]);
                oldMemory.CopyTo(_buffer);
            }

            if (Length < minSize)
                Length = minSize;
        }

        public static Packet Compose(Header header, params object[] values) => Compose(ClientType.Unknown, header, values);

        public static Packet Compose(ClientType protocol, Header header, params object[] values)
        {
            var packet = new Packet(header) { Protocol = protocol };
            packet.WriteValues(values);
            return packet;
        }

        public bool CanReadByte() => Available >= 1;

        public bool CanReadBool()
        {
            if (!CanReadByte()) return false;
            byte b = ReadByte();
            Position -= 1;
            return b <= 1;
        }

        public bool CanReadShort() => Available >= 2;

        public bool CanReadInt() => Available >= 4;

        public bool CanReadString()
        {
            if (Available < 2) return false;

            return Available >= 2 + BinaryPrimitives.ReadUInt16BigEndian(_buffer.Span[Position..]);
        }

        public bool CanReadDouble()
        {
            if (!CanReadString()) return false;

            int pos = Position;
            bool success = double.TryParse(ReadString(), NumberStyles.Float, CultureInfo.InvariantCulture, out _);
            Position = pos;

            return success;
        }

        public byte ReadByte()
        {
            if (Available < 1)
                throw new EndOfStreamException();

            Position++;
            return _buffer.Span[Position - 1];
        }

        public byte ReadByte(int position)
        {
            if (position + 1 > Length)
                throw new EndOfStreamException();

            Position = position;
            return ReadByte();
        }

        public void ReadBytes(Span<byte> buffer)
        {
            if (Available < buffer.Length)
                throw new EndOfStreamException();

            Position += buffer.Length;
            _buffer.Span[(Position - buffer.Length)..Position].CopyTo(buffer);
        }

        public void ReadBytes(Span<byte> buffer, int position)
        {
            Position = position;
            ReadBytes(buffer);
        }

        public bool ReadBool() => ReadByte() != 0;

        public bool ReadBool(int position)
        {
            Position = position;
            return ReadBool();
        }

        public short ReadShort()
        {
            if (Available < 2)
                throw new EndOfStreamException();

            Position += 2;
            return BinaryPrimitives.ReadInt16BigEndian(
                _buffer.Span[(Position - 2)..]
            );
        }

        public short ReadShort(int position)
        {
            Position = position;
            return ReadShort();
        }

        public int ReadInt()
        {
            if (Available < 4)
                throw new EndOfStreamException();

            Position += 4;
            return BinaryPrimitives.ReadInt32BigEndian(
                _buffer.Span[(Position - 4)..]
            );
        }

        public int ReadInt(int position)
        {
            Position = position;
            return ReadInt();
        }

        public float ReadFloat()
        {
            if (Available < 4)
                throw new EndOfStreamException();

            Position += 4;
            return BinaryPrimitives.ReadSingleBigEndian(
                _buffer.Span[(Position - 4)..]
            );
        }

        public float ReadFloat(int position)
        {
            Position = position;
            return ReadFloat();
        }

        public long ReadLong()
        {
            if (Available < 8)
                throw new EndOfStreamException();

            Position += 8;
            return BinaryPrimitives.ReadInt64BigEndian(
                _buffer.Span[(Position - 8)..]
            );
        }

        public long ReadLong(int position)
        {
            Position = position;
            return ReadLong();
        }

        public string ReadString()
        {
            if (!CanReadString())
                throw new EndOfStreamException();

            int len = (ushort)ReadShort();
            Position += len;

            return Encoding.UTF8.GetString(_buffer.Span[(Position - len)..Position]);
        }

        public string ReadString(int position)
        {
            Position = position;
            return ReadString();
        }

        public float ReadFloatAsString()
        {
            return float.Parse(ReadString(), CultureInfo.InvariantCulture);
        }

        public float ReadFloatAsString(int position)
        {
            Position = position;
            return ReadFloatAsString();
        }

        public Packet Write(IComposable composable)
        {
            composable.Compose(this);
            return this;
        }
        IPacket IPacket.Write(IComposable composable) => Write(composable);

        public Packet WriteByte(byte value)
        {
            Grow(1);
            _buffer.Span[Position++] = value;
            return this;
        }
        IPacket IPacket.WriteByte(byte value) => WriteByte(value);

        public Packet WriteByte(byte value, int position)
        {
            Position = position;
            return WriteByte(value);
        }
        IPacket IPacket.WriteByte(byte value, int position) => WriteByte(value, position);

        public Packet WriteBytes(ReadOnlySpan<byte> bytes)
        {
            Grow(bytes.Length);
            bytes.CopyTo(_buffer.Span[Position..]);
            Position += bytes.Length;
            return this;
        }
        IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes) => WriteBytes(bytes);

        public Packet WriteBytes(ReadOnlySpan<byte> bytes, int position)
        {
            Position = position;
            return WriteBytes(bytes);
        }
        IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes, int position) => WriteBytes(bytes, position);

        public Packet WriteBool(bool value) => WriteByte((byte)(value ? 1 : 0));
        IPacket IPacket.WriteBool(bool value) => WriteBool(value);

        public Packet WriteBool(bool value, int position)
        {
            Position = position;
            return WriteBool(value);
        }
        IPacket IPacket.WriteBool(bool value, int position) => WriteBool(value, position);

        public Packet WriteShort(short value)
        {
            Grow(2);
            BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[Position..], value);
            Position += 2;
            return this;
        }
        IPacket IPacket.WriteShort(short value) => WriteShort(value);

        public Packet WriteShort(short value, int position)
        {
            Position = position;
            return WriteShort(value);
        }
        IPacket IPacket.WriteShort(short value, int position) => WriteShort(value, position);

        public Packet WriteInt(int value)
        {
            Grow(4);
            BinaryPrimitives.WriteInt32BigEndian(_buffer.Span[Position..], value);
            Position += 4;
            return this;
        }
        IPacket IPacket.WriteInt(int value) => WriteInt(value);

        public Packet WriteInt(int value, int position)
        {
            Position = position;
            return WriteInt(value);
        }
        IPacket IPacket.WriteInt(int value, int position) => WriteInt(value, position);

        public Packet WriteLong(long value)
        {
            Grow(8);
            BinaryPrimitives.WriteInt64BigEndian(_buffer.Span[Position..], value);
            Position += 8;
            return this;
        }
        IPacket IPacket.WriteLong(long value) => WriteLong(value);

        public Packet WriteLong(long value, int position)
        {
            Position = position;
            return WriteLong(value);
        }
        IPacket IPacket.WriteLong(long value, int position) => WriteLong(value, position);

        public Packet WriteFloat(float value)
        {
            Grow(4);
            BinaryPrimitives.WriteSingleBigEndian(_buffer.Span[Position..], value);
            Position += 4;
            return this;
        }
        IPacket IPacket.WriteFloat(float value) => WriteFloat(value);

        public Packet WriteFloat(float value, int position)
        {
            Position = position;
            return WriteFloat(value);
        }
        IPacket IPacket.WriteFloat(float value, int position) => WriteFloat(value, position);

        public Packet WriteFloatAsString(float value)
        {
            return WriteString(value.ToString("0.0##############", CultureInfo.InvariantCulture));
        }
        IPacket IPacket.WriteFloatAsString(float value) => WriteFloatAsString(value);

        public Packet WriteFloatAsString(float value, int position)
        {
            Position = position;
            return WriteFloatAsString(value);
        }
        IPacket IPacket.WriteFloatAsString(float value, int position) => WriteFloatAsString(value, position);

        public Packet WriteString(string value)
        {
            int len = Encoding.UTF8.GetByteCount(value);
            WriteShort((short)len);

            Grow(len);
            Encoding.UTF8.GetBytes(value, _buffer.Span[Position..]);
            Position += len;
            return this;
        }
        IPacket IPacket.WriteString(string value) => WriteString(value);

        public Packet WriteString(string value, int position)
        {
            Position = position;
            return WriteString(value, position);
        }
        IPacket IPacket.WriteString(string value, int position) => WriteString(value, position);

        public Packet WriteValues(params object[] values)
        {
            foreach (object value in values)
            {
                switch (value)
                {
                    case byte x: WriteByte(x); break;
                    case bool x: WriteBool(x); break;
                    case short x: WriteShort(x); break;
                    case ushort x: WriteShort((short)x); break;
                    case LegacyShort x: WriteLegacyShort(x); break;
                    case int x: WriteInt(x); break;
                    case long x: WriteLong(x); break;
                    case LegacyLong x: WriteLegacyLong(x); break;
                    case byte[] x:
                        WriteInt(x.Length);
                        WriteBytes(x);
                        break;
                    case string x: WriteString(x); break;
                    case float x: WriteFloat(x); break;
                    case LegacyFloat x: WriteLegacyFloat(x); break;
                    case IComposable x: x.Compose(this); break;
                    case ICollection x:
                        {
                            WriteLegacyShort((short)x.Count);
                            foreach (object o in x)
                                WriteValues(o);
                        }
                        break;
                    case IEnumerable x:
                        {
                            int count = 0, startPosition = Position;
                            WriteLegacyShort(-1);
                            foreach (object o in x)
                            {
                                WriteValues(o);
                                count++;
                            }
                            int endPosition = Position;
                            WriteLegacyShort((short)count);
                            Position = endPosition;
                        }
                        break;
                    default:
                        if (value == null)
                            throw new Exception("Null value");
                        else
                            throw new Exception($"Invalid value type: {value.GetType().Name}");
                }
            }

            return this;
        }
        IPacket IPacket.WriteValues(params object[] values) => WriteValues(values);

        #region - Replacement -
        public Packet ReplaceString(string newValue) => ReplaceString(newValue, Position);
        IPacket IPacket.ReplaceString(string newValue) => ReplaceString(newValue);

        public Packet ReplaceString(string newValue, int position)
        {
            int prevStrLen = BinaryPrimitives.ReadInt16BigEndian(_buffer.Span[position..]);
            if (Length < position + 2 + prevStrLen)
                throw new InvalidOperationException($"Cannot replace string at position {position}");

            int newStrLen = Encoding.UTF8.GetByteCount(newValue);

            if (newStrLen == prevStrLen)
            {
                Encoding.UTF8.GetBytes(newValue, _buffer.Span[(position + 2)..]);
                Position = position + 2 + newStrLen;
                return this;
            }

            byte[] tail = _buffer.Span[(position + 2 + prevStrLen)..Length].ToArray();

            if (newStrLen > prevStrLen)
            {
                GrowToSize(Length + (newStrLen - prevStrLen));
            }
            else if (newStrLen < prevStrLen)
            {
                Length -= prevStrLen - newStrLen;
            }

            BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[position..], (short)newStrLen);
            Encoding.UTF8.GetBytes(newValue, _buffer.Span[(position + 2)..]);
            tail.CopyTo(_buffer.Span[(position + 2 + newStrLen)..]);

            Position = position + 2 + newStrLen;
            return this;
        }
        IPacket IPacket.ReplaceString(string newValue, int position) => ReplaceString(newValue, position);

        public Packet ReplaceString(Func<string, string> transform) => ReplaceString(transform, Position);
        IPacket IPacket.ReplaceString(Func<string, string> transform) => ReplaceString(transform);

        public Packet ReplaceString(Func<string, string> transform, int position)
        {
            string value = ReadString(position);
            return ReplaceString(transform(value), position);
        }
        IPacket IPacket.ReplaceString(Func<string, string> transform, int position) => ReplaceString(transform, position);

        public Packet ReplaceValues(params object[] newValues)
        {
            foreach (object value in newValues)
            {
                switch (value)
                {
                    case byte x: WriteByte(x); break;
                    case bool x: WriteBool(x); break;
                    case short x: WriteShort(x); break;
                    case ushort x: WriteShort((short)x); break;
                    case int x: WriteInt(x); break;
                    case long x: WriteLong(x); break;
                    case byte[] x: WriteBytes(x); break;
                    case string x: ReplaceString(x); break;
                    case Type t:
                        {
                            if (t == Byte) ReadByte();
                            else if (t == Bool) ReadBool();
                            else if (t == Short) ReadShort();
                            else if (t == Int) ReadInt();
                            else if (t == ByteArray) throw new NotSupportedException();
                            else if (t == String) ReadString();
                            else throw new Exception($"Invalid type specified: {t.FullName}");
                        }
                        break;
                    default: throw new Exception($"Value is of invalid type: {value.GetType().Name}");
                }
            }

            return this;
        }
        IPacket IPacket.ReplaceValues(params object[] newValues) => ReplaceValues(newValues);

        public Packet ReplaceValues(object[] newValues, int position)
        {
            Position = position;
            return ReplaceValues(newValues);
        }
        IPacket IPacket.ReplaceValues(object[] newValues, int position) => ReplaceValues(newValues, position);
        #endregion

        #region - Legacy -
        public short ReadLegacyShort()
        {
            return Protocol switch
            {
                ClientType.Unity => ReadShort(),
                ClientType.Flash => (short)ReadInt(),
                _ => throw new InvalidOperationException("Cannot read legacy short, unknown protocol type.")
            };
        }

        public float ReadLegacyFloat()
        {
            return Protocol switch
            {
                ClientType.Unity => ReadFloat(),
                ClientType.Flash => ReadFloatAsString(),
                _ => throw new InvalidOperationException("Cannot read legacy float, unknown protocol type.")
            };
        }

        public long ReadLegacyLong()
        {
            return Protocol switch
            {
                ClientType.Unity => ReadLong(),
                ClientType.Flash => ReadInt(),
                _ => throw new InvalidOperationException("Cannot read legacy long, unknown protocol type.")
            };
        }

        public Packet WriteLegacyShort(short value)
        {
            return Protocol switch
            {
                ClientType.Unity => WriteShort(value),
                ClientType.Flash => WriteInt(value),
                _ => throw new InvalidOperationException("Cannot write legacy short, unknown protocol type.")
            };
        }
        IPacket IPacket.WriteLegacyShort(short value) => WriteLegacyShort(value);

        public Packet WriteLegacyFloat(float value)
        {
            return Protocol switch
            {
                ClientType.Unity => WriteFloat(value),
                ClientType.Flash => WriteFloatAsString(value),
                _ => throw new InvalidOperationException("Cannot write legacy float, unknown protocol type.")
            };
        }
        IPacket IPacket.WriteLegacyFloat(float value) => WriteLegacyFloat(value);

        public Packet WriteLegacyLong(long value)
        {
            return Protocol switch
            {
                ClientType.Unity => WriteLong(value),
                ClientType.Flash => WriteInt((int)value),
                _ => throw new InvalidOperationException("Cannot write legacy long, unknown protocol type.")
            };
        }
        IPacket IPacket.WriteLegacyLong(long value) => WriteLegacyLong(value);
        #endregion
    }
}
