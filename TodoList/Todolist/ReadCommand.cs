using System;

namespace Todolist
{
    public class ReadCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public int Index { get; set; }

        public void Execute()
        {
            int taskIndex = Index - 1;
            TodoItem item = TodoList.GetItem(taskIndex);
            if (item != null)
            {
                Console.WriteLine($"=== Задача {Index} ===");
                Console.WriteLine(item.GetFullInfo());
            }
            else
            {
                Console.WriteLine("Ошибка: неверный номер задачи");
            }
        }
    }
}