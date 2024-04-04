using Xabbo.Messages;

namespace Xabbo;

public static partial class PacketExtensions
{
	#region Generic Read
	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2) Read<T1, T2>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3) Read<T1, T2, T3>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4) Read<T1, T2, T3, T4>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5) Read<T1, T2, T3, T4, T5>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6) Read<T1, T2, T3, T4, T5, T6>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7) Read<T1, T2, T3, T4, T5, T6, T7>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8) Read<T1, T2, T3, T4, T5, T6, T7, T8>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>(),
		packet.Read<T16>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>(),
		packet.Read<T16>(),
		packet.Read<T17>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>(),
		packet.Read<T16>(),
		packet.Read<T17>(),
		packet.Read<T18>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>(),
		packet.Read<T16>(),
		packet.Read<T17>(),
		packet.Read<T18>(),
		packet.Read<T19>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this IReadOnlyPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>(),
		packet.Read<T6>(),
		packet.Read<T7>(),
		packet.Read<T8>(),
		packet.Read<T9>(),
		packet.Read<T10>(),
		packet.Read<T11>(),
		packet.Read<T12>(),
		packet.Read<T13>(),
		packet.Read<T14>(),
		packet.Read<T15>(),
		packet.Read<T16>(),
		packet.Read<T17>(),
		packet.Read<T18>(),
		packet.Read<T19>(),
		packet.Read<T20>()
	);
	#endregion

	#region Generic Write
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2>(this TPacket packet, T1 arg1, T2 arg2)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15)
			.Write(arg16);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15)
			.Write(arg16)
			.Write(arg17);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15)
			.Write(arg16)
			.Write(arg17)
			.Write(arg18);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15)
			.Write(arg16)
			.Write(arg17)
			.Write(arg18)
			.Write(arg19);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static TPacket Write<TPacket, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this TPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20)
		where TPacket : IPacket
	{
		return packet
			.Write(arg1)
			.Write(arg2)
			.Write(arg3)
			.Write(arg4)
			.Write(arg5)
			.Write(arg6)
			.Write(arg7)
			.Write(arg8)
			.Write(arg9)
			.Write(arg10)
			.Write(arg11)
			.Write(arg12)
			.Write(arg13)
			.Write(arg14)
			.Write(arg15)
			.Write(arg16)
			.Write(arg17)
			.Write(arg18)
			.Write(arg19)
			.Write(arg20);
	}
	#endregion

	#region Generic Skip
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
		packet.Read<T16>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
		packet.Read<T16>();
		packet.Read<T17>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
		packet.Read<T16>();
		packet.Read<T17>();
		packet.Read<T18>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
		packet.Read<T16>();
		packet.Read<T17>();
		packet.Read<T18>();
		packet.Read<T19>();
	}
	/// <summary>
	/// Skips the specified value types.
	/// </summary>
	public static void Skip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this IReadOnlyPacket packet)
	{
		packet.Read<T1>();
		packet.Read<T2>();
		packet.Read<T3>();
		packet.Read<T4>();
		packet.Read<T5>();
		packet.Read<T6>();
		packet.Read<T7>();
		packet.Read<T8>();
		packet.Read<T9>();
		packet.Read<T10>();
		packet.Read<T11>();
		packet.Read<T12>();
		packet.Read<T13>();
		packet.Read<T14>();
		packet.Read<T15>();
		packet.Read<T16>();
		packet.Read<T17>();
		packet.Read<T18>();
		packet.Read<T19>();
		packet.Read<T20>();
	}
	#endregion
}