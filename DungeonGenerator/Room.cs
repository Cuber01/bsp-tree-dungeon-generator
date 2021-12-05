using Microsoft.Xna.Framework;

namespace DungeonGenerator
{
    public class Room
    {
        private readonly int x;
        private readonly int y;
        private readonly int w;
        private readonly int h;
        
        
        public Room(int x, int y, int w, int h)
        {
            this.x = x + Util.random.Next(0, (w / 3) + 1);
            this.y = y + Util.random.Next(0, (h / 3) + 1);
            this.w = w - (this.x - x);
            this.h = h - (this.y - y);
            this.w -= Util.random.Next(0, (this.w / 3) + 1);
            this.h -= Util.random.Next(0, (this.w / 3) + 1);
        }
        
        public void paint(PrimitiveDraw draw, Color color)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            draw.drawRectangle(rect, color, false);
        }
        
    }
}