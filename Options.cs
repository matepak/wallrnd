using CommandLine;
namespace randw
{
    class Options : RandomOpt
    {
        [Option(Required = true, HelpText = "Set path to folder conatining images")]
        public string Path { get; set; }
        [Option(HelpText = "Set wallpaper centered")] public bool Center { get; set; }

        [Option(HelpText = "Set wallpaper tiled")] public bool Tile { get; set; }

        [Option(HelpText = "Set wallpaper streched")] public bool Stretch { get; set; }
        [Option(HelpText = "Set wallpaper to fit desktop")] public bool Fit { get; set; }

        [Option(HelpText = "Set wallpaper to fill desktop")] public bool Fill { get; set; }

        [Option(HelpText = "Set wallpaper to span desktop")] public bool Span { get; set; }

        public bool Random {get; set;}
        public bool Recursive {get; set;}

    }

    interface RandomOpt
    {
        [Option(Group ="random", Default = false, HelpText = "Set random wallpaper from given path")] bool Random { get; set; }
        [Option(Group = "random", Default = false, HelpText = "Access subfolders")] bool Recursive { get; set; }

    }
}