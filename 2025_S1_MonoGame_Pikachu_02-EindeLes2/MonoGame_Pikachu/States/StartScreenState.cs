using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MonoGame_Pikachu.States
{
    public class StartScreenState(Game1 context) 
        : AbstractState(context)
    {
        private bool _isInitialized = false;
        public override void Update(GameTime gameTime)
        {
            if (!_isInitialized)
            {
                ResetContext();

                _isInitialized = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(new PlayingState(Context));
        }


        private void ResetContext()
        {
            Context._elapsedTimeSinceLastSharkInMs = 0;
            Context._playerPosition = new Vector2(0, 100);
            Context._backgroundPosition = new Vector2(0, -700);
            Context._numberOfRemainLives = 3;
            Context._sharkPositions = new List<Vector2>();
        }

        public override void Draw(GameTime gameTime)
        {
            Context._spriteBatch.DrawStringInCenter(
            Context._graphics, 
            Context._font, 
            "Druk op enter om te spelen");
        }
    }
}
