using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CommandLine;
using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.Versioning;

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
        public static List<string> FileList(string path, bool recursive)
        {
            Regex reg = new Regex(regexPattern);
            if (recursive)
            {
                var query = from file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                            where reg.IsMatch(file)
                            select file;
                return query.ToList();
            }
            else
            {
                var query = from file in Directory.EnumerateFiles(path, "*")
                            where reg.IsMatch(file)
                            select file;
                return query.ToList();
            }
        }
        public static string GetRandom(string path, bool recursive)
        {
            var rand = new Random();
            List<string> fileList = new List<string>();
            try
            {
                fileList = FileList(path, recursive);
                return fileList[rand.Next(fileList.Count)];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Error.WriteLine(e.Message);
                Console.WriteLine("Folder doesn't conatain any image files, use -r parameter for recursive");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }

    class Program
    {
        [SupportedOSPlatform("Windows")]
        static void Main(string[] args)
        {
                Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Center) { WallpaperStyle.Center(); }
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

                });
        }

        private static void SetRandomWallpaper(Options opt)
        {
            var path = ListFiles.GetRandom(opt.Path, opt.Recursive);
            WallPaper.SetWallpaper(path);
        }

        private static void SetWallpaper(Options opt)
        {
            WallPaper.SetWallpaper(opt.Path);
        }
    }
}