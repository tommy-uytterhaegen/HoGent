using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Pikachu.Core;
using System;
using System.Collections.Generic;

namespace MonoGame_Pikachu.Services
{
    public class ContentService
    {
        private static ContentService _instance = null;
        
        /// <summary>
        /// Makes sure that the Con,tentService has access to the Content class via Game
        /// </summary>
        /// <param name="game"></param>
        public static void Initialize(Game game)
        {
            _instance = new ContentService(game);
        }

        /// <summary>
        /// There can be only 1 instance, implemented as Singleton
        /// </summary>
        public static ContentService Instance
            => _instance ?? throw new Exception("Initialize was not called."); // Property met alleen een getter. Indien instance == null dan zal uitgevoerd worden wat na ?? komt, in dit geval een exception gethrowed

        // TODO: Voeg overige textures toe
        public const string Player = "surfing-pikachu";

        // TODO: Voeg een nieuwe dictionary voor de SpriteFont toe
        private readonly Dictionary<string, Texture2D> _textures;

        // Private constructor want we willen maar 1 instantie, dewelke beheert wordt door ContentService zelf
        private ContentService(Game game)
        {
            _textures = new Dictionary<string, Texture2D>();

            // Load our textures (our sprites)
            _textures.Add(Player, ContentFacade.LoadTexture2D(game, Player));
        }

        public Texture2D GetTexture(string name)
        {
            return _textures[name];
        }
    }
}
