using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame_Pikachu.Core
{
    // De encapsulation van MonoGame classes
    public static class ContentFacade
    {
        public static IEnumerable<Texture2D> LoadTexture2D(Game game, string[] names)
            => names?.Select(name => LoadTexture2D(game, name));

        public static Texture2D LoadTexture2D(Game game, string name)
         => game.Content.Load<Texture2D>(name);
    }
}
