using _2025_S1_MonoGame_Pikachu.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2025_S1_MonoGame_Pikachu
{
    public class Game1 : Game
    {
        private const int PLAYER_STEP = 3;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _player;

        private Vector2 _playerPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _playerPosition = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = Content.Load<Texture2D>("surfing-pikachu");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D)) // Right
                _playerPosition.X += PLAYER_STEP;

            if (Keyboard.GetState().IsKeyDown(Keys.Q)) // Left
                _playerPosition.X -= PLAYER_STEP;

            if (Keyboard.GetState().IsKeyDown(Keys.Z)) // Up
                _playerPosition.Y -= PLAYER_STEP;

            if (Keyboard.GetState().IsKeyDown(Keys.S)) // Down
                _playerPosition.Y += PLAYER_STEP;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_player, _playerPosition);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
