using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGenerator
{
	public class PrimitiveDraw
	{
		private static Texture2D pixel;
		private readonly GraphicsDevice graphicsDevice;
		private readonly SpriteBatch spriteBatch;

		public PrimitiveDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;
			this.graphicsDevice = graphicsDevice;
			
			pixel = new Texture2D(this.graphicsDevice, 1, 1);
			pixel.SetData(new[] { Color.White });
		}
		
		private void drawPixel(int x, int y, Color color)
		{
			spriteBatch.Draw(pixel, new Vector2(x, y), color);
		}

		private void drawStraightLine(int x1, int y1, int x2, int y2, Color color)
		{
			if (x1 == x2 && y1 == y2)
			{
				drawPixel(x1, x2, color);
			}
			else if (x1 < x2 && y1 == y2)
			{
				for (int x = x1; x <= x2; x++)
				{
					drawPixel(x, y1, color);
				}
			}
			else if (x1 > x2 && y1 == y2)
			{
				for (int x = x1; x >= x2; x--)
				{
					drawPixel(x, y1, color);
				}
			}
			else if (x1 == x2 && y1 < y2)
			{
				for (int y = y1; y <= y2; y++)
				{
					drawPixel(x1, y, color);
				}
			}
			else if (x1 == x2 && y1 > y2)
			{
				for (int y = y1; y >= y2; y--)
				{
					drawPixel(x1, y1, color);
				}
			}
			else
			{
				Console.WriteLine("Straight lines have straight coordinates. Dummy.");
				Console.WriteLine("Exiting...");
			}


		}
		
		
		public void bersenhamLine(int x1, int y1, int x2, int y2, Color color)
		{
			int x, y, xe, ye, i;

			int dx = x2 - x1;
			int dy = y2 - y1;

			int dx1 = Math.Abs(dx);
			int dy1 = Math.Abs(dy);

			int px = 2 * dy1 - dx1;
			int py = 2 * dx1 - dy1;
    
			if (dy1 <= dx1)
			{
				if (dx >= 0)
				{
					x = x1;
					y = y1;
					xe = x2;
				}
				else
				{
					x = x2;
					y = y2;
					xe = x1;
				}

				drawPixel(x, y, color);
        
				for (i = 0; x < xe; i++)
				{
					x = x + 1;
					if (px < 0)
					{
						px = px + 2 * dy1;
					}
					else
					{
						if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0))
						{
							y = y + 1;
						}
						else
						{
							y = y - 1;
						}
						px = px + 2 * (dy1 - dx1);
					}
					
					drawPixel(x, y, color);
				}
			}
			else
			{
				if (dy >= 0)
				{
					x = x1;
					y = y1;
					ye = y2;
				}
				else
				{
					x = x2;
					y = y2;
					ye = y1;
				}
				
				drawPixel(x, y, color);
				for (i = 0; y < ye; i++)
				{
					y = y + 1;
					if (py <= 0)
					{
						py = py + 2 * dx1;
					}
					else
					{
						if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0))
						{
							x = x + 1;
						}
						else
						{
							x = x - 1;
						}
						py = py + 2 * (dx1 - dy1);
					}

					
					drawPixel(x, y, color);
				}
			}
		}

		public void drawRectangle(Rectangle rect, Color color, bool filled)
		{
			if (filled)
			{
				spriteBatch.Draw(pixel, rect, color);
			}
			else
			{
				drawStraightLine(rect.X, rect.Y, rect.X + rect.Width , rect.Y, color);
				drawStraightLine(rect.X, rect.Y, rect.X, rect.Y + rect.Height , color);
				drawStraightLine(rect.X + rect.Width , rect.Y, rect.X + rect.Width , rect.Y + rect.Height , color);
				drawStraightLine(rect.X, rect.Y + rect.Height , rect.X + rect.Width , rect.Y + rect.Height , color);
			}
		}

		public void drawPath(Point current, Point destination, Color color)
		{
			// If they can be connected by drawing one straight line
			if (current.X == destination.X)
			{
				drawStraightLine(current.X, current.Y, destination.X, destination.Y, color);
			} else if (current.Y == destination.Y)
			{
				drawStraightLine(current.X, current.Y, destination.X, destination.Y, color);
			}
			else // Else draw an 'L'
			{
				// Go either 'I' or '—'. Doesn't matter which one we choose since the final path will always be the same.
				if (Util.random.Next(0, 2) == 0)
				{
					drawStraightLine(current.X, current.Y, destination.X, current.Y, color);	// '—'
					drawStraightLine(destination.X, current.Y, destination.X, destination.Y, color); // 'I'
				}
				else
				{
					drawStraightLine(current.X, current.Y, current.X, destination.Y, color);	// 'I'
					drawStraightLine(current.X, destination.Y, destination.X, destination.Y, color); // '—'
				}
				
			}
		}
	}
}