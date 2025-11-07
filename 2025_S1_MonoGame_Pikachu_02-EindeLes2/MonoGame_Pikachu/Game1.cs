using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Extensions;
using MonoGame_Pikachu.States;
using System;
using System.Collections.Generic;

namespace MonoGame_Pikachu
{
    public class Game1 : Game
    {
        // It is always a good idea to have specific numbers as constants. Makes it easy to find & change them to tweak the game
        internal const int PLAYER_STEP = 8;
        internal const int BACKGROUND_STEP = 2;
        internal const int SHARK_STEP = 4;

        internal GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        internal SpriteFont _font;
        internal Texture2D _player;
        internal Texture2D _shark;
        internal Texture2D _background;

        internal int _numberOfRemainLives;

        internal Vector2 _playerPosition;
        internal Vector2 _backgroundPosition;

        internal double _elapsedTimeSinceLastSharkInMs;

        internal List<Vector2> _sharkPositions;

        private AbstractState _currentState;
        internal void ChangeState(AbstractState newState)
        {
            _currentState = newState;
        }

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
            ChangeState(new StartScreenState(this));

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
            
            _currentState?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentState?.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
