using Microsoft.Xna.Framework;

namespace LambdaBW_Conway_sGameOfLife
{
    class Grid
    {
        public Tile[,] GridArray { get; private set; }

        public Grid(int columnCount = 25, int rowCount = 25)
        {
            GridArray = new Tile[columnCount, rowCount];
            for (int x = 0; x < columnCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    GridArray[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                }
            }
        }
    }
}
