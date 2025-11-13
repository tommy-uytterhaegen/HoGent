using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Pikachu.Core.Base;
using MonoGame_Pikachu.Interface;

namespace MonoGame_Pikachu.Object
{
    public class PlayerSprite : Sprite
    {
        public PlayerSprite(Texture2D texture, Vector2 position) 
            : base(texture, position)
        {
        }

        // We willen de inputservice meekrijgen waar we kunnen aan vragen of er bewogen is
        // Dit is in weze een PlayingStateInputService, maar we willen geen hardcoded link, vandaar de interface
        // Je zou ook gemakkelijk een situatie kunnen bedenken met meerdere levels waar je dus meerdere services hebt die de input geven,
        // maar uiteindelijk zouden ze allemaal de player movement ondersteunen (denk FpsLevelStateInputService & MeleeLevelStateInputService)
        public void Update(GameTime gameTime, IPlayerMovementInputService inputService) 
        {
            if (inputService.ShouldGoRight())
                ChangeXPosition(Game1.PLAYER_STEP);

            if (inputService.ShouldGoLeft())
                ChangeXPosition(-Game1.PLAYER_STEP);

            if (inputService.ShouldGoUp())
                ChangeYPosition(-Game1.PLAYER_STEP);

            if (inputService.ShouldGoDown())
                ChangeYPosition(Game1.PLAYER_STEP);
        }
    }
}
