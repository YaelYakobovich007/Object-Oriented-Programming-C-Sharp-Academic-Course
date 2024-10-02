namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private readonly MenuItem r_MenuItem;

        public MainMenu(MenuItem i_MenuItem)
        {
            r_MenuItem = i_MenuItem;
        }

        public void Show()
        {
            r_MenuItem.DoAction();
        }
    }
}