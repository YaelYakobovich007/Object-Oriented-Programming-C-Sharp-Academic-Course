using Ex04.Menus.Interfaces;
using Ex04.Menus.Test.Actions;

namespace Ex04.Menus.Test
{
    internal class MenuInterface
    {
        private readonly MainMenu r_MainMenu;

        internal MenuInterface()
        {
            r_MainMenu = new MainMenu(Create());
        }

        internal MenuItem Create()
        {
            InnerMenu inMain = new InnerMenu("Interface Main Menu");
            InnerMenu versionAndCapitalsMenu = new InnerMenu("Version and Capitals");
            LeafItem showVersionLeafItem = new LeafItem("Show Version");
            LeafItem countCapitalsLeafItem = new LeafItem("Count Capitals");
            InnerMenu showDateOrTimeMenu = new InnerMenu("Show Date/Time");
            LeafItem showTimeLeafItem = new LeafItem("Show Time");
            LeafItem showDateLeafItem = new LeafItem("Show Date");

            versionAndCapitalsMenu.AddToMenu(showVersionLeafItem);
            versionAndCapitalsMenu.AddToMenu(countCapitalsLeafItem);
            showDateOrTimeMenu.AddToMenu(showTimeLeafItem);
            showDateOrTimeMenu.AddToMenu(showDateLeafItem);
            inMain.AddToMenu(versionAndCapitalsMenu);
            inMain.AddToMenu(showDateOrTimeMenu);
            showVersionLeafItem.Action = new ShowVersion();
            countCapitalsLeafItem.Action = new CountCapital();
            showTimeLeafItem.Action = new ShowTime();
            showDateLeafItem.Action = new ShowDate();

            return inMain; 
        }

        internal void Show()
        {
            r_MainMenu.Show();
        }
    }
}