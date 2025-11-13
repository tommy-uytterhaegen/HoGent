using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Pikachu.Extensions;

namespace MonoGame_Pikachu.Core.Base
{
    // Basis klasse voor een sprite
    // TODO: Zet haai & background ook om naar een eigen object, overervend van Sprite
    public abstract class Sprite(Texture2D texture, Vector2 position)
    {
        public Texture2D Texture { get; } = texture;

        public Vector2 Position { get; private set; } = position;

        public int Width
            => Texture.Width;

        public int Height
            => Texture.Height;

        public Rectangle GetBounds()
            => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position);
        }

        internal void ChangeXPosition(float xChange)
        {
            Position = Position with { X = Position.X + xChange };
        }

        internal void ChangeYPosition(float yChange)
        {
            Position = Position with { Y = Position.Y + yChange };
        }
    }
}
