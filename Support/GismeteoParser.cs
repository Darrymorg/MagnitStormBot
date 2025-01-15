using System.Net;
using System.Text;

namespace Support
{
    public static class GismeteoParser
    {
        public static string Weather = null;
        public static string Parse(string city)
        {
            var proxy = new WebProxy("127.0.0.1:8888");
            var cookieContainer = new CookieContainer();
            var request = Uri.EscapeDataString($"{city}+гисметео");
            var address = $"https://yandex.ru/search/?text={request}&clid=2335348-29&suggest_reqid=1999943339706989270987789545853&win=652& HTTP/1.1";

            var getRequest = new GetRequest(address);
            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 YaBrowser/24.6.0.0 Safari/537.36";
            getRequest.Host = "yandex.ru";
            getRequest.Referer = "yandex.ru";
            //getRequest.Proxy = proxy;        

            getRequest.Headers.Add("downlink", "8.15");
            getRequest.Headers.Add("viewport-width", "1680");
            getRequest.Headers.Add("device-memory", "8");
            getRequest.Headers.Add("ect", "4g");
            getRequest.Headers.Add("rtt", "250");
            getRequest.Headers.Add("Serp-Rendering-Experiment", "snippet-with-header");
            getRequest.Headers.Add("Yandex-Video-Translation-Enabled", "1");
            getRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"YaBrowser\";v=\"24.6\", \"Not-A.Brand\";v=\"99\", \"Yowser\";v=\"2.5\"");
            getRequest.Headers.Add("sec-ch-ua-arch", "\"x86\"");
            getRequest.Headers.Add("sec-ch-ua-bitness", "\"64\"");
            getRequest.Headers.Add("sec-ch-ua-full-version", "\"24.6.4.580\"");
            getRequest.Headers.Add("sec-ch-ua-full-version-list", "\"Chromium\";v=\"124.0.6367.243\", \"YaBrowser\";v=\"24.6.4.580\", \"Not-A.Brand\";v=\"99.0.0.0\", \"Yowser\";v=\"2.5\"");
            getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            getRequest.Headers.Add("sec-ch-ua-model", "\"\"");
            getRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            getRequest.Headers.Add("sec-ch-ua-platform-version", "\"10.0.0\"");
            getRequest.Headers.Add("sec-ch-ua-wow64", "?0");
            getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            getRequest.Headers.Add("Sec-Fetch-Site", "none");
            getRequest.Headers.Add("Sec-Fetch-User", "?1");
            getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");

            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("_yasc", "aUmAin4FEmPh+2wJ0HOjKBdqLQl6umuDcgtYpIiCZOTDSLFIoKeEnLss26rydM1F/3559yAcz4I="));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("_ym_d", "1722932566"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("_ym_isad", "2"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("_ym_uid", "1722932565763072348"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("_ym_visorc", "b"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("bh", "ElAiQ2hyb21pdW0iO3Y9IjEyNCIsICJZYUJyb3dzZXIiO3Y9IjI0LjYiLCAiTm90LUEuQnJhbmQiO3Y9Ijk5IiwgIllvd3NlciI7dj0iMi41IhoFIng4NiIiDCIyNC42LjQuNTgwIioCPzAyAiIiOgkiV2luZG93cyJCCCIxMC4wLjAiSgQiNjQiUmciQ2hyb21pdW0iO3Y9IjEyNC4wLjYzNjcuMjQzIiwgIllhQnJvd3NlciI7dj0iMjQuNi40LjU4MCIsICJOb3QtQS5CcmFuZCI7dj0iOTkuMC4wLjAiLCAiWW93c2VyIjt2PSIyLjUiWgI/MGCcu8e1Bmoh3Mrh/wiS2KGxA5/P4eoD+/rw5w3r//32D/uRh9YH84EC"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("cycada", "x6FUM/Uu2JUhnDcQ85KX1IVIDcVlhxW58uj4yHIksRs="));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("gdpr", "0"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("i", "DBWxrej6Z34dmLWEIq2qI32AnCrE25gBMf50dIUX2qUhU9Vf3POj908d8bSCT8SS07u+34lQGV6xXnQ1uqSL71AzLOs="));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("is_gdpr_b", "CM6LYRDTiwIoAg=="));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("is_gdpr", "0"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("spravka", "dD0xNzIyOTM0NzAzO2k9MTc4LjcxLjY0LjEwMztEPUQwNDgxNjVGOTAyQ0E3REQ2QTRDOUJCRTAxNUQ5RUIyNjEyQ0JGQ0I3NTgzMkU3QUYyNkQ4QTQxNTQyMkQ4NTIwNjRGOTNGOUQwNEI3QjI1QTMzMjNCRjY2RDE2MDlGRTJDQjdCRDk0OTYzRjNBQTUzMDlGMEI2RjVGMENEN0EwMDAzODA2MzRGNzIwNDZEQjU4OTk3NDkzNEQ4RjI5QkY7dT0xNzIyOTM0NzAzNzQ0NjI5NTI5O2g9YjRkYjljYTZiYjk1YzA1YWI3N2FkNmFmNjc1MzNjOTM="));
            //cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("", ""));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("yandexuid", "6927084301722932556"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("yashr", "8602046861722932556"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("ymex", "2038292564.yrts.1722932564"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("yp", "2038294706.pcs.0#1725613106.hdrc.1#1723539512.szm.1:1680x1050:1663x934"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("ys", "wprid.1722934703767274-15120758698459614932-balancer-l7leveler-kubr-yp-klg-176-BAL"));
            cookieContainer.Add(new Uri("https://yandex.ru"), new Cookie("yuidss", "6927084301722932556"));

            getRequest.Run(cookieContainer);


            if (getRequest.Response.Contains("https://www.gismeteo.ru"))
            {
                var strStart = getRequest.Response.IndexOf("https://www.gismeteo.ru");
                var strEnd = getRequest.Response.IndexOf(";", strStart);

                address = getRequest.Response.Substring(strStart, strEnd - strStart);


                getRequest = new GetRequest(address);
                getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
                getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 YaBrowser/24.6.0.0 Safari/537.36";
                getRequest.Referer = "https://www.gismeteo.ru/";
                getRequest.Host = "www.gismeteo.ru";
                //getRequest.Proxy = proxy;

                getRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"YaBrowser\";v=\"24.6\", \"Not-A.Brand\";v=\"99\", \"Yowser\";v=\"2.5\"");
                getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
                getRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                getRequest.Headers.Add("Sec-Fetch-Dest", "document");
                getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
                getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
                getRequest.Headers.Add("Sec-Fetch-User", "?1");
                getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");

                getRequest.Run(cookieContainer);

                var descriptions = new string[8];
                var gmActivities = new string[8];
                if (getRequest.Response.Contains("temperature-value value="))
                {
                    var sb = new StringBuilder();
                    var startIndex = 0;
                    var endIndex = 0;
                    var startDescIndx = 0;
                    var endDescIndx = 0;
                    var time = 0;
                    for (int i = 0; i < 14; i++)
                    {
                        var description = "";   

                        
                        startIndex = getRequest.Response.IndexOf("temperature-value value=", endIndex);
                        startIndex = getRequest.Response.IndexOf("\"", startIndex) + 1;
                        endIndex = getRequest.Response.IndexOf("\"", startIndex);
                        var value = getRequest.Response.Substring(startIndex, endIndex - startIndex);
                        switch (i)
                        {
                            case 0:
                                sb.Append($"Сейчас {value},");
                                break;
                            case 1:
                                startDescIndx = getRequest.Response.IndexOf("data-tooltip=", endDescIndx);
                                startDescIndx = getRequest.Response.IndexOf("\"", startDescIndx) + 1;
                                endDescIndx = getRequest.Response.IndexOf("\"", startDescIndx);
                                description = getRequest.Response.Substring(startDescIndx, endDescIndx - startDescIndx);
                                sb.Append($" ощущается как {value} - {description}");
                                break;
                            case 2:
                                sb.Append($"\nСегодня от {value}");
                                break;
                            case 3:
                            case 5:
                                startDescIndx = getRequest.Response.IndexOf("data-tooltip=", endDescIndx);
                                startDescIndx = getRequest.Response.IndexOf("\"", startDescIndx) + 1;
                                endDescIndx = getRequest.Response.IndexOf("\"", startDescIndx);
                                description = getRequest.Response.Substring(startDescIndx, endDescIndx - startDescIndx);
                                sb.Append($" до {value} - {description}");
                                break;
                            case 4:
                                sb.Append($"\nЗавтра от {value}");
                                break;
                            default:
                                if(i == 6)
                                {
                                    sb.Append("\nСегодня по часам");
                                }
                                startDescIndx = getRequest.Response.IndexOf("data-tooltip=", endDescIndx);
                                startDescIndx = getRequest.Response.IndexOf("\"", startDescIndx) + 1;
                                endDescIndx = getRequest.Response.IndexOf("\"", startDescIndx);
                                description = getRequest.Response.Substring(startDescIndx, endDescIndx - startDescIndx);
                                if (int.Parse(value) > 0)
                                {
                                    sb.Append($"\n{time}:00 - (+{value}) - {description}");
                                }
                                else
                                {
                                    sb.Append($"\n{time}:00 - ({value}) - {description}");
                                }
                                time += 3;
                                break;
                        }                        
                    }
                    Weather = sb.ToString();
                }
                if (getRequest.Response.Contains("<span>Г/м активность, Кп-индекс</span>"))
                {
                    strStart = getRequest.Response.IndexOf("<span>Г/м активность, Кп-индекс</span>");
                    strStart = getRequest.Response.IndexOf("tip=", strStart) + 4;
                    strEnd = getRequest.Response.IndexOf(">", strStart);

                    descriptions[0] = getRequest.Response.Substring(strStart, strEnd - strStart);

                    strStart = getRequest.Response.IndexOf("<div class=", strEnd);
                    strStart = getRequest.Response.IndexOf(">", strStart);
                    strEnd = getRequest.Response.IndexOf("<", strStart);

                    gmActivities[0] = getRequest.Response.Substring(strStart, strEnd - strStart).Trim(' ', '>').Replace(" ", "").Replace("\n", "");

                    for (int i = 1; i < 8; i++)
                    {
                        strStart = getRequest.Response.IndexOf("tip=", strStart) + 4;
                        strEnd = getRequest.Response.IndexOf(">", strStart);

                        descriptions[i] = getRequest.Response.Substring(strStart, strEnd - strStart);

                        strStart = getRequest.Response.IndexOf("<div class=", strEnd);
                        strStart = getRequest.Response.IndexOf(">", strStart);
                        strEnd = getRequest.Response.IndexOf("<", strStart);

                        gmActivities[i] = getRequest.Response.Substring(strStart, strEnd - strStart).Trim(' ', '>').Replace(" ", "").Replace("\n", "");
                    }

                    strStart = getRequest.Response.IndexOf("погода") + 6;
                    strEnd = getRequest.Response.IndexOf(",", strStart);
                    var head = "Магнитные бури" + getRequest.Response.Substring(strStart, strEnd - strStart);

                    var sb = new StringBuilder();
                    sb.Append(head + "\n\n");
                    for (int i = 0; i < 4; i++)
                    {
                        sb.Append($"0{i * 3}:00 - {gmActivities[i]} - {descriptions[i]}\n");
                    }
                    for (int i = 4; i < 8; i++)
                    {
                        sb.Append($"{i * 3}:00 - {gmActivities[i]} - {descriptions[i]}\n");
                    }
                    return sb.ToString();
                }
                else
                {
                    return "Город не найден, попробуйте еще раз";
                }
            }
            else
            {
                return "Город не найден, попробуйте еще раз";
            }
        }
    }
}
