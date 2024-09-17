using System.Runtime.CompilerServices;
using DiffEngine;

namespace Xabbo.Common.Generator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        DiffTools.UseOrder(DiffTool.VisualStudioCode);
        VerifyDiffPlex.Initialize();
        VerifySourceGenerators.Initialize();
    }
}
