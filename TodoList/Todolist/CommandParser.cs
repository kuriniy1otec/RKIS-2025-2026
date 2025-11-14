using System;

namespace Todolist
{
    public static class CommandParser
    {
        public static ICommand Parse(string input, TodoList todoList, Profile profile)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new UnknownCommand();

            string lowerInput = input.ToLower();

            if (lowerInput == "help")
                return new HelpCommand();

            if (lowerInput == "profile")
                return new ProfileCommand { Profile = profile };

            if (lowerInput == "exit")
                return new ExitCommand();

            if (lowerInput.StartsWith("add"))
                return ParseAddCommand(input, todoList);

            if (lowerInput.StartsWith("view"))
                return ParseViewCommand(input, todoList);

            if (lowerInput.StartsWith("status "))
                return ParseStatusCommand(input, todoList);

            if (lowerInput.StartsWith("delete "))
                return ParseDeleteCommand(input, todoList);

            if (lowerInput.StartsWith("update "))
                return ParseUpdateCommand(input, todoList);

            if (lowerInput.StartsWith("read "))
                return ParseReadCommand(input, todoList);

            return new UnknownCommand();
        }

        private static ICommand ParseAddCommand(string input, TodoList todoList)
        {
            return new AddCommand
            {
                TodoList = todoList,
                Input = input,
                IsMultiline = ContainsFlag(input, "--multiline", "-m")
            };
        }

        private static ICommand ParseViewCommand(string input, TodoList todoList)
        {
            return new ViewCommand
            {
                TodoList = todoList,
                Input = input,
                ShowIndex = ContainsFlag(input, "--index", "-i"),
                ShowStatus = ContainsFlag(input, "--status", "-s"),
                ShowDate = ContainsFlag(input, "--update-date", "-d"),
                ShowAll = ContainsFlag(input, "--all", "-a")
            };
        }

        private static ICommand ParseStatusCommand(string input, TodoList todoList)
        {
            string[] parts = input.Split(' ');
            if (parts.Length >= 3)
            {
                if (int.TryParse(parts[1], out int index))
                {
                    TodoStatus status = ParseStatus(parts[2]);
                    if (status != (TodoStatus)(-1))
                    {
                        return new StatusCommand
                        {
                            TodoList = todoList,
                            Index = index,
                            Status = status
                        };
                    }
                }
            }

            return new UnknownCommand("Ошибка: используйте формат status <номер> <статус>");
        }

        private static TodoStatus ParseStatus(string statusText)
        {
            return statusText.ToLower() switch
            {
                "notstarted" or "not" => TodoStatus.NotStarted,
                "inprogress" or "in" or "progress" => TodoStatus.InProgress,
                "completed" or "done" or "complete" => TodoStatus.Completed,
                "postponed" or "post" => TodoStatus.Postponed,
                "failed" or "fail" => TodoStatus.Failed,
                _ => (TodoStatus)(-1)
            };
        }

        private static ICommand ParseDeleteCommand(string input, TodoList todoList)
        {
            if (int.TryParse(input.Substring(7).Trim(), out int index))
            {
                return new DeleteCommand
                {
                    TodoList = todoList,
                    Index = index
                };
            }

            return new UnknownCommand("Ошибка: используйте формат delete <номер>");
        }

        private static ICommand ParseUpdateCommand(string input, TodoList todoList)
        {
            string[] parts = input.Split('"');
            if (parts.Length >= 2)
            {
                string numberPart = parts[0].Substring(7).Trim();
                if (int.TryParse(numberPart, out int index))
                {
                    return new UpdateCommand
                    {
                        TodoList = todoList,
                        Index = index,
                        NewText = parts[1]
                    };
                }
            }

            return new UnknownCommand("Ошибка: используйте формат update <номер> \"новый текст\"");
        }

        private static ICommand ParseReadCommand(string input, TodoList todoList)
        {
            if (int.TryParse(input.Substring(5).Trim(), out int index))
            {
                return new ReadCommand
                {
                    TodoList = todoList,
                    Index = index
                };
            }

            return new UnknownCommand("Ошибка: используйте формат read <номер>");
        }

        private static bool ContainsFlag(string input, string longFlag, string shortFlag)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains(longFlag))
                return true;

            if (lowerInput.Contains("-") && shortFlag.Length == 2)
            {
                string shortFlagChar = shortFlag[1].ToString();

                int dashIndex = lowerInput.IndexOf(" -");
                if (dashIndex >= 0)
                {
                    string afterDash = lowerInput.Substring(dashIndex + 2);
                    if (afterDash.Length > 0 && afterDash.Contains(shortFlagChar))
                        return true;
                }

                if (lowerInput.Contains(" " + shortFlag + " ") ||
                    lowerInput.EndsWith(" " + shortFlag))
                    return true;
            }

            return false;
        }
    }

    public class UnknownCommand : ICommand
    {
        private string _message;

        public UnknownCommand(string message = "Неизвестная команда. Введите 'help' для списка команд.")
        {
            _message = message;
        }

        public void Execute()
        {
            Console.WriteLine(_message);
        }
    }
}
