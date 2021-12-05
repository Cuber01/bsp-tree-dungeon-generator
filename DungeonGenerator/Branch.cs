using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;

namespace DungeonGenerator
{
    public class Branch
    {
        private readonly int level;

        public Room room;
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

        /*
         So, we have a few ways how we can go about stopping the tree.
         1. The one used at the time of writing this (commit 0d665e6773a30e77e4b520e896ba2745d37ee4dc),  
            check if either Width or Height (depending on in which direction are we currently splitting)
            is bigger than the minimum variable provided.
         2. Stop the tree when it reaches a given level (tree depth).
         3. Stop the tree when both Width and Height are bigger than the minimum.
        */
        
        public void split()
        {

            if (Util.random.Next(0, 2) == 0)
            {
                splitHorizontal(DungeonGenerator.minHeight, DungeonGenerator.heightRatio);      
            }
            else
            {
                splitVertical(DungeonGenerator.minWidth, DungeonGenerator.widthRatio);    
            }

            lchild?.split();
            rchild?.split();
        }

        static (int, int, double) splitLineRandom(int length)
        {
            // Length of the Left/Right part of the line
            int newLengthLeft  = Util.random.Next(1, length);
            int newLengthRight = length - newLengthLeft;
            double aRatio = aspectRatio(newLengthLeft,newLengthRight);

            return (newLengthLeft, newLengthRight, aRatio);
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
                
                ( newHeightTop, newHeightBottom, aRatio ) = splitLineRandom(h);

                counter--;
                if (counter == 0)
                {
                    return;
                }   
                
            }
            

            lchild = new Branch(level + 1, x, y, w, newHeightTop);
            rchild = new Branch(level + 1, x, y + newHeightTop, w, newHeightBottom );

        }

        static double aspectRatio(int a, int b)
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

        public void makeRooms()
        {
            room = new Room(x, y, w, h);
            lchild?.makeRooms();
            rchild?.makeRooms();
        }

        public void paintRooms(PrimitiveDraw draw, Color color)
        {
            room.paint(draw, color);
            lchild?.paintRooms(draw, color);
            rchild?.paintRooms(draw, color);
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



