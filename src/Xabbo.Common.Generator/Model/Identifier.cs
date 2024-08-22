namespace Xabbo.Common.Generator.Model;

internal readonly record struct Identifier(Client Client, Direction Direction, string Name)
{
    public static bool IsValid(string name)
    {
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            if (!char.IsLetter(c))
            {
                if (i == 0)
                    return false;
                if (char.IsDigit(c))
                    continue;
                if (i < (name.Length - 1) && c == '_')
                    continue;
                return false;
            }
        }
        return name.Length > 0;
    }
}