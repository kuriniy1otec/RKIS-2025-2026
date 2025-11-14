using System;
using System.IO;

namespace Todolist
{
    class Program
    {
        private static TodoList todoList;
        private static Profile userProfile;
        private static string dataDirectory;
        private static string profileFilePath;
        private static string todoFilePath;

        static void Main()
        {
            InitializeFilePaths();
            LoadData();
            RunTodoList();
        }

        static void InitializeFilePaths()
        {
            dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");
            profileFilePath = Path.Combine(dataDirectory, "profile.txt");
            todoFilePath = Path.Combine(dataDirectory, "todo.csv");
        }

        static void LoadData()
        {
            Console.WriteLine("Работу выполнили Гергель и Дайнеко 3832");
            Console.WriteLine();

            FileManager.EnsureDataDirectory(dataDirectory);

            userProfile = FileManager.LoadProfile(profileFilePath);
            if (userProfile == null)
            {
                CreateNewProfile();
            }
            else
            {
                Console.WriteLine($"Загружен профиль: {userProfile.GetInfo()}");
            }

            todoList = FileManager.LoadTodos(todoFilePath);

            Console.WriteLine();
        }

        static void CreateNewProfile()
        {
            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите год рождения: ");
            int birthYear = int.Parse(Console.ReadLine());

            userProfile = new Profile(firstName, lastName, birthYear);
            FileManager.SaveProfile(userProfile, profileFilePath);
            Console.WriteLine($"Создан новый профиль: {userProfile.GetInfo()}");
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
