using System;

namespace Todolist
{
    public class StatusCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public int Index { get; set; }
        public TodoStatus Status { get; set; }

        public void Execute()
        {
            int taskIndex = Index - 1;
            TodoItem item = TodoList.GetItem(taskIndex);
            if (item != null)
            {
                item.SetStatus(Status);
                Console.WriteLine($"Задача {Index} установлена в статус: {GetStatusText(Status)}");

                string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
                string todoFile = Path.Combine(dataDir, "todo.csv");
                FileManager.SaveTodos(TodoList, todoFile);
            }
            else
            {
                Console.WriteLine("Ошибка: неверный номер задачи");
            }
        }

        private string GetStatusText(TodoStatus status)
        {
            return status switch
            {
                TodoStatus.NotStarted => "Не начато",
                TodoStatus.InProgress => "В процессе",
                TodoStatus.Completed => "Выполнено",
                TodoStatus.Postponed => "Отложено",
                TodoStatus.Failed => "Провалено",
                _ => "Неизвестно"
            };
        }
    }
}