using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Provides extensions for reading/writing values to/from packets.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class PacketExtensions
{
    #region Generic read
    /// <summary>
    /// Reads a generically typed value from the packet.
    /// </summary>
    /// <typeparam name="T">The type of value to read.</typeparam>
    /// <param name="p">The packet to read from.</param>
    /// <returns>The value read from the current postion in the packet.</returns>
    /// <exception cref="ArgumentException">The specified type <typeparamref name="T"/> cannot be read from the packet.</exception>
    public static T Read<T>(this IReadOnlyPacket p)
    {
        static TOut Convert<TIn, TOut>(TIn value) => Unsafe.As<TIn, TOut>(ref value);

        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.Boolean => Convert<bool, T>(p.ReadBool()),
            TypeCode.Byte or TypeCode.SByte => Convert<byte, T>(p.ReadByte()),
            TypeCode.Int16 or TypeCode.UInt16 => Convert<short, T>(p.ReadShort()),
            TypeCode.Int32 or TypeCode.UInt32 => Convert<int, T>(p.ReadInt()),
            TypeCode.Int64 or TypeCode.UInt64 => Convert<long, T>(p.ReadLegacyLong()),
            TypeCode.String => Convert<string, T>(p.ReadString()),
            TypeCode.Single => Convert<float, T>(p.ReadFloat()),
            _ when typeof(T) == typeof(LegacyShort) => Convert<LegacyShort, T>(p.ReadLegacyShort()),
            _ when typeof(T) == typeof(LegacyFloat) => Convert<LegacyFloat, T>(p.ReadLegacyFloat()),
            _ when typeof(T) == typeof(LegacyLong) => Convert<LegacyLong, T>(p.ReadLegacyLong()),
            _ => throw new ArgumentException($"The specified type is not supported for packet deserialization: {typeof(T).Name}.")
        };
    }

    /// <summary>
    /// Reads a collection of values of the specified type into a list.
    /// The size of the list is read as an int on Flash sessions, or a short on Unity sessions.
    /// </summary>
    public static List<T> ReadList<T>(this IReadOnlyPacket p)
    {
        List<T> list = new();
        int n = p.ReadLegacyShort();
        for (int i = 0; i < n; i++)
            list.Add(p.Read<T>());
        return list;
    }
    #endregion

    /// <summary>
    /// Writes the specified object to the packet.
    /// It is recommended to use the explicitly typed Write methods or the generically typed <see cref="Write{TPacket, T}(TPacket, T)"/>.
    /// </summary>
    /// <exception cref="ArgumentException">The type of the specified object is not supported for packet serialization.</exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static TPacket WriteObject<TPacket>(this TPacket p, object value)
        where TPacket : IPacket
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        return (TPacket)(value switch
        {
            bool x => p.WriteBool(x),
            byte x => p.WriteByte(x),
            short x => p.WriteShort(x),
            ushort x => p.WriteShort((short)x),
            LegacyShort x => p.WriteLegacyShort(x),
            int x => p.WriteInt(x),
            uint x => p.WriteInt((int)x),
            float x => p.WriteFloat(x),
            LegacyFloat x => p.WriteLegacyFloat(x),
            long x => p.WriteLegacyLong(x),
            ulong x => p.WriteLegacyLong((long)x),
            LegacyLong x => p.WriteLegacyLong(x),
            string x => p.WriteString(x),
            IComposable x => p.Write(x),
            ICollection x => WriteObjectCollection(p, x),
            IEnumerable x => WriteObjectEnumerable(p, x),
            _ => throw new ArgumentException($"The specified type is not supported for packet serialization: {value.GetType().Name}.", nameof(value))
        });
    }

    /// <summary>
    /// Writes the specified collection of objects to the packet.
    /// The size of the collection is written as an int on Flash sessions, or a short on Unity sessions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static TPacket WriteObjectCollection<TPacket>(this TPacket p, ICollection collection)
        where TPacket : IPacket
    {
        p.WriteLegacyShort((short)collection.Count);
        foreach (object value in collection)
            WriteObject(p, value);
        return p;
    }

    /// <summary>
    /// Writes the specified enumerable to the packet.
    /// The number of items is written as an int on Flash sessions, or a short on Unity sessions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static TPacket WriteObjectEnumerable<TPacket>(this TPacket p, IEnumerable enumerable)
        where TPacket : IPacket
    {
        int startPosition = p.Position;
        p.WriteLegacyShort(-1);

        int count = 0;
        foreach (object item in enumerable)
        {
            p.WriteObject(item);
            count++;
        }

        int endPosition = p.Position;
        p.Position = startPosition;
        p.WriteLegacyShort((short)count);
        p.Position = endPosition;

        return p;
    }

    #region Generic write
    /// <summary>
    /// Writes the specified generically typed value to the packet.
    /// </summary>
    /// <typeparam name="TPacket">The type of the packet.</typeparam>
    /// <typeparam name="T">The type of value to write.</typeparam>
    /// <param name="p">The packet.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>A reference to this instance after the value has been written.</returns>
    /// <exception cref="ArgumentException">The specified type is not supported for packet serialization.</exception>
    public static TPacket Write<TPacket, T>(this TPacket p, T value)
        where TPacket : IPacket
    {
        return (TPacket)(value switch
        {
            bool x => p.WriteBool(x),
            byte x => p.WriteByte(x),
            short x => p.WriteShort(x),
            ushort x => p.WriteShort((short)x),
            LegacyShort x => p.WriteLegacyShort(x),
            int x => p.WriteInt(x),
            uint x => p.WriteInt((int)x),
            float x => p.WriteFloat(x),
            LegacyFloat x => p.WriteLegacyFloat(x),
            long x => p.WriteLegacyLong(x),
            ulong x => p.WriteLegacyLong((long)x),
            LegacyLong x => p.WriteLegacyLong(x),
            string x => p.WriteString(x),
            IComposable x => p.Write(x),
            ICollection x => WriteObjectCollection(p, x),
            IEnumerable x => WriteObjectEnumerable(p, x),
            _ => throw new ArgumentException($"The specified type is not supported for packet serialization: {typeof(T).Name}.", nameof(value))
        });
    }

    /// <summary>
    /// Writes the specified generically typed collection to the packet.
    /// The size of the collection is written as an int on Flash sessions, or a short on Unity sessions.
    /// </summary>
    public static TPacket WriteCollection<TPacket, T>(this TPacket p, ICollection<T> collection)
        where TPacket : IPacket
    {
        p.WriteLegacyShort((short)collection.Count);
        foreach (T value in collection)
            Write(p, value);
        return p;
    }

    /// <summary>
    /// Writes the specified generically typed enumerable to the packet.
    /// The number of items is written as an int on Flash sessions, or a short on Unity sessions.
    /// </summary>
    public static TPacket WriteEnumerable<TPacket, T>(this TPacket p, IEnumerable<T> enumerable)
        where TPacket : IPacket
    {
        int startPosition = p.Position;
        p.WriteLegacyShort(-1);

        int count = 0;
        foreach (T item in enumerable)
        {
            p.Write(item);
            count++;
        }

        int endPosition = p.Position;
        p.Position = startPosition;
        p.WriteLegacyShort((short)count);
        p.Position = endPosition;

        return p;
    }
    #endregion
}
