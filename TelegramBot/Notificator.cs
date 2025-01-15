using Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot
{
    internal class Notificator
    {
        public static async Task Notification(ITelegramBotClient client, ChatId chatId, DateTime userTimeNow, DateTime notificationTime)
        {
            var timeDifference = notificationTime - userTimeNow;
            await Task.Factory.StartNew(async () =>
            {
                if (timeDifference.Ticks >= 0)
                {
                    await Task.Delay(timeDifference);
                }
                else
                {
                    await Task.Delay(notificationTime.AddDays(1) - userTimeNow);
                }
                var dataLoad = DataController.GetData(chatId.ToString());
                notificationTime = DateTime.Parse(dataLoad["NotificationTime"]).AddDays(1);
                userTimeNow = DateTime.Now.AddHours(-double.Parse(dataLoad["TimeDif"]));
                await Notification(client, chatId, userTimeNow, notificationTime);
                if ((userTimeNow - notificationTime).TotalMinutes < 1)
                {
                    await client.SendTextMessageAsync(chatId, GismeteoParser.Parse(dataLoad["City"]));
                    return;
                }
            });
        }
        public static async Task CreateUsersNotifications(TelegramBotClient _bot, List<string> Ids)
        {
            foreach (var item in Ids)
            {
                var data = DataController.GetData(item);
                if (data.ContainsKey("TimeDif") && data.ContainsKey("NotificationTime"))
                {
                    await Notification(_bot, item, DateTime.Now.AddHours(-double.Parse(data["TimeDif"])), DateTime.Parse(data["NotificationTime"]));
                }
            }
        }
    }
}
