using System;
using System.Collections.Generic;

namespace Todolist
{
    public class AddCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public string Input { get; set; }
        public bool IsMultiline { get; set; }

        public void Execute()
        {
            if (IsMultiline)
            {
                AddMultilineTask();
            }
            else
            {
                AddSingleLineTask();
            }
        }

        private void AddSingleLineTask()
        {
            string[] parts = Input.Split('"');
            if (parts.Length >= 2)
            {
                string task = parts[1];
                TodoItem newItem = new TodoItem(task);
                TodoList.Add(newItem);
                Console.WriteLine($"Задача добавлена: {task}");
            }
            else
            {
                Console.WriteLine("Ошибка: используйте формат add \"текст задачи\"");
            }
        }

        private void AddMultilineTask()
        {
            Console.WriteLine("Многострочный режим. Вводите задачи (для завершения введите 'end'):");

            List<string> lines = new List<string>();
            while (true)
            {
                string line = Console.ReadLine();
                if (line?.ToLower() == "end")
                    break;

                if (!string.IsNullOrEmpty(line))
                    lines.Add(line);
            }

            if (lines.Count > 0)
            {
                string task = string.Join("\n", lines);
                TodoItem newItem = new TodoItem(task);
                TodoList.Add(newItem);
                Console.WriteLine($"Добавлена многострочная задача ({lines.Count} строк)");
            }
            else
            {
                Console.WriteLine("Не добавлено ни одной строки");
            }
            string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            string todoFile = Path.Combine(dataDir, "todo.csv");
            FileManager.SaveTodos(TodoList, todoFile);
        }
    }
}

