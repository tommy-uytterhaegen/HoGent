using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Pikachu.States
{
    public abstract class AbstractState(Game1 context) 
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
