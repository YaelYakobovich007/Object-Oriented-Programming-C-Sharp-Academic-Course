using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class ShowDate : IActionSelect
    {
        public void Action()
        {
            Console.WriteLine(string.Format("The Date is: {0}", DateTime.Today.ToString("dd/MM/yyyy")));
        }
    }
}