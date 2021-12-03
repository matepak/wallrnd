using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace randw
{
    sealed class ListFiles
    {
        public static string regexPattern = @"\b[\w\-]+\.(jpe?g|bmp|dib|png|jfif|jpe|gif|tif?f|wdp|heics|heifs|hif|avcs|avifs?)\b";
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
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Folder doesn't conatain any image files, use --recursive parameter for recursive");
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }
    }
}