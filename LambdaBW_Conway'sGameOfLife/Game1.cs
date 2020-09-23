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
        Texture2D stopButton;
        Texture2D whiteBackground;
        Texture2D filledGridBox;
        Texture2D unFilledGridBox;
        Texture2D rules;

        Vector2 playButtonPosition;
        Vector2 stopButtonPosition;

        MouseState mouseState;
        MouseState oldState;

        bool playButtonBool = false;

        Color blackish;
        Color buttonClickedColor;

        Tile[,] gridArray = new Tile[25, 25];

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
            buttonClickedColor = new Color(19, 23, 28, 1);

            playButtonPosition = new Vector2(100, 650);
            stopButtonPosition = new Vector2(400, 650);

            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    gridArray[x, y] = new Tile( new Vector2((x * 25) + 10, (y * 25) + 10), false );
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playButton = Content.Load<Texture2D>("playButton");
            stopButton = Content.Load<Texture2D>("stopReset");
            whiteBackground = Content.Load<Texture2D>("whiteBackground");
            filledGridBox = Content.Load<Texture2D>("filledGridBox");
            unFilledGridBox = Content.Load<Texture2D>("unfilledGridBox");
            rules = Content.Load<Texture2D>("rules");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.X >= playButtonPosition.X && mouseState.X <= playButtonPosition.X + 115 && mouseState.Y >= playButtonPosition.Y && mouseState.Y <= playButtonPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                playButtonBool = !playButtonBool;
            }

            if (mouseState.X >= stopButtonPosition.X && mouseState.X <= stopButtonPosition.X + 115 && mouseState.Y >= stopButtonPosition.Y && mouseState.Y <= stopButtonPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        gridArray[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                    }
                }
                playButtonBool = false;
            }

            if (playButtonBool)
            {

            }
            else
            {
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        if (mouseState.X >= gridArray[x, y].Location.X && mouseState.X <= gridArray[x, y].Location.X + 25 && mouseState.Y >= gridArray[x, y].Location.Y && mouseState.Y <= gridArray[x, y].Location.Y + 25 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                        {
                            gridArray[x, y].CurrentState = !gridArray[x, y].CurrentState;
                        }
                    }
                }
            }

            oldState = mouseState;

            base.Update(gameTime);
        }

        private void DrawPlayButton(bool hover)
        {
            if (hover)
            {
                _spriteBatch.Draw(playButton, playButtonPosition, buttonClickedColor);
            }
            else
            {
                _spriteBatch.Draw(playButton, playButtonPosition, Color.White);
            }
        }

        private void DrawGrid()
        {
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    if (gridArray[x, y].CurrentState) 
                    {
                        _spriteBatch.Draw(filledGridBox, gridArray[x, y].Location, Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(unFilledGridBox, gridArray[x, y].Location, Color.White);
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(blackish);

            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteBackground, new Vector2(10, 10), Color.White);
            _spriteBatch.Draw(rules, new Vector2(710, 10), Color.White);

            DrawPlayButton(playButtonBool);
            _spriteBatch.Draw(stopButton, stopButtonPosition, Color.White);

            DrawGrid();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
