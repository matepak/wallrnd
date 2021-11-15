using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CommandLine;

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

    sealed class ListFiles
    {
        private ListFiles() { }
        public static string RandFiles(string path)
        {
            var rand = new Random();
            List<string> fileList = new List<string>();
            try
            {
                var files = Directory.EnumerateFiles(path);
                foreach (var f in files)
                {
                    fileList.Add(f.ToString());
                }
                return fileList[rand.Next(fileList.Count)];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed(Run)
            .WithNotParsed(HandleParsedError);
        }

        private static void HandleParsedError(IEnumerable<Error> obj)
        {
            throw new NotImplementedException();
        }

        private static void Run(Options opt)
        {
            var path = ListFiles.RandFiles(opt.Path);
            WallPaper.SetWallpaper(path);
        }
    }
}