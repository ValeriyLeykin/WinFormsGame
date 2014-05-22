using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    internal class GameBoardCard
    {
        private readonly eBoardCardChars r_BoardChar;
        private bool m_IsVisible;

        internal GameBoardCard(eBoardCardChars i_Letter)
        {
            r_BoardChar = i_Letter;
        }

        internal bool IsVisible
        {
            get { return m_IsVisible; }
            set { m_IsVisible = value; }
        }

        internal eBoardCardChars Char
        {
            get { return r_BoardChar; }
        }
    }
}
