namespace Todolist
{
    public class HelpCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine(@"
Доступные команды:
help - вывести список команд
profile - показать данные пользователя
add ""текст задачи"" - добавить задачу (однострочный режим)
add --multiline (-m) - добавить задачу (многострочный режим)
view [флаги] - показать задачи
  Флаги: 
    --index (-i) - показать номера
    --status (-s) - показать статус
    --update-date (-d) - показать дату
    --all (-a) - показать всю информацию
  Комбинации: view -is, view -isd, view --index --status и т.д.
done <номер> - отметить задачу выполненной
delete <номер> - удалить задачу
update <номер> ""новый текст"" - изменить задачу
read <номер> - посмотреть полный текст задачи
exit - выйти из программы
".Trim());
        }
    }
}