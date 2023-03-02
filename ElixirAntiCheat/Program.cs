using Microsoft.Win32;
using System.Diagnostics;

namespace ElixirAntiCheat
{
    internal static class Program
    {
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new ANITCHEAT());
        }
    }
}