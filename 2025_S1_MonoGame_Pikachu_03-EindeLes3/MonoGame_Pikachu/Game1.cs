using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Core;
using MonoGame_Pikachu.Object;
using MonoGame_Pikachu.Services;
using MonoGame_Pikachu.States;
using MonoGame_Pikachu.States.Base;
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

        internal int _numberOfRemainLives;

        internal PlayerSprite Player;

        internal Texture2D _background;
        internal Vector2 _backgroundPosition;

        internal double _elapsedTimeSinceLastSharkInMs;

        internal Texture2D _shark;
        internal List<Vector2> _sharkPositions;

        private State _currentState;

        #region Constructor

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #endregion

        // The State themselves will change the state. They are the ones who know what states they can move to.
        internal void ChangeState(State newState)
        {
            // Trust that the state that is setting the new state knows what it is doing, so no validation. They know more than the context (Game1.cs)
            _currentState = newState;
        }

        protected override void Initialize()
        {
            // Setting the window size. If not set it will default to 720x540
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges(); // Don't forget to apply, will not set otherwise.

            ContentService.Initialize(this);

            // This is the only state that Game1 has to know about: the start of it all
            ChangeState(new StartScreenState(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Taking a reference to the SpriteBatch, the base object for drawing our sprites & text
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load our textures (our sprites)
            _background = Content.Load<Texture2D>("water");
            _shark = Content.Load<Texture2D>("haai");

            // Loading a font, no font -> no text
            _font = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            InputFacade.Update();

            // No matter which state, we always check if the user wants to exit the game
            if (InputFacade.WasKeyJustPressed(Keys.Escape))
                Exit();

            // There is only 1 Update method to call: the one for the active state and Game1 doesn't care what is behind it
            _currentState?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // There is only 1 draw method to call: the one for the active state and Game1 doesn't care what is behind it
            _currentState?.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
