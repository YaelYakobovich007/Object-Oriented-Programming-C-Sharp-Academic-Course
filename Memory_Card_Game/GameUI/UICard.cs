namespace GameUI
{
    internal class UICard
    {
        internal bool IsFlipped { get; private set; }

        internal char Symbol { get; private set; }

        internal UICard(char i_Symbol)
        {
            Symbol = i_Symbol;
            IsFlipped = false;
        }

        internal void Flip()
        {
            IsFlipped = !IsFlipped;
        }
    }
}
