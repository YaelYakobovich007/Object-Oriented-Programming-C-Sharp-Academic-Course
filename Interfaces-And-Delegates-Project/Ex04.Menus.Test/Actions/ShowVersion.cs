using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class ShowVersion : IActionSelect
    {
        public void Action()
        {
            Console.WriteLine("App Version: 24.2.4.9504");
        }
    }
}