using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Pikachu.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public static void DrawStringInTopLeft(this SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont spriteFont, string text)
        {
            DrawStringInTopLeft(spriteBatch, graphics, spriteFont, text, Color.Black);
        }

        public static void DrawStringInTopLeft(this SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont spriteFont, string text, Color textColor)
        {
            spriteBatch.DrawString(spriteFont, text, Vector2.Zero, textColor);
        }

        public static void DrawStringInCenter(this SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont spriteFont, string text)
        {
            DrawStringInCenter(spriteBatch, graphics, spriteFont, text, Color.Black);
        }

        public static void DrawStringInCenter(this SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont spriteFont, string text, Color textColor)
        {
            var position = CalculateCenterPosition(graphics, spriteFont, text);

            spriteBatch.DrawString(spriteFont, text, position, textColor);
        }

        private static Vector2 CalculateCenterPosition(GraphicsDeviceManager graphics, SpriteFont spriteFont, string text)
        {
            var textSize = spriteFont.MeasureString(text); // Measure the string, so we can bring it in the center

            return new Vector2((graphics.PreferredBackBufferWidth - textSize.X) * 0.5F,
                               (graphics.PreferredBackBufferHeight - textSize.Y) * 0.5F); // Multiplication is 10 or so times faster than divisions
        }
    }
}
