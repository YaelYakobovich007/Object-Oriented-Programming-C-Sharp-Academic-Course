using System;
using System.Collections.Generic;

namespace GameLogic
{
    internal class ComputerAi
    {
        internal enum eDifficulty
        {
            Easy = 4,
            Medium = 7,
            Hard = 10,
        }
        private readonly eDifficulty r_AiSkillLevel;
        private Queue<Card> m_AiMemory;
        private readonly string r_Name;
        private List<string> m_AvailableCardPositions;
        private readonly Random r_Randomizer;

        internal int Points { get; set; } = 0;

        internal string Name
        {
            get { return r_Name; }
        }

        internal ComputerAi(string i_Name, eDifficulty i_Difficulty, List<string> i_AvailableCardPositions)
        {
            r_Randomizer = new Random();
            r_AiSkillLevel = i_Difficulty;
            r_Name = i_Name;
            m_AiMemory = new Queue<Card>();
            m_AvailableCardPositions = new List<string>(i_AvailableCardPositions);
        }

        internal Card AiChoice(Card i_FirstCard, Board i_BoardGame)
        {
            Card chosenCard = null;

            if(i_FirstCard != null)
            {
                bool potentialFlag = false;

                foreach(Card potentialCard in m_AiMemory)
                {
                    if(potentialCard.Symbol == i_FirstCard.Symbol)
                    {
                        chosenCard = potentialCard;
                        potentialFlag = true;
                        break;
                    }
                }

                if(!potentialFlag)
                {
                   chosenCard = getRandomCardFromAvailable(i_BoardGame);
                }
            }
            else
            {
                if(!lookForMatchInMemory(out chosenCard))
                {
                    chosenCard = getRandomCardFromAvailable(i_BoardGame);
                }
            }

            return chosenCard;
        }

        private Card getRandomCardFromAvailable(Board i_BoardGame)
        {
            Card returnCard;

            if(m_AvailableCardPositions.Count != 0)
            {
                int index = r_Randomizer.Next(0, m_AvailableCardPositions.Count);
                string location = m_AvailableCardPositions[index];

                removeFromAvailableCardLocationsList(location);
                returnCard = i_BoardGame.GetCardInPosition(location);
            }
            else
            {
                returnCard = m_AiMemory.Dequeue();
            }
            
            return returnCard;
        }

        private bool lookForMatchInMemory(out Card i_Choice)
        {
            i_Choice = null;
            int counter = 0;
            bool returnValue = false;

            if(m_AiMemory.Count > 1)
            {
                while(m_AiMemory.Count > counter)
                {
                    Card cardToCheck = m_AiMemory.Dequeue();

                    foreach(Card potentialCard in m_AiMemory)
                    {
                        if(potentialCard.Symbol == cardToCheck.Symbol)
                        {
                            i_Choice = cardToCheck;
                            returnValue = true;
                            break;
                        }
                    }

                    if(returnValue)
                    {
                        break;
                    }

                    m_AiMemory.Enqueue(cardToCheck);
                    counter++;
                }

                while(m_AiMemory.Count - counter > 0)
                {
                    m_AiMemory.Enqueue(m_AiMemory.Dequeue());
                    counter++;
                }
            }

            return returnValue;
        }
        
        internal void AddToAiMemory(Card i_FirstCard, Card i_SecondCard)
        {
            addToAiMemory(i_FirstCard);
            addToAiMemory(i_SecondCard);
        }

        private void addToAiMemory(Card i_Card)
        {
            if(!m_AiMemory.Contains(i_Card))
            {
                if(m_AiMemory.Count >= (int)r_AiSkillLevel)
                {
                    addToAvailableCardLocationsList(m_AiMemory.Dequeue().Position);
                }

                m_AiMemory.Enqueue(i_Card);
                removeFromAvailableCardLocationsList(i_Card.Position);
            }
        }

        internal void RemoveFromAiMemory(Card i_FirstCard, Card i_SecondCard)
        {
            int counter = 0;
            bool containsFirstCard = m_AiMemory.Contains(i_FirstCard);
            bool containsSecondCard = m_AiMemory.Contains(i_SecondCard);

            if(containsFirstCard || containsSecondCard)
            {
                int initialCount = m_AiMemory.Count;

                while(counter < initialCount)
                {
                    Card currentCard = m_AiMemory.Dequeue();

                    if((currentCard.Symbol!=i_FirstCard.Symbol)&& (currentCard.Symbol!= i_SecondCard.Symbol))
                    {
                        m_AiMemory.Enqueue(currentCard);
                    }

                    counter++;
                }
            }
        }

        private void addToAvailableCardLocationsList(string i_CardLocation)
        {
            if(!m_AvailableCardPositions.Contains(i_CardLocation))
            {
                m_AvailableCardPositions.Add(i_CardLocation);
            }
        }

        private void removeFromAvailableCardLocationsList(string i_CardLocation)
        {
            if(m_AvailableCardPositions.Contains(i_CardLocation))
            {
                m_AvailableCardPositions.Remove(i_CardLocation);
            }
        }
    }
}
