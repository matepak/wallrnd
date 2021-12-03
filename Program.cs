using CommandLine;
using System.Text.RegularExpressions;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using static System.Console;

namespace randw
{
    class Program
    {
        [SupportedOSPlatform("Windows")]
        static void Main(string[] args)
        {
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WriteLine("OS is not supported");
                return;
            }
            
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
                    SetWallpaper(o.Path);
                }

            });
        }

        private static void SetRandomWallpaper(Options opt)
        {
            var path = ListFiles.GetRandom(opt.Path, opt.Recursive);
            SetWallpaper(path);
        }

        private static void SetWallpaper(string path)
        {
            Regex reg = new Regex(ListFiles.regexPattern);
            if (!reg.IsMatch(path)) return;
            WallPaper.SetWallpaper(path);
        }
    }
}