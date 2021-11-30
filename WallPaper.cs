using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace randw

{
    sealed class WallPaper
    {
        public static void SetWallpaper(String pathToWallpaper)
        {
            if (!SystemParametersInfo(0x0014, 0, pathToWallpaper, 0x01))
            {
                Int32 err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err);
            }
        }
        [DllImport("User32.dll", SetLastError = true)]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);
        private WallPaper() { }
    }

}