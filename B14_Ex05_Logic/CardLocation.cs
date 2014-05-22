using System;
using System.Collections.Generic;
using System.Text;

namespace B14_Ex05_Logic
{
    public struct CardLocation
    {
        private int m_Row;
        private int m_Column;

        public CardLocation(int i_Row, int i_Column)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }

        public int Row
        {
            get { return m_Row; }
        }
        
        public int Column
        {
            get { return m_Column; }
        }
    }
}
