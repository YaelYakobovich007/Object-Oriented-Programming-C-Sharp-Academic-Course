namespace GameLogic
{
    internal class Card
    {
        private readonly string r_Position;
        private readonly int r_Symbol;

        internal bool IsFlipped { get; private set; }

        internal string Position
        {
            get { return r_Position; }
        }

        internal int Symbol
        {
            get { return r_Symbol; }
        }

        internal void Flip()
        {
            IsFlipped = !IsFlipped; 
        }

        internal Card(int i_Symbol, string i_Position)
        {
            r_Position = i_Position;
            r_Symbol = i_Symbol;
        }
    }
}
