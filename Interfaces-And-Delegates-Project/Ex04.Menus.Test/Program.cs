namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            MenuInterface menuInterface = new MenuInterface();
            MenuDelegate menuDelegate = new MenuDelegate();

            menuInterface.Show();
            menuDelegate.Show();
        }
    }
}