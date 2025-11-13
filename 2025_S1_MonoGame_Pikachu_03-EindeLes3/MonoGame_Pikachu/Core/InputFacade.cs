using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame_Pikachu.Core
{
    // De encapsulation van MonoGame classes
    public static class InputFacade
    {
        private static KeyboardState CurrentState { get; set; }
        private static KeyboardState PreviousState { get; set; }

        // Deze method moet in elke Update van game aangeroepen worden om de nieuwe keyboard state op te vragen en de vorige bij te houden
        public static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public static bool IsKeyDown(IEnumerable<Keys> keys)
            => keys.Any(IsKeyDown);
        
        public static bool IsKeyDown(Keys key)
            => CurrentState.IsKeyDown(key);

        // Geen herhaling, zal enkel 'true' geven als de key juist ingedrukt is
        public static bool WasKeyJustPressed(Keys key)
            => PreviousState.IsKeyUp(key) && CurrentState.IsKeyDown(key);
        
    }
}
