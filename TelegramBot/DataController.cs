using Newtonsoft.Json;
using File = System.IO.File;
using Telegram.Bot.Types;

namespace TelegramBot
{
    internal class DataController
    {
        private static readonly string FOLDER_PATH = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/magnitstormbot";
        private static readonly string IDS_PATH = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/magnitstormbot/Ids.txt";
        public static Dictionary<string, string> GetData(string chatId)
        {
            if (!Directory.Exists(FOLDER_PATH))
            {
                Directory.CreateDirectory(FOLDER_PATH);
            }
            else if (File.Exists($@"{FOLDER_PATH}/{chatId}.txt"))
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText($@"{FOLDER_PATH}/{chatId}.txt"));
                if (data != null && !data.ContainsKey("City"))
                {
                    return new Dictionary<string, string>() {
                    {"City",""},
                    {"LastMessage",""},
                    {"TimeDif",""},
                    {"NotificationTime",""}
                };
                }
                return data;
            }
            return new Dictionary<string, string>();
        }
        public static List<string> GetData()
        {
            if (File.Exists(IDS_PATH))
            {
                var data = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(IDS_PATH)) ?? new List<string>();
                return data;
            }
            return new List<string>();
        }
        public static void WriteData(Dictionary<string, string> userInfo, long chatId)
        {
            var data = JsonConvert.SerializeObject(userInfo);
            File.WriteAllText($@"{FOLDER_PATH}/{chatId}.txt", data);
        }
        public static void WriteIds(List<string> Ids)
        {
            File.WriteAllLines(IDS_PATH, Ids);
        }
        public static List<string> GetAllUsersId()
        {
            var userIds = new List<string>();
            var users = Directory.GetFiles(FOLDER_PATH);
            foreach (var userFile in users)
            {
                var startIndex = userFile.IndexOf("magnitstormbot", 0) + 15;
                var endIndex = userFile.IndexOf(".txt", startIndex);
                var userId = userFile.Substring(startIndex, endIndex - startIndex);
                if (userId == "Ids")
                {
                    continue;
                }
                userIds.Add(userId);
            }
            return userIds;
        }
    }
}
