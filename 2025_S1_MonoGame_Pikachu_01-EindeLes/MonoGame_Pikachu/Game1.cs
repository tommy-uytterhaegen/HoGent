using _2025_S1_MonoGame_Pikachu.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _2025_S1_MonoGame_Pikachu
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

        private const int PLAYER_STEP = 4;
        private const int BACKGROUND_STEP = 2;
        private const int SHARK_STEP = 3;

        private GameStates _gameState = GameStates.StartScreen;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _player;
        private Texture2D _background;
        private Texture2D _shark;
        private SpriteFont _font;

        private Vector2 _playerPosition;       
        private Vector2 _backgroundPosition;   
        private List<Vector2> _sharkPositions;

        private double _elapsedTimeInMs = 0;

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
            _playerPosition = new Vector2(0, 0);
            _backgroundPosition = new Vector2(0, -700);
            _sharkPositions = new List<Vector2>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = Content.Load<Texture2D>("surfing-pikachu");
            _background = Content.Load<Texture2D>("water");
            _shark = Content.Load<Texture2D>("haai");
            _font = Content.Load<SpriteFont>("game-font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if ( _gameState == GameStates.StartScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.Playing;

                _elapsedTimeInMs = 0;
                _playerPosition = new Vector2(0, 0);
                _backgroundPosition = new Vector2(0, -700);
                _sharkPositions.Clear();
            }
            else if (_gameState == GameStates.Playing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D)) // Right
                    _playerPosition.X += PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Q)) // Left
                    _playerPosition.X -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Z)) // Up
                    _playerPosition.Y -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.S)) // Down
                    _playerPosition.Y += PLAYER_STEP;

                _backgroundPosition.X -= BACKGROUND_STEP;

                _elapsedTimeInMs += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_elapsedTimeInMs >= 1_000) // Shark per second
                {
                    _elapsedTimeInMs = 0;
                    _sharkPositions.Add(new Vector2(_graphics.PreferredBackBufferWidth, Random.Shared.Next(_graphics.PreferredBackBufferHeight)));
                }

                for (var i = 0; i <_sharkPositions.Count; i++)
                {
                    _sharkPositions[i] = _sharkPositions[i] with { X = _sharkPositions[i].X - SHARK_STEP };
                }
                
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    _gameState = GameStates.Paused;
            }
            else if (_gameState == GameStates.Paused)
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

            if (_gameState == GameStates.StartScreen)
            {
                _spriteBatch.DrawString(_font, "Druk enter om te spelen.", Vector2.Zero, Color.Black);
            }
            else if (_gameState == GameStates.Playing)
            {
                _spriteBatch.Draw(_background, _backgroundPosition);
                _spriteBatch.Draw(_player, _playerPosition);

                foreach (var sharkPosition in _sharkPositions)
                    _spriteBatch.Draw(_shark, sharkPosition);
            }
            else if (_gameState == GameStates.Paused)
            {
                _spriteBatch.DrawString(_font, "Druk enter om verder te spelen.", Vector2.Zero, Color.Black);
            }
            else if (_gameState == GameStates.GameOver)
            {
                _spriteBatch.DrawString(_font, "Game Over. Druk enter om naar het startscherm te gaan.", Vector2.Zero, Color.Black);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
