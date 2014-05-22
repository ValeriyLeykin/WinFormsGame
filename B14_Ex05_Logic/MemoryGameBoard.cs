using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    internal class MemoryGameBoard
    {
        private GameBoardCard[,] m_BoardCards;
        private int m_Height = 0;
        private int m_Width = 0;
        private int m_NumberOfCardsLeftToBeRevealed = 0;

        public MemoryGameBoard()
        {
            m_Height = 0;
            m_Width = 0;
        }

        internal void Init(int i_BoardHeight, int i_BoardWidth)
        {
            Height = i_BoardHeight;
            Width = i_BoardWidth;

            // there is really no point in calling IsBoardValid() here because we can't throw exceptions yet. and... i know this is a comment... but this is just for you!
            m_BoardCards = new GameBoardCard[Height, Width];

            m_NumberOfCardsLeftToBeRevealed = totalNumberOfCards;
            fillBoard();
        }

        internal int Height
        {
            get { return m_Height; }
            private set { m_Height = value; }
        }

        internal int Width
        {
            get { return m_Width; }
            private set { m_Width = value; }
        }

        internal bool IsFullyRevealed
        {
            get { return m_NumberOfCardsLeftToBeRevealed == 0; }
        }

        internal bool IsBoardValid(int i_Height, int i_Width)
        {
            return (i_Height * i_Width) % 2 == 0;
        }

        internal int totalNumberOfCards
        {
            get
            {
                return Height * Width;
            }
        }

        internal GameBoardCard GetCardAt(CardLocation i_CardLocation)
        {
            return m_BoardCards[i_CardLocation.Row, i_CardLocation.Column];
        }

        internal bool AreCardsEqual(CardLocation i_FirstCardLocation, CardLocation i_SecondCardLocation)
        {
            GameBoardCard firstCardToCompare = GetCardAt(i_FirstCardLocation);
            GameBoardCard secondCardToCompare = GetCardAt(i_SecondCardLocation);

            return firstCardToCompare.Char == secondCardToCompare.Char;
        }

        internal bool IsCardVisibile(CardLocation i_CardLocation)
        {
            return GetCardAt(i_CardLocation).IsVisible == true;
        }

        internal bool IsCardHidden(CardLocation i_CardLocation)
        {
            return GetCardAt(i_CardLocation).IsVisible == false;
        }

        internal char? GetCharacterAt(CardLocation i_CardLocation)
        {
            char? result;
            GameBoardCard card = GetCardAt(i_CardLocation);
            result = card.IsVisible ? (char?)card.Char : null;

            return result;
        }

        internal void Reveal(CardLocation i_CardLocation)
        {
            if (IsCardLocationInsideBoard(i_CardLocation))
            {
                m_BoardCards[i_CardLocation.Row, i_CardLocation.Column].IsVisible = true;
                m_NumberOfCardsLeftToBeRevealed--;
            }
        }

        internal void Unreveal(CardLocation i_CardLocation)
        {
            if (IsCardLocationInsideBoard(i_CardLocation))
            {
                m_BoardCards[i_CardLocation.Row, i_CardLocation.Column].IsVisible = false;
                m_NumberOfCardsLeftToBeRevealed++;
            }
        }

        internal bool IsCardLocationInsideBoard(CardLocation i_CardLocation)
        {
            bool result = true;
            if ((i_CardLocation.Row >= Height) || (i_CardLocation.Row < 0))
            {
                result = false;
            }
            else if ((i_CardLocation.Column >= Width) || (i_CardLocation.Column < 0))
            {
                result = false;
            }

            return result;
        }

        internal List<CardLocation> GetListOfUnrevealedCards()
        {
            List<CardLocation> UnrevealedCardsCollection = new List<CardLocation>();
            GameBoardCard currentCard;
            
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    CardLocation currentCardLocation = new CardLocation(row, col);
                    currentCard = GetCardAt(currentCardLocation);
                    if (!currentCard.IsVisible)
                    {
                        UnrevealedCardsCollection.Add(currentCardLocation);
                    }
                }
            }

            return UnrevealedCardsCollection;
        }

        private void fillBoard()
        {
            List<GameBoardCard> cardsLeftToDistribute = getListOfCardsToDistribute();
            List<CardLocation> locationsToDistributeCardsTo = getListOfLocationsToDistributeCardsTo();
            Random randomLocation = new Random();

            foreach (GameBoardCard cardToDistribute in cardsLeftToDistribute)
            {
                int designatedLocation = randomLocation.Next(0, locationsToDistributeCardsTo.Count);
                assignCardToLocation(cardToDistribute, locationsToDistributeCardsTo[designatedLocation]);
                locationsToDistributeCardsTo.RemoveAt(designatedLocation);
            }
        }

        private List<GameBoardCard> getListOfCardsToDistribute()
        {
            List<GameBoardCard> cardsToDistribute = new List<GameBoardCard>();

            int pairsToDistribute = totalNumberOfCards / 2;
            string[] charsToDistribute = Enum.GetNames(typeof(eBoardCardChars));
            
            for (int i = 0; i < pairsToDistribute; i++)
            {
                string currentStringToDistribute = charsToDistribute[i];
                char currentCharToDistribute = currentStringToDistribute[0];
                GameBoardCard card = new GameBoardCard((eBoardCardChars)currentCharToDistribute);
                cardsToDistribute.Add(card);
                card = new GameBoardCard((eBoardCardChars)currentCharToDistribute);
                cardsToDistribute.Add(card);
            }

            return cardsToDistribute;
        }

        private List<CardLocation> getListOfLocationsToDistributeCardsTo()
        {
            List<CardLocation> list = new List<CardLocation>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    CardLocation distributionLocation = new CardLocation(i, j);
                    list.Add(distributionLocation);
                }
            }

            return list;
        }

        private void assignCardToLocation(GameBoardCard i_Card, CardLocation i_Location)
        {
            m_BoardCards[i_Location.Row, i_Location.Column] = i_Card;
        }
    }
}