using System;

namespace Todolist
{
    public class UpdateCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public int Index { get; set; }
        public string NewText { get; set; }

        public void Execute()
        {
            int taskIndex = Index - 1;
            TodoItem item = TodoList.GetItem(taskIndex);
            if (item != null)
            {
                string oldText = item.Text;
                item.UpdateText(NewText);
                Console.WriteLine($"Задача обновлена: '{oldText}' -> '{NewText}'");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный номер задачи");
            }
        }
    }
}