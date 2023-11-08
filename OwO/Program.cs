using System;
using System.Drawing;
using WindowsInput;

namespace OwO {
    public enum TileState {
        empty,
        yellow,
        blue,
    }
    internal class Program {
        public static Point[][] positions; //where all the tiles are. the cordinates on the screen
        public static TileState[][] tiles; //Enum to see if tiles are yellow, blue or emty. find by using double array
        public static Point upperLeft = new Point(670, 268); //upper left of the game screen. in the middle
        public static Point lowerRight = new Point(1255, 851);
        public static int gridSize = 12;
        public static InputSimulator simulator = new InputSimulator();
        static void Main(string[] args) {
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            ComputePositions(12);
            ReadScreen();
            StartLogo();
            
            Console.WriteLine("Press Enter");
            Console.ReadLine();

            Print();
        }
        static void ComputePositions(int gridSize) {
            int unitLength = (int)((lowerRight.X - upperLeft.X) / (gridSize - 1)); //unit length is the distance between each box.

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
            g.CopyFromScreen(upperLeft, new(0, 0), size);

            tiles = new TileState[gridSize][];
            for (int i = 0; i < gridSize; i++) {
                tiles[i] = new TileState[gridSize];
                for (int j = 0; j < gridSize; j++) {
                    Color color = bmp.GetPixel(positions[i][j].X - upperLeft.X, positions[i][j].Y - upperLeft.Y);
                    if (color.B >= 180) tiles[i][j] = TileState.blue;
                    else if (color.G >= 200) tiles[i][j] = TileState.yellow;
                    else tiles[i][j] = TileState.empty;
                }
            }



        }
        static void StartLogo() {
            bool isNotDone=true;
            while (isNotDone){
                isNotDone = false;
                for (int i = 0; i < gridSize; i++) {
                    for (int j = 0 ; j < gridSize; j++) {
                        if (tiles[i][j] == tiles[i + 2][j]){
                            isNotDone=true;
                            tiles[i+1][j] = tiles[i][j];
                        }
                    }
                }
                
               
            }



        }
        static void Gabi(int i, int j) {
            if (i < gridSize - 2) {

            }
        }


        static void Print() {
            for (int i = 0; i < gridSize; i++) {
                for (int j = 0; j < gridSize; j++) {

                    if (tiles[i][j] == TileState.blue) {
                        BlueClick(positions[i][j]);
                    }
                    if (tiles[i][j] == TileState.yellow) {
                        YellowClick(positions[i][j]);
                    }
                }
            }
        }
        static void YellowClick(Point point) {
            point = Scalluing(point);
            simulator.Mouse.MoveMouseTo(point.X, point.Y);
            simulator.Mouse.LeftButtonClick();
        }
        /*static void Check3rule() {
            for (int i = 0;i < 3;i++) {
                tiles[1][1] = TileState.blue;
                simulator.Mouse.MoveMouseTo(positions[i][j]);
                simulator.Mouse.RightButtonClick();
            }
        }*/

        static void BlueClick(Point point) {
            point = Scalluing(point);
            simulator.Mouse.MoveMouseTo(point.X, point.Y);
            simulator.Mouse.RightButtonClick();
        }
        static Point Scalluing(Point point) {
            float xRes = 1920, yRes = 1080;

            Point newPoint = new Point();

            newPoint.X = (int)((point.X / xRes) * 65535);
            newPoint.Y = (int)((point.Y / yRes) * 65535);

            return newPoint;
        }//Converter for simulating input screen size and pixel to use for the mouse ONLY!!!!!
    }
}







/*StreamWriter streamWriter = new StreamWriter("C:\\Users\\Asbjo\\OneDrive\\Skrivebord\\Programmering\\C# og Maui\\OwO\\test.csv");
streamWriter.WriteLine("123,3,21,32");
streamWriter.Close();
StreamReader streamReader = new StreamReader("C:\\Users\\Asbjo\\OneDrive\\Skrivebord\\Programmering\\C# og Maui\\OwO\\test.csv");
Console.WriteLine(streamReader.ReadLine());
//string[] strings = streamReader.ReadLine().Split(",");
streamReader.Close();*/
