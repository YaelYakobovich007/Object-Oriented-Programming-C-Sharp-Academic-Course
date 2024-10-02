using System;

namespace Ex04.Menus.Interfaces
{
    public class LeafItem: MenuItem
    {
        private const int k_TimeOutPeriod = 2000;
        public IActionSelect Action { get; set;}

        public LeafItem(string i_Name)
            : base(i_Name)
        {
        }

        internal override void DoAction()
        {
            if(Action != null)
            {
                Console.Clear();
                Action.Action();
            }

            System.Threading.Thread.Sleep(k_TimeOutPeriod);
            Father?.DoAction();
        }
    }
}