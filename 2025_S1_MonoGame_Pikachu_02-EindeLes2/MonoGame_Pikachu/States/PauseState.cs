using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Pikachu.States
{
    internal class PauseState(Game1 context, AbstractState originState)
        : AbstractState(context)
    {
        private AbstractState OriginState { get; } = originState;

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(OriginState);
        }

        public override void Draw(GameTime gameTime)
        {
            OriginState.Draw(gameTime);

            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font, 
                "Pause. Press enter to resume.");
        }

    }
}
