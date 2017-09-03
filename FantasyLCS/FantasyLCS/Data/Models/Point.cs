namespace FantasyLCS.Data.Models
{
    public class Point
    {
        public const int PointParameterBypass = -100;
        public const int FocusDNE = -1;
        public const int ShopXLocation = -1;
        public const int RedShopYLocation = 1;
        public const int BlueShopYLocation = 0;

        public int X { get; set; }
        public int Y { get; set; }

        public Point() {
            this.X = 0;
            this.Y = 0;
        }
        public Point(int x, int y) {
            this.X = x;
            this.Y = y;
        }
    }
}
