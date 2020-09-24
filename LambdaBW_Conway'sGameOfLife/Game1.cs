using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;

namespace LambdaBW_Conway_sGameOfLife
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playButton;
        Texture2D stopButton;
        Texture2D nextGenButton;
        Texture2D whiteBackground;
        Texture2D filledGridBox;
        Texture2D unFilledGridBox;
        Texture2D rules;
        Texture2D filledGreenTile;
        Texture2D unfilledGreenTile;

        Texture2D normalSpeed;
        Texture2D halfSpeed;
        Texture2D quarterSpeed;

        SpriteFont arial;

        Vector2 playButtonPosition;
        Vector2 stopButtonPosition;
        Vector2 nextGenButtonPosition;

        Vector2 normalSpeedPosition;
        Vector2 halfSpeedPosition;
        Vector2 quarterSpeedPosition;

        MouseState mouseState;
        MouseState oldState;

        float time = 0;
        int currentGen = 0;
        double evenOrOdd;
        float speed = 1;

        bool playButtonBool = false;
        bool nextGenBool = false;

        Color blackish;
        Color buttonClickedColor;

        Tile[,] gridArray = new Tile[25, 25];
        Tile[,] swapGrid = new Tile[25, 25];

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
            stopButtonPosition = new Vector2(360, 650);
            nextGenButtonPosition = new Vector2(500, 650);

            quarterSpeedPosition = new Vector2(670, 585);
            halfSpeedPosition = new Vector2(750, 585);
            normalSpeedPosition = new Vector2(830, 585);

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
            nextGenButton = Content.Load<Texture2D>("nextGenButton");
            whiteBackground = Content.Load<Texture2D>("whiteBackground");
            filledGridBox = Content.Load<Texture2D>("filledGridBox");
            unFilledGridBox = Content.Load<Texture2D>("unfilledGridBox");
            rules = Content.Load<Texture2D>("rules");
            arial = Content.Load<SpriteFont>("Fonts/Font");
            normalSpeed = Content.Load<Texture2D>("SpeedButtons/normalSpeed");
            halfSpeed = Content.Load<Texture2D>("SpeedButtons/halfSpeed");
            quarterSpeed = Content.Load<Texture2D>("SpeedButtons/quarterSpeed");
            filledGreenTile = Content.Load<Texture2D>("filledGreenTile");
            unfilledGreenTile = Content.Load<Texture2D>("unfilledGreenTile");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            #region Buttons
            if (mouseState.X >= playButtonPosition.X && mouseState.X <= playButtonPosition.X + 115 && mouseState.Y >= playButtonPosition.Y && mouseState.Y <= playButtonPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                playButtonBool = !playButtonBool;
            }

            if (mouseState.X >= quarterSpeedPosition.X && mouseState.X <= quarterSpeedPosition.X + 115 && mouseState.Y >= quarterSpeedPosition.Y && mouseState.Y <= quarterSpeedPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                speed = 0.25f;
            }

            if (mouseState.X >= halfSpeedPosition.X && mouseState.X <= halfSpeedPosition.X + 115 && mouseState.Y >= halfSpeedPosition.Y && mouseState.Y <= halfSpeedPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                speed = 0.5f;
            }

            if (mouseState.X >= normalSpeedPosition.X && mouseState.X <= normalSpeedPosition.X + 115 && mouseState.Y >= normalSpeedPosition.Y && mouseState.Y <= normalSpeedPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                speed = 1;
            }

            if (mouseState.X >= nextGenButtonPosition.X && mouseState.X <= nextGenButtonPosition.X + 115 && mouseState.Y >= nextGenButtonPosition.Y && mouseState.Y <= nextGenButtonPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                nextGenBool = true;
            }
            #endregion

            if (mouseState.X >= stopButtonPosition.X && mouseState.X <= stopButtonPosition.X + 115 && mouseState.Y >= stopButtonPosition.Y && mouseState.Y <= stopButtonPosition.Y + 50 && mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        gridArray[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                        swapGrid[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                    }
                }
                currentGen = 0;
                playButtonBool = false;
            }

            if (nextGenBool)
            {
                int nextGen = currentGen + 1;
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                evenOrOdd = currentGen % 2;
                if (evenOrOdd == 0)
                {
                    SwapCurrentGrid(swapGrid, gridArray);
                }
                else
                {
                    SwapCurrentGrid(gridArray, swapGrid);
                }
                if (currentGen == nextGen)
                {
                    nextGenBool = false;
                } 
            }

            if (playButtonBool)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                evenOrOdd = currentGen % 2;
                if (evenOrOdd == 0)
                {
                    SwapCurrentGrid(swapGrid, gridArray);
                }
                else
                {
                    SwapCurrentGrid(gridArray, swapGrid);
                }
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
                    evenOrOdd = currentGen % 2;
                    if (evenOrOdd == 0)
                    {
                        if (currentGen >= 10)
                        {
                            if (gridArray[x, y].CurrentState)
                            {
                                _spriteBatch.Draw(filledGreenTile, gridArray[x, y].Location, Color.White);
                            }
                            else
                            {
                                _spriteBatch.Draw(unfilledGreenTile, gridArray[x, y].Location, Color.White);
                            }
                        }
                        else
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
                    else
                    {
                        if (currentGen >= 10)
                        {
                            if (swapGrid[x, y].CurrentState)
                            {
                                _spriteBatch.Draw(filledGreenTile, swapGrid[x, y].Location, Color.White);
                            }
                            else
                            {
                                _spriteBatch.Draw(unfilledGreenTile, swapGrid[x, y].Location, Color.White);
                            }
                        }
                        else
                        {
                            if (swapGrid[x, y].CurrentState)
                            {
                                _spriteBatch.Draw(filledGridBox, swapGrid[x, y].Location, Color.White);
                            }
                            else
                            {
                                _spriteBatch.Draw(unFilledGridBox, swapGrid[x, y].Location, Color.White);
                            }
                        }
                    }
                }
            }
        }

        private void SwapCurrentGrid(Tile[,] nextTile, Tile[,] prevTile)
        {
            if (time >= speed)
            {
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        if (x > 0 && x < 24 && y > 0 && y < 24)
                        {
                            int count = 0;
                            // West || Left
                            if (prevTile[x - 1, y].CurrentState)
                            {
                                count++;
                            }
                            // North West || Up Left
                            if (prevTile[x - 1, y - 1].CurrentState)
                            {
                                count++;
                            }
                            // North || Up
                            if (prevTile[x, y - 1].CurrentState)
                            {
                                count++;
                            }
                            // North East || Up Right
                            if (prevTile[x + 1, y - 1].CurrentState)
                            {
                                count++;
                            }
                            // East || Right
                            if (prevTile[x + 1, y].CurrentState)
                            {
                                count++;
                            }
                            // South East || Down Right
                            if (prevTile[x + 1, y + 1].CurrentState)
                            {
                                count++;
                            }
                            // South || Down
                            if (prevTile[x, y + 1].CurrentState)
                            {
                                count++;
                            }
                            // South West || Down Left
                            if (prevTile[x - 1, y + 1].CurrentState)
                            {
                                count++;
                            }

                            switch (count)
                            {
                                case 2:
                                    nextTile[x, y] = prevTile[x, y];
                                    break;
                                case 3:
                                    nextTile[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), true);
                                    break;
                                default:
                                    nextTile[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                                    break;
                            }
                        }
                        else
                        {
                            nextTile[x, y] = new Tile(new Vector2((x * 25) + 10, (y * 25) + 10), false);
                        }
                    }
                }
                currentGen += 1;
                time = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(blackish);

            _spriteBatch.Begin();
            _spriteBatch.Draw(whiteBackground, new Vector2(10, 10), Color.White);
            _spriteBatch.Draw(rules, new Vector2(710, 10), Color.White);
            _spriteBatch.DrawString(arial, "Current Speed: " + speed, new Vector2(670, 530), Color.White);
            _spriteBatch.DrawString(arial, "Gen: " + currentGen, new Vector2(245, 660), Color.White);

            _spriteBatch.Draw(quarterSpeed, quarterSpeedPosition, Color.White);
            _spriteBatch.Draw(halfSpeed, halfSpeedPosition, Color.White);
            _spriteBatch.Draw(normalSpeed, normalSpeedPosition, Color.White);
            DrawPlayButton(playButtonBool);
            _spriteBatch.Draw(stopButton, stopButtonPosition, Color.White);
            _spriteBatch.Draw(nextGenButton, nextGenButtonPosition, Color.White);

            DrawGrid();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
