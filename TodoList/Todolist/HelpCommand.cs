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
  Флаги: --index (-i), --status (-s), --update-date (-d), --all (-a)
status <номер> <статус> - изменить статус задачи
  Статусы: notstarted, inprogress, completed, postponed, failed
delete <номер> - удалить задачу
update <номер> ""новый текст"" - изменить задачу
read <номер> - посмотреть полный текст задачи
exit - выйти из программы
".Trim());
        }
    }
}
