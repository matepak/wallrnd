using CommandLine;
namespace randw
{
    class Options
    {
        [Option('p', "path", Required = true, HelpText = "Set path to folder conatining images")]
        public string Path { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Access subfolders")]
        public bool Recursive { get; set; }
    }
}