using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Core;
using MonoGame_Pikachu.Extensions;
using MonoGame_Pikachu.Object;
using MonoGame_Pikachu.Services;
using MonoGame_Pikachu.Services.InputServices;
using MonoGame_Pikachu.States.Base;
using System.Collections.Generic;

namespace MonoGame_Pikachu.States
{
    /// <summary>
    /// State for the 'Menu', the start screen
    /// </summary>
    /// <param name="context"></param>
    public class StartScreenState(Game1 context) 
        : State(context)
    {
        private bool _isInitialized = false;

        public override void Update(GameTime gameTime)
        {
            // Making sure we only reset the variables onces. Remember, this method will - ideally - run every 16.6ms
            if (!_isInitialized)
            {
                ResetContext();

                _isInitialized = true;
            }

            // Only real thing that needs to be checked. When the user pressed enter it will change state to 'Playing'
            if (InputFacade.IsKeyDown(Keys.Enter))
                Context.ChangeState(new PlayingState(Context));
        }


        private void ResetContext()
        {
            Context._elapsedTimeSinceLastSharkInMs = 0;
            
            Context.Player = new PlayerSprite(ContentService.Instance.GetTexture(ContentService.Player), 
                                              new Vector2(0, 100));

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
