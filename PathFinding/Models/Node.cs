namespace PathFinding.Models
{
    public struct GridNode
    {
        public Position CurrentPos;
        public Position PreviousPos;

        public GridNode(short x, short y)
        {
            CurrentPos = new Position(x, y);
            PreviousPos = new Position(x, y);
        }
    }
}
