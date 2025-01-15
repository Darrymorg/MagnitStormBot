
namespace Support
{
    public static class BotTexts
    {
        public static string Start { get; } = @"Вас приветствует бот магнитных бурь
Бот позволяет получать и отслеживать прогнозы магнитных бурь в вашем городе!
Начать работу с приложением можно с помощью кнопки 'Меню' слева от строки ввода сообщения.

Команды бота:
/city - установить город, в котором интересуют магнитные бури
/storm - получить информацию о магнитных бурях сейчас
/notifications - настроить уведомления о магнитных бурях ";
        public static string CityQuestion { get; } = "Напишите в каком городе вас интересуют магнитные бури";
        public static string YouNotSetCity { get; } = "Вы еще не установили город, в котором вас интересуют магнитные бури. Напишите в каком городе вас интересуют магнитные бури?";
        public static string NotificationSettings { get; } = "Сейчас у вас установлены уведомления, если хотите их удалить воспользуйтесь командой /del , поменять часовой пояс /timezone, если хотите установить другое время - просто пришлите его в формате 00:00";
        public static string NotificationCityQuestion { get; } = "Сначала нужно установить город, в каком городе вас интересуют магнитные бури?";
        public static string NotificationTimeDifferenceSet { get; } = "Часовой пояс определен";
        public static string NotificationTimeQuestion { get; } = "Теперь пришлите время, в которе вам было бы удобно получать уведомления в формате 00:00";
        public static string NotificationTimeZoneQuestion { get; } = "Сколько у вас сейчас времени? Это нужно для определения часового пояса.Пришлите в формате 00:00";
        public static string CityNotFounded { get; } = "Город не найден!";
        public static string NotificationCitySet { get; } = "Город установлен";
        public static string CitySetted { get; } = "Город установлен.Теперь вы можете использовать команды \n/storm - чтобы получить прогноз\n/notifications - чтобы настроить время, в которое вы будете получать прогноз";
        public static string NotificationsDeleted { get; } = "Уведомления успешно удалены!";
        public static string NotificationsSetted { get; } = "Уведомления успешно настроены";
        public static string WrongTimeFormat { get; } = "Неверный формат времени";
        public static string TimeZoneChange { get; } = "Часовой пояс успешно установлен";
    }

}
