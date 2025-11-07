namespace Todolist
{
    public class ProfileCommand : ICommand
    {
        public Profile Profile { get; set; }
        public string DataDirectory { get; set; }

        public void Execute()
        {
            Console.WriteLine(Profile.GetInfo());

            if (!string.IsNullOrEmpty(DataDirectory))
            {
                string profileFile = Path.Combine(DataDirectory, "profile.txt");
                FileManager.SaveProfile(Profile, profileFile);
            }
        }
    }
}
