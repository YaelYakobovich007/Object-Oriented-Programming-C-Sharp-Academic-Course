namespace GameLogic
{
    internal class Player
    {
        private readonly string r_Name;
        private int m_Points = 0;

        internal Player(string i_PlayerName)
        {
           r_Name= i_PlayerName;
        }

        internal int Points
        {
            get { return m_Points; }

            set { m_Points = value; }
        }

        internal string Name
        {
            get { return r_Name; }
        }
    }
}

