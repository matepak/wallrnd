using System.Runtime.Versioning;
using Microsoft.Win32;

namespace randw
{
    [SupportedOSPlatform("Windows")]
    sealed class WallpaperStyle
    {
        private WallpaperStyle() { }

        public static void Center() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "0");
        }
        public static void Tile() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "1");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "0");            
        }
        public static void Stretch() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "2");
        }
        public static void Fit() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "6");
        }
        public static void Fill() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "10");
        }

        public static void Span() 
        { 
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "TileWallpaper", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\", "WallpaperStyle", "22");
        }
    }
}