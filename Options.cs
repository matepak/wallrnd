using CommandLine;
namespace randw
{
    class Options
    {
        [Option('p', "path", Required = true, HelpText = "Set path to folder conatining images")]
        public string Path { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Access subfolders")] public bool Recursive { get; set; }
        [Option(Default = false, HelpText = "Set random wallpaper from given path")] public bool Random { get; set; }

        [Option(Default = false, HelpText = "Set wallpaper centered")] public bool Center { get; set; }

        [Option(Default = false, HelpText = "Set wallpaper tiled")] public bool Tile { get; set; }

        [Option(Default = false, HelpText = "Set wallpaper streched")] public bool Stretch { get; set; }
        [Option(Default = false, HelpText = "Set wallpaper to fit desktop")] public bool Fit { get; set; }

        [Option(Default = false, HelpText = "Set wallpaper to fill desktop")] public bool Fill { get; set; }

        [Option(Default = false, HelpText = "Set wallpaper to span desktop")] public bool Span { get; set; }

    }
}