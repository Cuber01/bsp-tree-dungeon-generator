using System;
using System.Reflection;
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

            if (Util.random.Next(0, 2) == 0)
            {
                splitHorizontal(40, 0.4);      
            }
            else
            {
                splitVertical(40, 0.4);    
            }
            
          

            lchild?.split();
            rchild?.split();
        }

        (int, int, double) splitLineRandom(int width)
        {
            int newWidthLeft  = Util.random.Next(1, width);
            int newWidthRight = width - newWidthLeft;
            double aRatio = aspectRatio(newWidthLeft,newWidthRight);

            return (newWidthLeft, newWidthRight, aRatio);
        }
        
        void splitVertical(int minWidth, double ratio)
        {

            if (w < minWidth)
            {
                return;
            }

            int newWidthLeft;
            int newWidthRight;
            double aRatio;
            
            int counter = 100;
            ( newWidthLeft, newWidthRight, aRatio ) = splitLineRandom(w);

            while ( ( aRatio < (1 - ratio) || aRatio > (1 + ratio) ) )
            {
                ( newWidthLeft, newWidthRight, aRatio ) = splitLineRandom(w);

                counter--;
                if (counter == 0)
                {
                    return;
                }                
            }

            lchild = new Branch(level + 1, x, y, newWidthLeft, h);// Left
            rchild = new Branch(level + 1, x + newWidthLeft, y, newWidthRight, h);// Right
            
        }

        void splitHorizontal( int minHeight, double ratio  )
        {
            
            if (h < minHeight)
            {
                return;
            }

            int newHeightTop;
            int newHeightBottom;
            double aRatio;
            
            int counter = 100;
            ( newHeightTop, newHeightBottom, aRatio ) = splitLineRandom(h);

            while ( ( aRatio < (1 - ratio) || aRatio > (1 + ratio)  )  )
            {
                ( newHeightTop, newHeightBottom, aRatio )= splitLineRandom(h);               
                if (counter-- == 0) { return; }                
            }
            

            lchild = new Branch(level + 1, x, y, w, newHeightTop);
            rchild = new Branch(level + 1, x, y + newHeightTop, w, newHeightBottom );

        }

        double aspectRatio(int a, int b)
        {
            double rv = (double)a / (double)b;
            return rv;
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



