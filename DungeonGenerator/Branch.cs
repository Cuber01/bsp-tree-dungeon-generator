using System;
using Microsoft.Xna.Framework;

namespace DungeonGenerator
{
    public class Branch
    {
        private readonly int level;

        private readonly int x;
        private readonly int y;
        private readonly int w;
        private readonly int h;

        private Branch lchild;
        private Branch rchild;

        public Branch(int level, int x, int y, int w, int h)
        {
            this.level = level;

            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public void split()
        {
            if (level < 5)
            {
                splitRandom();
            }
            
            lchild?.split();
            rchild?.split();
        }

        void splitRandom()
        {
            // Split in width
            if (Util.random.Next(0, 2) == 0)
            {
                int newWidth = Util.random.Next(1, w);
                
                // if (newWidth < DungeonGenerator.minWidth)
                // {
                //     return;
                // }
                
                // Left
                lchild = new Branch(level + 1, x, y, newWidth, h);

                // Right
                rchild = new Branch(level + 1, x + newWidth, y, w - newWidth, h);
            }
            // Split in height
            else
            {
                int newHeight = Util.random.Next(1, h);
                
                // if (newHeight < DungeonGenerator.minHeight)
                // {
                //     return;
                // }
                
                // Up
                lchild = new Branch(level + 1, x, y, w, newHeight);
                
                // Down 
                rchild = new Branch(level + 1, x, y + newHeight, w, h - newHeight );
            }

        }

        public void printInfo()
        {
            printDebugInfo();
            lchild?.printInfo();
            rchild?.printInfo();
        }

        private void printDebugInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("level " + level + ": ");
            
            Console.ResetColor();
            Console.Write(x + ", " + y + ", " + w + ", " + h);
            Console.Write("\n");
        }
        
        public void draw(PrimitiveDraw draw, Color color)
        {
            paint(draw, color);
            lchild?.draw(draw, color);
            rchild?.draw(draw, color);    
        }

        private void paint(PrimitiveDraw draw, Color color)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            draw.drawRectangle(rect, color, false);
        }
        
        

    }
}

