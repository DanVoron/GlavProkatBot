using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
class Program
{
    // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
    private static ITelegramBotClient _botClient;

    // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
    private static ReceiverOptions _receiverOptions;

    static async Task Main()
    {

        _botClient = new TelegramBotClient("6630241278:AAGXS-UHFu68ao9iaca1c57GKwBimPyyt_o"); // Присваиваем нашей переменной значение, в параметре передаем Token, полученный от BotFather
        _receiverOptions = new ReceiverOptions // Также присваем значение настройкам бота
        {
            AllowedUpdates = new[] // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
            {
                UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
                UpdateType.CallbackQuery // Inline кнопки
            },
            // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
            // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
            ThrowPendingUpdates = true,
        };

        using var cts = new CancellationTokenSource();

        // UpdateHander - обработчик приходящих Update`ов
        // ErrorHandler - обработчик ошибок, связанных с Bot API
        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); // Запускаем бота

        var me = await _botClient.GetMeAsync(); // Создаем переменную, в которую помещаем информацию о нашем боте.
        Console.WriteLine($"{me.FirstName} запущен!");

        await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
{
            new KeyboardButton[] { "📚Каталог", "🗺️Как добраться" },
            new KeyboardButton[] { "📱Контакты", "📝Отзывы" },
    })
        {
            ResizeKeyboard = true
        };

        ReplyKeyboardMarkup replyKeyboardMarkup2 = new(new[]
    {
            new KeyboardButton[] { "📞Телефон", "🌐Сайт" },
            new KeyboardButton[] { "🌍Вк", "📷Инстаграм"},
            new KeyboardButton[] { "⬅️Назад" }
            })
        {
            ResizeKeyboard = true
        };

        ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
    {
            new KeyboardButton[] { "☀️Летний инвентарь" },
            new KeyboardButton[] { "🎿Зимний инвентарь"},
            new KeyboardButton[] { "📝В меню"},
            })
        {
            ResizeKeyboard = true
        };

        // Здесь Инлайн кейборд
        var inlineKeyboard2 = new InlineKeyboardMarkup(
        new List<InlineKeyboardButton[]>() // здесь создаем лист (массив), который содрежит в себе массив из класса кнопок
        {
        // Каждый новый массив - это дополнительные строки,
        // а каждая дополнительная строка (кнопка) в массиве - это добавление ряда

        new InlineKeyboardButton[] // тут создаем массив кнопок
        {
            InlineKeyboardButton.WithUrl("Яндекс карты","https://yandex.ru/maps/org/glavprokat/220246121964/?l=stv%2Csta&ll=30.378478%2C59.924227&mode=search&sctx=ZAAAAAgBEAAaKAoSCS4fSUkPuUJAEadYNQhz6UtAEhIJtCJqos9HuT8R2NR5VPzfoT8iBgABAgMEBSgKOABA6a4HSAFqAnJ1nQHNzEw9oAEAqAEAvQELcKsFwgEG7LvEvbQG6gEA8gEA%2BAEAggIOZ2xhdnByb2thdCBzcGKKAgCSAgCaAgxkZXNrdG9wLW1hcHM%3D&sll=30.378478%2C59.924227&source=serp_navig&sspn=0.003066%2C0.000884&text=glavprokat%20spb&z=18.88"),
        },
        new InlineKeyboardButton[]
          {
            InlineKeyboardButton.WithCallbackData("Посмотреть как пройти к зданию")
        } 
        }); ;
        var inlineKeyboard = new InlineKeyboardMarkup(
        new List<InlineKeyboardButton[]>() // здесь создаем лист (массив), который содрежит в себе массив из класса кнопок
        {
        // Каждый новый массив - это дополнительные строки,
        // а каждая дополнительная строка (кнопка) в массиве - это добавление ряда

        new InlineKeyboardButton[] // тут создаем массив кнопок
        {
            InlineKeyboardButton.WithCallbackData("Назад"),
            InlineKeyboardButton.WithCallbackData("Далее"),

        },

        new InlineKeyboardButton[]
        {
            InlineKeyboardButton.WithUrl("Перейти на сайт", "https://glavprokatspb.ru/"),
        },
        });
        // Обязательно ставим блок try-catch, чтобы наш бот не "падал" в случае каких-либо ошибок
        try
        {
            // Сразу же ставим конструкцию switch, чтобы обрабатывать приходящие Update
            switch (update.Type)
            {
                case UpdateType.Message:
                    {

                        // эта переменная будет содержать в себе все связанное с сообщениями
                        var message = update.Message;

                        // Only process text messages
                        if (message.Text is not { } messageText)
                            return;
                        var command = message.Text.ToLower().Split(' ');
                        var command1 = command[0];
                        var chatId = message.Chat;
                        // From - это от кого пришло сообщение (или любой другой Update)
                        var user = message.From;
                        var chat = message.Chat;


                        switch (message.Type)
                        {
                            // Тут понятно, текстовый тип
                            case MessageType.Text:
                                {
                                    
                                    Console.WriteLine($"Received a '{message.Text}' message in chat {user.FirstName} ({user.Id}).");
                                    switch (command1)
                                    {
                                        case "/start" or "команды"or "📝в":
                                            Message sentMessage = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Добро пожаловать в магазин ГлавПрокат!",
                                            replyMarkup: replyKeyboardMarkup,
                                            cancellationToken: cancellationToken);
                                            return;
                                        //если не добавлен в бд добавить

                                        case "📚каталог":
                                            Message sentMessage7 = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Выбирите тип инвентаря",
                                            replyMarkup: replyKeyboardMarkup3,
                                            cancellationToken: cancellationToken);
                                            return;

                                        case "☀️летний":
                                            Message sentMessage11 = await botClient.SendPhotoAsync(
                                                chat.Id,
                                                
                                        photo: InputFile.FromUri("https://glavprokatspb.ru/wp-content/uploads/2023/05/6130418523.jpg"),
                                        caption: "Вот это крутая палка",
                                        replyMarkup: inlineKeyboard
                                        );
                                            return; 

                                        case "📝отзывы":
                                            Message Message5 = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Всё круто! \nПо личному опыту говорю.",
                                            cancellationToken: cancellationToken);
                                            return;

                                        case "⬅️назад":
                                            Message Message = await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Возращение в меню. ",
                                        replyMarkup: replyKeyboardMarkup,
                                        cancellationToken: cancellationToken);
                                            return;

                                        case "📷инстаграм":
                                            Message message8 = await botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "Нажмите на ссылку ниже, чтобы посмотреть на наш спортивный и походный инвентарь в инстаграме\\!👇",
                                                parseMode: ParseMode.MarkdownV2,
                                                disableNotification: true,
                                                replyMarkup: new InlineKeyboardMarkup(
                                                    InlineKeyboardButton.WithUrl(
                                                        text: "Инстаграм",
                                                        url: "https://www.instagram.com/glavprokat_spb/")),
                                                cancellationToken: cancellationToken);
                                            return;

                                        case "🗺️как":
                                            Message message9 = await botClient.SendVenueAsync(
                                                chatId: chatId,
                                                latitude: 59.924246f,
                                                longitude: 30.378737f,
                                                title: "Главпрокат",
                                                address: "Тележная улица, 37Ж, Санкт-Петербург, 191167",
                                                replyMarkup: inlineKeyboard2,
                                                cancellationToken: cancellationToken);
                                            return;

                                        case "🌍вк":
                                            Message message13 = await botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "🌄 Нажмите на ссылку ниже, чтобы узнать больше и об спортивном и походном инвентаре в Вконтакте\\!👇",
                                                parseMode: ParseMode.MarkdownV2,
                                                disableNotification: true,
                                                replyMarkup: new InlineKeyboardMarkup(
                                                    InlineKeyboardButton.WithUrl(
                                                        text: "Вконтакте",
                                                        url: "https://vk.com/prokat_v_spb")),
                                                cancellationToken: cancellationToken);
                                            return;

                                        case "📞телефон":
                                            Message message4 = await botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "📞 Позвоните нам по номеру XXX-XXX-XXXX, чтобы получить помощь и перейти к аренде спортивного и походного инвентаря",
                                                cancellationToken: cancellationToken);
                                            return;

                                        case "🌐сайт":
                                            Message message5 = await botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "🌄🎒 Арендуйте спортивный и походный инвентарь для своих приключений на природе, посетите наш сайт и ознакомьтесь с нашим широким ассортиментом",
                                                parseMode: ParseMode.MarkdownV2,
                                                disableNotification: true,
                                                replyMarkup: new InlineKeyboardMarkup(
                                                    InlineKeyboardButton.WithUrl(
                                                        text: "ГЛАВПРОКАТ",
                                                        url: "https://glavprokatspb.ru/")),
                                                cancellationToken: cancellationToken);
                                            return;

                                        case "📱контакты":
                                            Message sentMessage2 = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Здравствуйте! Будем рады ответить на ваши вопросы.",
                                            replyMarkup: replyKeyboardMarkup2,
                                            cancellationToken: cancellationToken);
                                            return;

                                        case "🎿зимний":
                                            Message sentMessage12 = await botClient.SendPhotoAsync(
                                                chat.Id,
                                                photo: InputFile.FromUri("https://glavprokatspb.ru/wp-content/uploads/2020/12/photo_2020-12-02_11-20-27.jpg"),
                                                caption: "Вот это крутые лыжи",
                                                replyMarkup: inlineKeyboard
                                                ); 
                                            return;
                                        case "ggez":
                                            Message sentMessage10 = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Клавиатура убрана, /start для возрата",
                                            replyMarkup: new ReplyKeyboardRemove(),
                                            cancellationToken: cancellationToken);
                                            return;

                                        default:
                                            Message Message3 = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Введена некорректная команда",
                                            cancellationToken: cancellationToken);
                                            return;

                                    }
                                    
                                }
                            // Добавил default , чтобы показать вам разницу типов Message
                            default:
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "Используй только текст!");
                                    return;
                                }
                        }
                    //ТУТ КЕЙС КОНЧАЕТСЯ
                    }
                case UpdateType.CallbackQuery:
                    {
                        // Переменная, которая будет содержать в себе всю информацию о кнопке, которую нажали
                        var callbackQuery = update.CallbackQuery;

                        // Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.
                        var user = callbackQuery.From;

                        // Выводим на экран нажатие кнопки
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

                        // Вот тут нужно уже быть немножко внимательным и не путаться!
                        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
                        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
                        var chat = callbackQuery.Message.Chat;
                        
                        // Добавляем блок switch для проверки кнопок
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "Назад":
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Вы нажали на {callbackQuery.Data}");
                                    return;
                                }

                            case "Далее":
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Вы нажали на {callbackQuery.Data}");
                                    return;
                                }
                            case "Посмотреть как пройти к зданию":
                                {
                                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                                    Message[] messages = await botClient.SendMediaGroupAsync(
                                        chatId: chat.Id,
                                        media: new IAlbumInputMedia[]
                                        {
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg")),
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg")),
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg")),
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg")),
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg")),
                                                    new InputMediaPhoto(
                                                        InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg")),
                                        },
                                        cancellationToken: cancellationToken);
                                    return;
                                }
                        }

                        return;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}