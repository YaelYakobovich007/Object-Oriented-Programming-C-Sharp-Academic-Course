using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class InnerMenu : MenuItem
    {
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private const int k_Zero = 0;
        private readonly List<MenuItem> r_ItemList;

        public InnerMenu(string i_Name)
            : base(i_Name)
        {
            r_ItemList = new List<MenuItem>();
        }

        public void AddToMenu(MenuItem i_NewItem)
        {
            i_NewItem.Father = this;
            r_ItemList.Add(i_NewItem);
        }

        private void show()
        {
            const string k_Seperator = "-----------------------";
            const string k_Option = "{0} -> {1}{2}";
            int i = 1;
            StringBuilder toShow = new StringBuilder();

            Console.Clear();
            toShow.AppendFormat("**{0}** {1}", r_ItemName, Environment.NewLine);
            toShow.AppendLine(k_Seperator);
            foreach(MenuItem item in r_ItemList)
            {
                toShow.AppendFormat(k_Option, i++, item.ItemName, Environment.NewLine);
            }
            if (Father == null)
            {
                toShow.AppendFormat(k_Option, k_Zero, k_Exit, Environment.NewLine);
            }
            else
            {
                toShow.AppendFormat(k_Option, k_Zero, k_Back, Environment.NewLine);
            }

            toShow.AppendLine(k_Seperator);
            Console.Write(toShow);
            askForChoice(toShow);
        }

        private void askForChoice(StringBuilder i_OptionsMenu)
        {
            string requestLine = string.Format("Enter your request: ({0} to {1} or press '0' to {2}).", 1, r_ItemList.Count, Father == null ? k_Exit : k_Back);
            StringBuilder invalidInput = new StringBuilder();
            int choice;

            invalidInput.AppendFormat("Input wasn't valid please choose only one of the following:{0}{0}", Environment.NewLine);
            invalidInput.Append(i_OptionsMenu);
            invalidInput.AppendLine(requestLine);
            Console.WriteLine(requestLine);
            while(!int.TryParse(Console.ReadLine(), out choice) || choice > r_ItemList.Count || choice < 0)
            {
                Console.Clear();
                Console.WriteLine(invalidInput);
            }

            if(choice == 0)
            {
                Father?.OnOptionChosen();
            }
            else
            {
                r_ItemList[choice - 1].OnOptionChosen();
            }
        }

        internal override void OnOptionChosen()
        {
            show();
        }
    }
}
