using System.ComponentModel;

using Xabbo.Messages;

namespace Xabbo;

#pragma warning disable CS1591
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class PacketExtensions
#pragma warning restore CS1591
{
	#region Generic Read
	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2) Read<T1, T2>(this IPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3) Read<T1, T2, T3>(this IPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4) Read<T1, T2, T3, T4>(this IPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5) Read<T1, T2, T3, T4, T5>(this IPacket packet) => (
		packet.Read<T1>(),
		packet.Read<T2>(),
		packet.Read<T3>(),
		packet.Read<T4>(),
		packet.Read<T5>()
	);

	/// <summary>
	/// Reads the specified generically typed values from the packet into a tuple.
	/// </summary>
	public static (T1, T2, T3, T4, T5, T6) Read<T1, T2, T3, T4, T5, T6>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7) Read<T1, T2, T3, T4, T5, T6, T7>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8) Read<T1, T2, T3, T4, T5, T6, T7, T8>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IPacket packet) => (
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
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IPacket packet) => (
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
	#endregion

	#region Generic Write
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2>(this IPacket packet, T1 arg1, T2 arg2)
	{
		packet.Write(arg1);
		packet.Write(arg2);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
		packet.Write(arg12);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
		packet.Write(arg12);
		packet.Write(arg13);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
		packet.Write(arg12);
		packet.Write(arg13);
		packet.Write(arg14);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
		packet.Write(arg12);
		packet.Write(arg13);
		packet.Write(arg14);
		packet.Write(arg15);
	}
	/// <summary>
	/// Writes the specified generically typed values to the packet.
	/// </summary>
	public static void Write<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IPacket packet, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
	{
		packet.Write(arg1);
		packet.Write(arg2);
		packet.Write(arg3);
		packet.Write(arg4);
		packet.Write(arg5);
		packet.Write(arg6);
		packet.Write(arg7);
		packet.Write(arg8);
		packet.Write(arg9);
		packet.Write(arg10);
		packet.Write(arg11);
		packet.Write(arg12);
		packet.Write(arg13);
		packet.Write(arg14);
		packet.Write(arg15);
		packet.Write(arg16);
	}
	#endregion
}