using System;
using System.Drawing;

namespace OwO {
    public enum TileState {
        empty,
        yellow,
        blue,
    }
    internal class Program {
        public static Point[][] positions;
        public static TileState[][] tiles;
        public static Point upperLeft = new Point(670, 268);
        public static Point lowerRight = new Point(1255, 851);
        public static int gridSize = 12;

        static void Main(string[] args) {
            Console.ReadLine();
            ComputePositions(12);
            ReadScreen();
        }
        static void ComputePositions(int gridSize) {
            int unitLength = (int)((lowerRight.X - upperLeft.X) / (gridSize - 1));

            // dedicating space
            positions = new Point[gridSize][];
            for (int i = 0; i < gridSize; i++) {
                positions[i] = new Point[gridSize];
                for (int j = 0; j < gridSize; j++) {
                    positions[i][j] = new Point(upperLeft.X + unitLength * i, upperLeft.Y + unitLength * j);
                }
            }



        }
        static void ReadScreen() {
            Size size = new(lowerRight.X - upperLeft.X, lowerRight.Y - upperLeft.Y + 1);
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(upperLeft, lowerRight, size);

            tiles = new TileState[gridSize][];
            for (int i = 0; i < gridSize; i++) {
                tiles[i] = new TileState[gridSize];
                for (int j = 0; j < gridSize; j++) {
                    Color color = bmp.GetPixel(positions[i][j].X - upperLeft.X, positions[i][j].Y - upperLeft.Y);
                    if (color.B >= 200) tiles[i][j] = TileState.blue;
                    else if (color.G >= 200) tiles[i][j] = TileState.yellow;
                    else tiles[i][j] = TileState.empty;
                }
            }



        }
    }
}