using Microsoft.Xna.Framework;

namespace MonoGame_Pikachu.States.Base
{
    // More information about the 'State Pattern' can be found here: https://refactoring.guru/design-patterns/state
    public abstract class State(Game1 context) 
    {
        protected Game1 Context { get; } = context;

        /* 
            We force every state to have an 'Update' method, we also force every state to have the 'Draw' method
            Those 2 methods are also the only real difference between all states
         */

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}
