using System;

namespace Prototype.Core
{
    [Serializable]
    public struct GridPosition
    {
        public int row;
        public int column;

        public GridPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}