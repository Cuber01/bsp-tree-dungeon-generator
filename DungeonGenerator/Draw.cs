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
		
		public void drawPixel(int x, int y, Color color)
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

		public void drawRectangle(Rectangle rect, Color color, bool filled)
		{
			if (filled)
			{
				spriteBatch.Draw(pixel, rect, color);
			}
			else
			{
				drawStraightLine(rect.X, rect.Y, rect.X + rect.Width, rect.Y, color);
				drawStraightLine(rect.X, rect.Y, rect.X, rect.Y + rect.Height, color);
				drawStraightLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height, color);
				drawStraightLine(rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height, color);
			}
		}
	}
}