namespace Todolist
{
    class Program
    {
        private static TodoList todoList;
        private static Profile userProfile;

        static void Main()
        {
            InitializeUser();
            InitializeTodoList();
            RunTodoList();
        }

        static void InitializeUser()
        {
            Console.WriteLine("Работу выполнили Дайнеко и Гергель 3832");
            Console.WriteLine();

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите год рождения: ");
            int birthYear = int.Parse(Console.ReadLine());

            userProfile = new Profile(firstName, lastName, birthYear);
            Console.WriteLine($"Добавлен пользователь: {userProfile.GetInfo()}");
            Console.WriteLine();
        }

        static void InitializeTodoList()
        {
            todoList = new TodoList();
        }

        static void RunTodoList()
        {
            while (true)
            {
                Console.Write("Введите команду (help - список команд): ");
                string input = Console.ReadLine();

                ICommand command = CommandParser.Parse(input, todoList, userProfile);
                command.Execute();

                Console.WriteLine();
            }
        }
    }
}

