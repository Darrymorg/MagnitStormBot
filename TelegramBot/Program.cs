using Newtonsoft.Json;
using Support;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using TelegramBot;

internal class Program
{
    private static List<string> Ids = new List<string>();
    private const string PASSWORD = "тут ваш пароль";
    private static async Task Main(string[] args) 
    {
        var token = "Тут должен быть ваш токен";
        var bot = new Host(token);
        var _bot = bot.Run();
        bot.OnMessage += OnMessage;

        Ids = DataController.GetData();
        await Notificator.CreateUsersNotifications(_bot, Ids);
        Console.ReadLine();
    }
    private static void OnMessage(ITelegramBotClient client, Update update)
    {
        if (update.Message?.Text != null)
        {
            if (update.Message.Text.StartsWith("/"))
            {
                CommandHandler(client, update);
            }
            else if (!update.Message.Text.StartsWith("/") && DataController.GetData(update.Message.Chat.Id.ToString())["LastMessage"] != "")
            {
                Console.WriteLine("onMessage:LastMessage");
                LastMessageHandler(client, update);
            }
        }
        #region PaymentsTest
        ////Console.WriteLine(update.PreCheckoutQuery == null);
        //if (update.Message?.Text != null)
        //{
        //    if (update.Message.Text.StartsWith("/"))
        //    {
        //        CommandHandler(client, update);
        //    }
        //    /*else if (update.Message.Invoice.Title == "title")
        //    {
        //        Console.WriteLine("ALL GOOD");
        //    }*/
        //    else if (!update.Message.Text.StartsWith("/") && DataController.GetData(update.Message.Chat.Id.ToString())["LastMessage"] != "")
        //    {
        //        Console.WriteLine("onMessage:LastMessage");//TODO: убрать
        //        LastMessageHandler(client, update);
        //    }
        //}
        ///*else if (update.PreCheckoutQuery != null)
        //{
        //    client.AnswerPreCheckoutQueryAsync(update.PreCheckoutQuery.Id);
        //}*/
        #endregion
    }
    private async static void CommandHandler(ITelegramBotClient client, Update update)
    {
        var userInfo = DataController.GetData(update.Message?.Chat.Id.ToString() ?? "1");
        DataController.WriteData(userInfo, update.Message.Chat.Id);
        switch (update.Message?.Text)
        {
            case "/start":
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.Start);
                break;
            case "/weather":
                userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                if (userInfo.TryGetValue("City", out string? value) && value != "")
                {
                    GismeteoParser.Parse(userInfo?["City"] ?? "12123123123");
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, GismeteoParser.Weather);
                }
                else
                {
                    userInfo["LastMessage"] = "/weather";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.YouNotSetCity);
                }
                break;
            case "/city":
                userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                userInfo["LastMessage"] = "/city";
                DataController.WriteData(userInfo, update.Message.Chat.Id);
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.CityQuestion);
                break;
            case "/storm":
                userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                if (userInfo.TryGetValue("City", out value) && value != "")
                {
                    var gismeteoData = GismeteoParser.Parse(userInfo?["City"] ?? "12123123123");
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, gismeteoData);
                    // await client.SendInvoiceAsync(update.Message.Chat.Id, "title", "desc", "123", "401643678:TEST:9b56724a-75d2-4673-ba56-2a933fb8c4fc", "RUB", [new LabeledPrice("label", 1000)]);
                }
                else
                {
                    userInfo["LastMessage"] = "/storm";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.YouNotSetCity);
                }
                break;
            case "/notifications":
                userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                Console.WriteLine("getdata:" + userInfo);
                userInfo["LastMessage"] = "/notifications";
                DataController.WriteData(userInfo, update.Message.Chat.Id);

                if (userInfo.TryGetValue("NotificationTime", out value) && value != "")
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationSettings);
                }
                else if (userInfo.TryGetValue("City", out value) && value == "" && userInfo.TryGetValue("NotificationTime", out value) && value == "")
                {
                    userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                    userInfo["LastMessage"] = "/notifications";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationCityQuestion);
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationTimeZoneQuestion);
                }
                break;
            case "/del":
                userInfo["NotificationTime"] = "";
                userInfo["TimeDif"] = "";
                userInfo["LastMessage"] = "";
                DataController.WriteData(userInfo, update.Message.Chat.Id);

                Ids.Remove($"{update.Message.Chat.Id}");
                DataController.WriteIds(Ids);
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationsDeleted);
                break;
            case "/timezone":
                userInfo["LastMessage"] = "/timezone";
                DataController.WriteData(userInfo, update.Message.Chat.Id);
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationTimeZoneQuestion);
                break;
            case "/msg":
                userInfo = DataController.GetData(update.Message.Chat.Id.ToString());
                userInfo["LastMessage"] = "/msg";
                DataController.WriteData(userInfo, update.Message.Chat.Id);
                await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Введите пароль");
                break;
        }
    }
    private async static void LastMessageHandler(ITelegramBotClient client, Update update)
    {
        var userInfo = DataController.GetData(update.Message?.Chat.Id.ToString() ?? "1");
        switch (userInfo["LastMessage"])
        {
            case "/city":
                var gismeteoData = GismeteoParser.Parse(update.Message?.Text ?? "asb12as");
                if (gismeteoData != "Город не найден, попробуйте еще раз")
                {
                    userInfo["LastMessage"] = update.Message?.Text ?? "not text";
                    userInfo["City"] = update.Message?.Text ?? "not text";
                    DataController.WriteData(userInfo, update.Message?.Chat.Id ?? 0);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.CitySetted);
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.CityNotFounded);
                }
                break;
            case "/weather":
            case "/storm":
                gismeteoData = GismeteoParser.Parse(update.Message?.Text ?? "not text");
                if (gismeteoData != "Город не найден, попробуйте еще раз")
                {
                    if (userInfo["LastMessage"] == "/storm")
                    {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, gismeteoData);
                    }
                    else if (userInfo["LastMessage"] == "/weather")
                    {
                        Console.WriteLine("WEATHER LAST");
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, GismeteoParser.Weather);
                    }
                    userInfo["LastMessage"] = update.Message?.Text ?? "not text";
                    userInfo["City"] = update.Message?.Text ?? "not text";
                    DataController.WriteData(userInfo, update.Message?.Chat.Id ?? 0);
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.CityNotFounded);
                }
                break;
            case "/notifications":
                if (userInfo.TryGetValue("City", out string? value) && value == "")
                {
                    gismeteoData = GismeteoParser.Parse(update.Message?.Text ?? "not text");
                    if (gismeteoData != "Город не найден, попробуйте еще раз")
                    {
                        userInfo["City"] = update.Message?.Text ?? "not text";
                        DataController.WriteData(userInfo, update.Message?.Chat.Id ?? 0);
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationCitySet);
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationTimeZoneQuestion);
                        return;
                    }
                    else
                    {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.CityNotFounded);
                    }
                }
                else
                {
                    var userSendCorrectTime = DateTime.TryParse(update.Message?.Text, out DateTime notificationTime);
                    if (userSendCorrectTime)
                    {
                        if (userInfo.TryGetValue("TimeDif", out value) && value == "")
                        {
                            userInfo["TimeDif"] = DateTime.Now.Subtract(notificationTime).TotalHours.ToString();
                            DataController.WriteData(userInfo, update.Message.Chat.Id);
                            await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationTimeDifferenceSet);
                            await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationTimeQuestion);
                        }
                        else
                        {
                            userInfo["LastMessage"] = "";
                            userInfo["NotificationTime"] = notificationTime.ToShortTimeString();
                            var userTimeNow = DateTime.Now.AddHours(-double.Parse(userInfo["TimeDif"]));
                            await Notificator.Notification(client, update.Message.Chat.Id, userTimeNow, notificationTime);
                            DataController.WriteData(userInfo, update.Message.Chat.Id);
                            if (!Ids.Contains($"{update.Message.Chat.Id}"))
                            {
                                Ids.Add($"{update.Message.Chat.Id}");
                            }
                            var data = JsonConvert.SerializeObject(Ids);
                            DataController.WriteIds(Ids);
                            await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.NotificationsSetted);
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.WrongTimeFormat);
                    }
                }
                break;
            case "/timezone":
                if (DateTime.TryParse(update.Message?.Text, out DateTime timezone))
                {
                    userInfo["LastMessage"] = "";
                    userInfo["TimeDif"] = DateTime.Now.Subtract(timezone).TotalHours.ToString();
                    var userTimeNow = DateTime.Now.AddHours(-double.Parse(userInfo["TimeDif"]));
                    await Notificator.Notification(client, update.Message.Chat.Id, userTimeNow, timezone);
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.TimeZoneChange);
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, BotTexts.WrongTimeFormat);
                }
                break;
            case "/msg":
                if (update.Message.Text == PASSWORD)
                {
                    userInfo["LastMessage"] = "/msg right";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Введите сообщение");
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Неверный пароль!");
                }
                break;
            case "/msg right":
                if (update.Message.Text != "/cancel")
                {
                    userInfo["LastMessage"] = "";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                    await client.SendTextMessageAsync(update.Message?.Chat.Id ?? 0, "Рассылаю сообщения!");
                    var usersId =DataController.GetAllUsersId();
                    foreach (var userId in usersId)
                    {
                        await client.SendTextMessageAsync(userId, update.Message.Text);
                    }
                }
                else
                {
                    userInfo["LastMessage"] = "";
                    DataController.WriteData(userInfo, update.Message.Chat.Id);
                }
                break;
        }
    }
}