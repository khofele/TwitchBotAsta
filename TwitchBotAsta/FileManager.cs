using System.IO;
using System.Text.RegularExpressions;

namespace TwitchBotAsta
{
    class FileManager
    {
        private static string path = @"E:\Desktop\Temp\text.txt";
        private string tempPath = @"E:\Desktop\Temp\temptext.txt";


        public static string Path
        {
            get => path;
        }
        public void WriteToFile(string message)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(message);
            }
        }

        public void DeleteTaskInFile(string user)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(path))
            {
                using (StreamWriter writer = new StreamWriter(tempPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(user))
                        {
                            continue;
                        }
                        writer.WriteLine(line);
                    }
                }
            }
            RenameAndDeleteFile();
            DeleteEmptyLines();
        }

        public string FindTask(string user)
        {
            if (File.Exists(FileManager.Path))
            {
                string line = "";

                using (StreamReader reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(user))
                        {
                            string modifiedLine = line.Replace((user + " - "), "");
                            return modifiedLine;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public void FindAndEditTask(string user, string message)
        {
            if (File.Exists(FileManager.Path))
            {
                string line = "";

                using (StreamReader reader = new StreamReader(path))
                {
                    using (StreamWriter writer = new StreamWriter(tempPath))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith(user))
                            {
                                string modifiedLine = line.Replace(line, user + message);
                                writer.WriteLine(modifiedLine);
                                continue;
                            }
                            writer.WriteLine(line);
                        }
                    }
                }
                RenameAndDeleteFile();
                DeleteEmptyLines();
            }
        }

        private void DeleteEmptyLines()
        {
            string text = File.ReadAllText(path);
            string result = Regex.Replace(text, @"(^\p{Zs}*\r\n){2,}", "\r\n", RegexOptions.Multiline);
            File.WriteAllText(tempPath, result);
            RenameAndDeleteFile();
        }

        private void RenameAndDeleteFile()
        {
            File.Delete(path);
            File.Move(tempPath, path);
            File.Delete(tempPath);
        }
    }
}
