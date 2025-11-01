using System;

namespace Todolist
{
    public class DeleteCommand : ICommand
    {
        public TodoList TodoList { get; set; }
        public int Index { get; set; }

        public void Execute()
        {
            int taskIndex = Index - 1;
            TodoItem item = TodoList.GetItem(taskIndex);
            if (item != null)
            {
                string deletedText = item.Text;
                TodoList.Delete(taskIndex);
                Console.WriteLine($"Задача удалена: {deletedText}");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный номер задачи");
            }
        }
    }
}
