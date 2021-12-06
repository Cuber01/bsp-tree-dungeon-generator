using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;

namespace DungeonGenerator
{
    public class Branch
    {
        private readonly int level;

        // Starts as false!
        private bool triedHorizontal;
        private bool triedVertical;
        private bool isSplit; 

        private Room room;
        private readonly int x;
        private readonly int y;
        private readonly int w;
        private readonly int h;

        private Point center;

        private Branch lchild;
        private Branch rchild;

        public Branch(int level, int x, int y, int w, int h)
        {
            this.level = level;

            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;

            this.center = new Point(this.x + (this.w/2), this.y + (this.h / 2));
        }

        /*
         So, we have a few ways how we can go about stopping the tree.
         1. Check if either Width or Height (depending on in which direction are we currently splitting)
            is bigger than the minimum variable provided.
         2. Stop the tree when it reaches a given level (tree depth).
         3. Stop the tree when both Width and Height are bigger than the minimum.
         
         As of now, we're using method 3
        */
        
        public void split()
        {
            if (Util.random.Next(0, 2) == 0)
            {
                bool horizontalSuccess = splitHorizontal(DungeonGenerator.minHeight, DungeonGenerator.heightRatio);  
                if(!horizontalSuccess)
                {
                    splitVertical(DungeonGenerator.minWidth, DungeonGenerator.widthRatio); 
                }
            }
            else
            {
                bool verticalSuccess = splitVertical(DungeonGenerator.minWidth, DungeonGenerator.widthRatio);
                if (!verticalSuccess)
                {
                    splitHorizontal(DungeonGenerator.minHeight, DungeonGenerator.heightRatio);  
                }
            }

            lchild?.split();
            rchild?.split();
        }

        bool splitVertical(int minWidth, double ratio)
        {

            if (w < minWidth)
            {
                return false;
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
                    return false;
                }                
            }

            lchild = new Branch(level + 1, x, y, newWidthLeft, h); // Left
            rchild = new Branch(level + 1, x + newWidthLeft, y, newWidthRight, h); // Right
            
            isSplit = true;
            return true;
        }

        bool splitHorizontal( int minHeight, double ratio  )
        {
            
            if (h < minHeight)
            {
                return false;
            }

            int newHeightTop;
            int newHeightBottom;
            double aRatio;
            
            int counter = 100;
            ( newHeightTop, newHeightBottom, aRatio ) = splitLineRandom(h);

            while ( ( aRatio < (1 - ratio) || aRatio > (1 + ratio)  )  )
            {
                
                ( newHeightTop, newHeightBottom, aRatio ) = splitLineRandom(h);

                counter--;
                if (counter == 0)
                {
                    return false;
                }   
                
            }
            
            lchild = new Branch(level + 1, x, y, w, newHeightTop); // Top
            rchild = new Branch(level + 1, x, y + newHeightTop, w, newHeightBottom ); // Bottom
            
            isSplit = true;
            return true;
        }
        
        static (int, int, double) splitLineRandom(int length)
        {
            // Length of the Left/Right part of the line
            int newLengthLeft  = Util.random.Next(1, length);
            int newLengthRight = length - newLengthLeft;
            double aRatio = aspectRatio(newLengthLeft,newLengthRight);

            return (newLengthLeft, newLengthRight, aRatio);
        }

        static double aspectRatio(int a, int b)
        {
            double rv = (double)a / (double)b;
            return rv;
        }

        public void makeRooms()
        {
            // We don't need rooms for sections which were split - only for their children which are the only ones visible to the user.
            if (!isSplit)
            {
                room = new Room(x, y, w, h);
            }
            
            lchild?.makeRooms();
            rchild?.makeRooms();
        }

        public void drawRooms(PrimitiveDraw draw, Color color)
        {
            room?.paint(draw, color);
            lchild?.drawRooms(draw, color);
            rchild?.drawRooms(draw, color);
        }

        public void drawPaths(PrimitiveDraw draw, Color color)
        {
            paintPath(draw, color, new Point(20, 20));
            lchild?.drawPaths(draw, color);
            rchild?.drawPaths(draw, color);
        }

        private void paintPath(PrimitiveDraw draw, Color color, Point destination)
        {

            // moveTo(center.X, center.Y);
            // c.lineTo(destination.X, destination.Y);
        }
        
        public void drawConnections(PrimitiveDraw draw, Color color)
        {
            paintConnection(draw, color);
            lchild?.drawConnections(draw, color);
            rchild?.drawConnections(draw, color);    
        }

        private void paintConnection(PrimitiveDraw draw, Color color)
        {
            if (lchild != null)
            {
                draw.bersenhamLine(center.X, center.Y, lchild.center.X, lchild.center.Y, color);
            }

            if (rchild != null)
            {
                draw.bersenhamLine(center.X, center.Y, rchild.center.X, rchild.center.Y, color);
            }
  
        }
        
        public void drawSections(PrimitiveDraw draw, Color color)
        {
            paint(draw, color);
            lchild?.drawSections(draw, color);
            rchild?.drawSections(draw, color);    
        }

        private void paint(PrimitiveDraw draw, Color color)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            draw.drawRectangle(rect, color, false);
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
        
    }
}



