using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    internal class ComputerSelectionLogic
    {
        Random randomUnrevealedCardGenerator;

        internal CardLocation GetSelection(List<CardLocation> i_UnrevealedCards)
        {
            // $G$ NTT-007 (-5) There's no need to re-instantiate the Random instance each time it is used. - done
            randomUnrevealedCardGenerator = new Random((int)DateTime.Now.Ticks); //each time new seed
            int randomLocation = randomUnrevealedCardGenerator.Next(0, i_UnrevealedCards.Count);
            
            return i_UnrevealedCards[randomLocation];
        }
    }
}
