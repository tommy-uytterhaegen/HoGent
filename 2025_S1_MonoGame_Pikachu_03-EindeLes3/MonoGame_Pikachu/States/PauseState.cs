using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Core;
using MonoGame_Pikachu.Extensions;
using MonoGame_Pikachu.Services.InputServices;
using MonoGame_Pikachu.States.Base;

namespace MonoGame_Pikachu.States
{
    /// <summary>
    /// Pauzing the game
    /// </summary>
    /// <param name="context"></param>
    /// <param name="originState"></param>
    internal class PauseState(Game1 context, State originState)
        : State(context)
    {
        // We keep track what state brought us here, so we can go back to the same state
        private State OriginState { get; } = originState;

        public override void Update(GameTime gameTime)
        {
            // When the user pressed enter, it will return the system to the Playing state, but we will be using the origin
            // We want to return to the exact state that 'paused' the game. Not a new state.
            if (InputFacade.IsKeyDown(Keys.P))
                Context.ChangeState(OriginState);
        }

        public override void Draw(GameTime gameTime)
        {
            // Because we have the origin state, we can just call its 'Draw' method and have the current game screen visible to the user.
            // Nothing will move since we aren't calling the 'Update' method on the OriginState, we just draw
            OriginState.Draw(gameTime);

            // Now we put the paused text on top
            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font, 
                "Pause. Press enter to resume.");
        }

    }
}
