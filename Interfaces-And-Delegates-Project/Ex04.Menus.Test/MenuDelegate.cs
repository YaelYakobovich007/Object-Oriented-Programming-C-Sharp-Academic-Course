using Ex04.Menus.Delegates;
using Ex04.Menus.Test.Actions;

namespace Ex04.Menus.Test
{
    internal class MenuDelegate
    {
        private readonly MainMenu r_MainMenu;

        internal MenuDelegate()
        {
            r_MainMenu = new MainMenu(Create());
        }

        internal MenuItem Create()
        {
            InnerMenu inMain = new InnerMenu("Delegates Main Menu");
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
            showVersionLeafItem.OptionChosen += new ShowVersion().Action;
            countCapitalsLeafItem.OptionChosen += new CountCapital().Action;
            showTimeLeafItem.OptionChosen += new ShowTime().Action;
            showDateLeafItem.OptionChosen += new ShowDate().Action;

            return inMain;
        }

        internal void Show()
        {
            r_MainMenu.Show();
        }
    }
}