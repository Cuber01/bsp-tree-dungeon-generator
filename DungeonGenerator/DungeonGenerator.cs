using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonGenerator
{
    public class DungeonGenerator : Game
    {
        
        private static SpriteBatch spriteBatch;
        private static GraphicsDeviceManager graphics;

        private readonly Color mainColor = new Color(122, 21, 17);
        private readonly Color backgroundColor = new Color(23, 12, 17);

        private const int mapWidth = 250;
        private const int mapHeight = 250;
        private const int scale = 2;


        public DungeonGenerator()
        {
             graphics = new GraphicsDeviceManager(this);
             Content.RootDirectory = "Content";
             IsMouseVisible = true;
        }

        
        protected override void Initialize()
        {
            // Branch root = new Branch(0, 0, 0, mapWidth, mapHeight);
            // root.split();
            //
            // root.printInfo();
            
            graphics.PreferredBackBufferWidth = mapWidth * scale;  
            graphics.PreferredBackBufferHeight = mapHeight * scale; 
            graphics.ApplyChanges();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            
            spriteBatch.Begin();
            
            // TODO you can't make a new object every frame 
            PrimitiveDraw draw = new PrimitiveDraw(GraphicsDevice, spriteBatch);

            Rectangle rect = new Rectangle(20, 20, 40, 40);
            draw.drawRectangle(rect, mainColor, true);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}