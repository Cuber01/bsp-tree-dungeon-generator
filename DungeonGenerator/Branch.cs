using System;

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
            splitRandom();
            lchild?.split();
            rchild?.split();
        }

        void splitRandom()
        {
            if (level > 10)
            {
                return;
            }
            
            // Split in width
            if (Util.random.Next(0, 2) == 0)
            {
                // Left
                lchild = new Branch(level + 1, x, y, w / 2, h);

                // Right
                rchild = new Branch(level + 1, x + w / 2, y, w / 2, h);
            }
            // Split in height
            else
            {
                // Up
                lchild = new Branch(level + 1, x, y, w, h / 2);
                
                // Down 
                rchild = new Branch(level + 1, x, y + h / 2, w, h / 2 );
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
        
        public void draw()
        {
            
        }
        
        

    }
}