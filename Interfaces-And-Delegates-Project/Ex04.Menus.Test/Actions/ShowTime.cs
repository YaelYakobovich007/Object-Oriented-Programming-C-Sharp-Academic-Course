using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test.Actions
{
    public class ShowTime : IActionSelect
    {
        public void Action()
        {
            Console.WriteLine(string.Format("The Hour is: {0}", DateTime.Now.ToString("HH:mm:ss")));
        }
    }
}