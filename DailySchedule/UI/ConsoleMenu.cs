using System;
using Task.Data;
using Task.Logic;

namespace Task.UI
{
    internal class ConsoleMenu
    {
        private DailySchedulerHelper _helper;

        public ConsoleMenu()
        {
            _helper = new DailySchedulerHelper();
        }

        public void Start()
        {
            ShowHelp();

            Command command;

            do
            {
                Console.Write("> ");

                string input = Console.ReadLine() ?? "";

                bool isValid = Enum.TryParse(input.Trim(), true, out command);

                if (!isValid)
                {
                    Console.WriteLine("Я не знаю такої команди.");
                    continue;
                }

                switch (command)
                {
                    case Command.Hello:
                        Console.WriteLine("Привіт!");
                        break;

                    case Command.Time:
                        _helper.CalculateWalkingTimePerDay();
                        break;

                    case Command.Help:
                        ShowHelp();
                        break;

                    case Command.Exit:
                        Console.WriteLine("Бот завершив роботу.");
                        break;
                }

            } while (command != Command.Exit);
        }

        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Доступні команди:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("hello - привітання");
            Console.WriteLine("time  - показати розпорядок");
            Console.WriteLine("help  - список команд");
            Console.WriteLine("exit  - завершити програму");

            Console.ResetColor();
        }
    }
}