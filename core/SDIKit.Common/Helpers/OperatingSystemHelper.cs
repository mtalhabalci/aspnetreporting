using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SDIKit.Common.Helpers
{
    public static class OperatingSystemHelper
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
