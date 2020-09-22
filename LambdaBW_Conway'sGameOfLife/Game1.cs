using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace LambdaBW_Conway_sGameOfLife
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D playButton;
        Texture2D whiteBackground;
        Texture2D filledGridBox;
        Texture2D unFilledGridBox;
        Vector2[,] gridArray = new Vector2[25, 25];

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    gridArray[x, y] = new Vector2((x * 25) + 10, (y * 25) + 10);
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playButton = Content.Load<Texture2D>("playButton");
            whiteBackground = Content.Load<Texture2D>("whiteBackground");
            filledGridBox = Content.Load<Texture2D>("filledGridBox");
            unFilledGridBox = Content.Load<Texture2D>("unfilledGridBox");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteBackground, new Vector2(10, 10), Color.White);
            _spriteBatch.Draw(
                playButton, 
                new Vector2(30, 650), 
                null, 
                Color.White, 
                0, 
                new Vector2(0, 0), 
                0.45f, 
                SpriteEffects.None, 
                0
            );
            for(int x = 0; x < 25; x++)
            {
                for(int y = 0; y < 25; y++)
                {
                    _spriteBatch.Draw(unFilledGridBox, gridArray[x, y], Color.White);
                }
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
