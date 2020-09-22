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
        Texture2D pauseButton;
        Texture2D stopButton;
        Texture2D whiteBackground;
        Texture2D filledGridBox;
        Texture2D unFilledGridBox;

        Vector2 playButtonPosition;
        Vector2 pauseButtonPosition;
        Vector2 stopButtonPosition;

        MouseState mouseState;

        bool playButtonBool = false;

        Color blackish;
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

            blackish = new Color(19, 23, 28);

            playButtonPosition = new Vector2(30, 650);
            pauseButtonPosition = new Vector2(260, 650);
            stopButtonPosition = new Vector2(500, 650);

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
            pauseButton = Content.Load<Texture2D>("pauseButton");
            stopButton = Content.Load<Texture2D>("stopButton");
            whiteBackground = Content.Load<Texture2D>("whiteBackground");
            filledGridBox = Content.Load<Texture2D>("filledGridBox");
            unFilledGridBox = Content.Load<Texture2D>("unfilledGridBox");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.X >= playButtonPosition.X && mouseState.X <= playButtonPosition.X + 115 && mouseState.Y >= playButtonPosition.Y && mouseState.Y <= playButtonPosition.Y + 50)
            {
                playButtonBool = true;
            }
            else
            {
                playButtonBool = false;
            }

            base.Update(gameTime);
        }

        private void DrawPlayButton(bool hover)
        {
            if (hover)
            {
                _spriteBatch.Draw(playButton, playButtonPosition, Color.Black);
            }
            else
            {
                _spriteBatch.Draw(playButton, playButtonPosition, Color.White);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(blackish);

            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteBackground, new Vector2(10, 10), Color.White);

            DrawPlayButton(playButtonBool);

            _spriteBatch.Draw(pauseButton, pauseButtonPosition, Color.White);
            _spriteBatch.Draw(stopButton, stopButtonPosition, Color.White);
            for (int x = 0; x < 25; x++)
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
