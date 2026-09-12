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

            while (true)
            {
                Console.Write("> ");

                string command =
                    Console.ReadLine() ?? "";

                command =
                    command.Trim().ToLower();

                switch (command)
                {
                    case "hello":
                        Console.WriteLine(
                            "Привіт!"
                        );
                        break;

                    case "time":
                        _helper.CalculateWalkingTimePerDay();
                        break;

                    case "help":
                        ShowHelp();
                        break;

                    case "exit":
                        Console.WriteLine(
                            "Бот завершив роботу."
                        );
                        return;

                    default:
                        Console.WriteLine(
                            "Я не знаю такої команди."
                        );
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            Console.ForegroundColor =
                ConsoleColor.Yellow;

            Console.WriteLine(
                "Доступні команди:"
            );

            Console.ForegroundColor =
                ConsoleColor.Blue;

            Console.WriteLine(
                "hello - привітання"
            );

            Console.WriteLine(
                "time  - показати розпорядок"
            );

            Console.WriteLine(
                "help  - список команд"
            );

            Console.WriteLine(
                "exit  - завершити програму"
            );

            Console.ResetColor();
        }
    }
}