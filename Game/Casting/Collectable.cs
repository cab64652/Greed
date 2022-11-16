namespace Greed.Game.Casting
{
    public class Collectable : Actor
    {
        private int _points;

        public Collectable()
        {
        }

        public int GetPoints()
        {
            return _points;
        }

        public int SetPoints(int points)
        {
            _points = points;
        }
    }
}