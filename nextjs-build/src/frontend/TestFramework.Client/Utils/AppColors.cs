using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Utils
{
    public class AppColors
    {
        public static string Success { get; } = Colors.LightGreen.Lighten1;
        public static string NotStarted { get; } = Colors.BlueGrey.Lighten3;
        public static string Started { get; } = Colors.Blue.Lighten2;
        public static string Failure { get; } = Colors.Red.Darken1;
    }
}
