using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CommandLine;
using System.Text.RegularExpressions;
using System.Linq;

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
        static string regexPattern = @"\b[\w\-]+\.(jpe?g|bmp|dib|png|jfif|jpe|gif|tif?f|wdp|heics|heifs|hif|avcs|avifs?)\b";
        private ListFiles() { }
        public static string GetRandom(string path)
        {
            var rand = new Random();
            List<string> fileList = new List<string>();
            try
            {
                Regex reg = new Regex(regexPattern);
                var files = Directory.EnumerateFiles(path).Where(path => reg.IsMatch(path));
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
            .WithParsed<Options>(o =>
            {
                if (o.Center) WallpaperStyle.Center();
                if (o.Tile) WallpaperStyle.Tile();
                if (o.Stretch) WallpaperStyle.Stretch();
                if (o.Fit) WallpaperStyle.Fit();
                if (o.Fill) WallpaperStyle.Fill();
                if (o.Span) WallpaperStyle.Span();
                
                if (o.Random) 
                {
                    SetRandomWallpaper(o);
                }
                else 
                {
                    SetWallpaper(o);
                }

            })
            .WithNotParsed(HandleParsedError);
            
        }

        private static void HandleParsedError(IEnumerable<Error> obj)
        {
            throw new NotImplementedException();
        }

        private static void SetRandomWallpaper(Options opt)
        {
            var path = ListFiles.GetRandom(opt.Path);
            WallPaper.SetWallpaper(path);
        }

        private static void SetWallpaper(Options opt) 
        {
            WallPaper.SetWallpaper(opt.Path);
        }
    }
}