using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2025_S1_MonoGame_Pikachu.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
