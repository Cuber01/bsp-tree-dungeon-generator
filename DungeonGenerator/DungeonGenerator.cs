using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonGenerator
{
    public class DungeonGenerator : Game
    {

        private int mapWidth = 250;
        private int mapHeight = 250;
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        

        public DungeonGenerator()
        {
             graphics = new GraphicsDeviceManager(this);
             Content.RootDirectory = "Content";
             IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Branch root = new Branch(0, 0, 0, mapWidth, mapHeight);
            root.split();
            
            root.printInfo();
            
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}