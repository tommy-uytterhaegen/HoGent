using Microsoft.Xna.Framework;
using MonoGame_Pikachu.Extensions;
using MonoGame_Pikachu.Services.InputServices;
using MonoGame_Pikachu.States.Base;
using System;

namespace MonoGame_Pikachu.States
{
    public class PlayingState(Game1 context)
        : State(context)
    {
        private PlayingStateInputService InputService { get; } = new PlayingStateInputService();

        public override void Update(GameTime gameTime)
        {
            Context.Player.Update(gameTime, InputService);

            // Check if the user wants to Pause the game
            if (InputService.ShouldPause())
                Context.ChangeState(new PauseState(Context, this));

            // Slide background
            Context._backgroundPosition.X -= Game1.BACKGROUND_STEP;

            // Shark Update
            // Keep tracker of how much time has passed since the last shark generation
            Context._elapsedTimeSinceLastSharkInMs += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Context._elapsedTimeSinceLastSharkInMs > 3_000)
            {
                // Resetting the elapsedTime
                Context._elapsedTimeSinceLastSharkInMs = 0;

                // Adding a new shark (which means adding a new position)
                Context._sharkPositions.Add(new Vector2(Context._graphics.PreferredBackBufferWidth, Random.Shared.Next(Context._graphics.PreferredBackBufferHeight)));
            }

            // Check if we have collided with a shark
            var pikachuBounds = Context.Player.GetBounds();
            for (var i = Context._sharkPositions.Count - 1; i >= 0; i--)
            {
                // Update the X position
                Context._sharkPositions[i] = Context._sharkPositions[i] with { X = Context._sharkPositions[i].X - Game1.SHARK_STEP };

                // Remove useless sharks (those who have passed the left side)
                if (Context._sharkPositions[i].X < -Context._shark.Width)
                {
                    Context._sharkPositions.RemoveAt(i);
                }
                // Check if it intersects with the pikachu bounds, if so it means that the shark hit pikachu
                else if (pikachuBounds.Intersects(new Rectangle((int)Context._sharkPositions[i].X, (int)Context._sharkPositions[i].Y, Context._shark.Width, Context._shark.Height)))
                {
                    Context._numberOfRemainLives--;

                    // If the player has no lives left change state to gameover
                    if (Context._numberOfRemainLives == 0)
                        Context.ChangeState(new GameOverState(Context));
                    else // Still have lives left, so just remove the shark
                        Context._sharkPositions.RemoveAt(i);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        { 
            // Draw the background
            Context._spriteBatch.Draw(
                Context._background, 
                Context._backgroundPosition);

            // Draw all sharks
            foreach (var sharkPosition in Context._sharkPositions)
                Context._spriteBatch.Draw(
                    Context._shark, 
                    sharkPosition);

            // Draw the player
            Context.Player.Draw(Context._spriteBatch);

            // Draw the number of lives the player has left
            Context._spriteBatch.DrawStringInTopLeft(
                Context._graphics,
                Context._font, 
                "Levens: " + Context._numberOfRemainLives, 
                Color.DimGray);
        }
    }
}
