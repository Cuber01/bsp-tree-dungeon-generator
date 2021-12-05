using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonGenerator
{
    public class DungeonGenerator : Game
    {
        
        private static SpriteBatch spriteBatch;
        private static GraphicsDeviceManager graphics;

        private readonly Color lineColor = new Color(122, 21, 17);
        private readonly Color roomColor = new Color(227, 224, 255);

        public const int minWidth = 40;
        public const int minHeight = 40;
        
        public const double widthRatio  = 0.45;
        public const double heightRatio = 0.45;

        private const int mapWidth = 250;
        private const int mapHeight = 250;
        private const int scale = 2;


        public DungeonGenerator()
        {
             graphics = new GraphicsDeviceManager(this);
             Content.RootDirectory = "Content";
             IsMouseVisible = true;
        }

        // Create root
        Branch root = new Branch(0, 0, 0, mapWidth, mapHeight);
        
        protected override void Initialize()
        {
            root.split();
            root.makeRooms();

            graphics.PreferredBackBufferWidth  = (mapWidth  *  scale)   + 1;  
            graphics.PreferredBackBufferHeight = (mapHeight * scale)    + 1; 
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
            PrimitiveDraw draw = new PrimitiveDraw(GraphicsDevice, spriteBatch);
            var scaleMatrix = Matrix.CreateScale(scale, scale, 1.0f);

            //GraphicsDevice.Clear(backgroundColor);
            spriteBatch.Begin(transformMatrix: scaleMatrix);
            
            root.drawSections(draw, lineColor);
            root.drawRooms(draw, roomColor);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}