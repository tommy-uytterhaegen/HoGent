using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Extensions;
using System;
using System.Collections.Generic;

namespace MonoGame_Pikachu
{
    public class Game1 : Game
    {
        private enum GameStates
        {
            StartScreen,
            Playing,
            Paused,
            GameOver
        }

        // It is always a good idea to have specific numbers as constants. Makes it easy to find & change them to tweak the game
        private const int PLAYER_STEP = 8;
        private const int BACKGROUND_STEP = 2;
        private const int SHARK_STEP = 4;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;
        private Texture2D _player;
        private Texture2D _shark;
        private Texture2D _background;
        private GameStates _gameState;

        private int _numberOfRemainLives;

        private Vector2 _playerPosition;
        private Vector2 _backgroundPosition;

        private double _elapsedTimeSinceLastSharkInMs;

        private List<Vector2> _sharkPositions;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Setting the window size. If not set it will default to 720x540
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges(); // Don't forget to apply, will not set otherwise.

            // Reset all variables
            _gameState = GameStates.StartScreen;
            _elapsedTimeSinceLastSharkInMs = 0;
            _playerPosition = new Vector2(0, 100);
            _backgroundPosition = new Vector2(0, -700);
            _numberOfRemainLives = 3;
            _sharkPositions = new List<Vector2>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Taking a reference to the SpriteBatch, the base object for drawing our sprites & text
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load our textures (our sprites)
            _background = Content.Load<Texture2D>("water");
            _player = Content.Load<Texture2D>("surfing-pikachu");
            _shark = Content.Load<Texture2D>("haai");

            // Loading a font, no font -> no text
            _font = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            // No matter which state, we always check if the user wants to exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if ( _gameState == GameStates.StartScreen )
            {
                _elapsedTimeSinceLastSharkInMs = 0;
                _playerPosition = new Vector2(0, 100);
                _backgroundPosition = new Vector2(0, -700);
                _numberOfRemainLives = 3;
                _sharkPositions.Clear();

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.Playing;
            }
            else if ( _gameState == GameStates.Playing )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) // Right
                    _playerPosition.X += PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Q) || Keyboard.GetState().IsKeyDown(Keys.Left)) // Left
                    _playerPosition.X -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Z) || Keyboard.GetState().IsKeyDown(Keys.Up)) // Up
                    _playerPosition.Y -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) // Down
                    _playerPosition.Y += PLAYER_STEP;

                // Check if the user wants to Pause the game
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                    _gameState = GameStates.Paused;

                // Slide background
                _backgroundPosition.X -= BACKGROUND_STEP;

                // Shark Update
                // Keep tracker of how much time has passed since the last shark generation
                _elapsedTimeSinceLastSharkInMs += gameTime.ElapsedGameTime.TotalMilliseconds;
                if ( _elapsedTimeSinceLastSharkInMs > 3_000 )
                {
                    // Resetting the elapsedTime
                    _elapsedTimeSinceLastSharkInMs = 0;

                    // Adding a new shark (which means adding a new position)
                    _sharkPositions.Add(new Vector2(_graphics.PreferredBackBufferWidth, Random.Shared.Next(_graphics.PreferredBackBufferHeight)));
                }

                // Check if we have collided with a shark
                var pikachuBounds = new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, _player.Width, _player.Height);
                for (var i = _sharkPositions.Count - 1; i >= 0; i--)
                {
                    // Update the X position
                    _sharkPositions[i] = _sharkPositions[i] with { X = _sharkPositions[i].X - SHARK_STEP };

                    // Remove useless sharks (those who have passed the left side)
                    if (_sharkPositions[i].X  < -_shark.Width)
                    {
                        _sharkPositions.RemoveAt(i);
                    }
                    // Check if it intersects with the pikachu bounds, if so it means that the shark hit pikachu
                    else if (pikachuBounds.Intersects(new Rectangle((int)_sharkPositions[i].X, (int)_sharkPositions[i].Y, _shark.Width, _shark.Height)))
                    {
                        _numberOfRemainLives--;

                        // If the player has no lives left change state to gameover
                        if (_numberOfRemainLives == 0)
                        {
                            _gameState = GameStates.GameOver;
                            continue; // State changed, we don't need to do the rest of the loop
                        }
                        // Still have lives left, so just remove the shark
                        else
                            _sharkPositions.RemoveAt(i);
                    }
                }

            }
            else if (_gameState == GameStates.Paused )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.Playing;
            }
            else if (_gameState == GameStates.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.StartScreen;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if ( _gameState == GameStates.StartScreen )
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Druk op enter om te spelen");
            }
            else if (_gameState == GameStates.Playing)
            {
                // Draw the background
                _spriteBatch.Draw(_background, _backgroundPosition);

                // Draw all sharks
                foreach (var sharkPosition in _sharkPositions)
                    _spriteBatch.Draw(_shark, sharkPosition);

                // Draw the player
                _spriteBatch.Draw(_player, _playerPosition);

                // Draw the number of lives the player has left
                _spriteBatch.DrawStringInTopLeft(_graphics, _font, "Levens: " + _numberOfRemainLives, Color.DimGray);
            }
            else if (_gameState == GameStates.Paused)
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Paused. Press enter to resume.");
            }
            else if ( _gameState == GameStates.GameOver)
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Game Over. Press enter to go back to the startscreen.");
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
