using System;
using System.Text;

namespace Xabbo.Common.Tests;

public static class Utility
{
    public static string HexDump(this Span<byte> bytes) => HexDump((ReadOnlySpan<byte>)bytes);
    public static string HexDump(this ReadOnlySpan<byte> bytes)
    {
        static char ToChar(byte b)
        {
            if (0x20 <= b && b <= 0x7e)
                return (char)b;
            else
                return '.';
        }

        var sb = new StringBuilder();
        int rows = (bytes.Length + 7) / 8;
        for (int row = 0; row < rows; row++)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i > 0)
                    sb.Append(' ');
                int index = row * 8 + i;
                if (index < bytes.Length)
                    sb.Append($"{bytes[index]:x02}");
                else
                    sb.Append("  ");
            }
            sb.Append(' ');
            for (int i = 0; i < 8; i++)
            {
                int index = row * 8 + i;
                if (index < bytes.Length)
                    sb.Append(ToChar(bytes[index]));
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}