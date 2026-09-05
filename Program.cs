namespace Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowHelp();
            initDailyScheduler();
        }

        private static void initDailyScheduler()
        {
            DailySchedulerHelper helper = new DailySchedulerHelper();
         
            while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case "hello":
                        Console.WriteLine("Привіт!");
                        break;

                    case "time":
                        helper.calculateWalkingTimePerDay();
                        break;

                    case "helo":
                        Console.WriteLine("Доступні команди:");
                        Console.WriteLine("hello - привітання");
                        Console.WriteLine("time  - показати час");
                        Console.WriteLine("exit  - завершити програму");
                        break;

                    case "exit":
                        Console.WriteLine("Бот завершив роботу.");
                        return;

                    default:
                        Console.WriteLine("Я не знаю такої команди.");
                        break;
                }
            }
        }
        static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("List of commands");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("help");
            Console.WriteLine("add");
            Console.WriteLine("show");
            Console.WriteLine("exit");
            Console.ResetColor();
        }
    }
}
