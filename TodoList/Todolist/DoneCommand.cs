using System;

namespace Todolist
{
    public class DoneCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public int Index { get; set; }

        public void Execute()
        {
            int taskIndex = Index - 1;
            TodoItem item = TodoList.GetItem(taskIndex);
            if (item != null)
            {
                item.MarkDone();
                Console.WriteLine($"Задача {Index} отмечена как выполненная");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный номер задачи");
            }
            string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            string todoFile = Path.Combine(dataDir, "todo.csv");
            FileManager.SaveTodos(TodoList, todoFile);
        }
    }
}
