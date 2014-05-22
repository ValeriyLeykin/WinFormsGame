using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using B14_Ex05_Logic;

namespace B14_Ex05_WindowsUI
{
    //TODO: should it be nested class of GameForm? the margin of the GameForm  should be known to cardButton
    //in order it  can locate itself in the form in the proper way
    class CardButton : Button 

    {
        private readonly CardLocation r_CardLocation;
        private const int k_CardWidth = 50;
        private const int k_CardHeight = 50;
        private const int k_SpaceBetweenCards = 5;
        private bool m_Revealed = false; //TODO - do I need this field?

        public CardButton(CardLocation i_CardLocation, int i_Margin)
        {
            this.Size = new Size(k_CardWidth, k_CardHeight);
            r_CardLocation = i_CardLocation;
            int xPoint = (k_CardWidth + k_SpaceBetweenCards) * r_CardLocation.Column + i_Margin;
            int yPoint = (k_CardHeight + k_SpaceBetweenCards) * r_CardLocation.Row + i_Margin;
            this.Location = new Point(xPoint, yPoint);
        }
    }    
}
