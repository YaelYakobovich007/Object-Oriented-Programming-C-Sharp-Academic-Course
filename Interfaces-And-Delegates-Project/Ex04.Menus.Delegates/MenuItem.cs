namespace Ex04.Menus.Delegates
{
    public abstract class MenuItem
    {
        protected readonly string r_ItemName;

        internal MenuItem Father { get; set; }

        protected MenuItem(string i_ItemName)
        {
            r_ItemName = i_ItemName;
        }

        internal string ItemName
        {
            get
            {
                return r_ItemName;
            }
        }

        internal abstract void OnOptionChosen();
    }
}