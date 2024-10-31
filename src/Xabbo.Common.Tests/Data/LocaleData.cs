using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Xabbo.Common.Tests;

public class LocaleData : IEnumerable<object[]>
{
    private static readonly string[] CultureNames = [ "en", "es", "fi", "it", "nl", "de", "fr", "pt", "tr" ];

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (string cultureName in CultureNames)
        {
            yield return [new CultureInfo(cultureName)];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}