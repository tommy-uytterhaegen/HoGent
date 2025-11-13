using Microsoft.Xna.Framework.Input;
using MonoGame_Pikachu.Core;
using MonoGame_Pikachu.Interface;

namespace MonoGame_Pikachu.Services.InputServices
{
    // TODO: Maak de InputServiceses voor de andere states
    public class PlayingStateInputService 
        : IPlayerMovementInputService // Implementeert de player movements
    {
        public bool ShouldGoRight()
        {
            // TODO: Hier zouden we nog een config kunnen achter steken (simpele xml of json file)
            if (InputFacade.IsKeyDown([Keys.D, Keys.Right]))
                return true;

            return false;
        }

        public bool ShouldGoLeft()
        {
            // TODO: Hier zouden we nog een config kunnen achter steken (simpele xml of json file)
            if (InputFacade.IsKeyDown([Keys.Q, Keys.Left]))
                return true;

            return false;
        }

        public bool ShouldGoUp()
        {
            // TODO: Hier zouden we nog een config kunnen achter steken (simpele xml of json file)
            if (InputFacade.IsKeyDown([Keys.Z, Keys.Up]))
                return true;

            return false;
        }

        public bool ShouldGoDown()
        {
            // TODO: Hier zouden we nog een config kunnen achter steken (simpele xml of json file)
            if (InputFacade.IsKeyDown([Keys.S, Keys.Down]))
                return true;

            return false;
        }

        public bool ShouldPause()
        {
            if (InputFacade.IsKeyDown(Keys.P))
                return true;

            return false;
        }
    }
}
