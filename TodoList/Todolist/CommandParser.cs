using Todolist;

private static ICommand ParseProfileCommand(string input, Profile profile)
{
    return new ProfileCommand
    {
        Profile = profile,
        DataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data")
    };
}
