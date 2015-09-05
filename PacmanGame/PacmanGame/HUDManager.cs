using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace PacmanGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// 
    namespace HUDManager
    {
        public class Timer : Microsoft.Xna.Framework.GameComponent
        {
            SpriteBatch spriteBatch;
            
            public int Player1Score;
            //{ 
            //    get { return Player1Score; } 
            //    set { Player1Score = value; } 
            //}
            public int Player2Score;
            //{ 
            //    get { return Player2Score; } 
            //    set { Player2Score = value; } 
            //}
            //public float timer;
            public int seconds;
            int hours, min;
            string gtime;

            public float timer;

            public Timer(Game game)
                : base(game)
            {
                // TODO: Construct any child components here
                spriteBatch = new SpriteBatch(game.GraphicsDevice);
                timer = 0;
                seconds = 0;
                min = 0;
                hours = 0;
                //Player1Lives = 0;
                Player1Score = 0;
                //Player2Lives = 0;
                Player2Score = 0;
            }

            /// <summary>
            /// Allows the game component to perform any initialization it needs to before starting
            /// to run.  This is where it can query for any required services and load content.
            /// </summary>
            public override void Initialize()
            {
                // TODO: Add your initialization code here

                base.Initialize();
            }
            public void DisplayTime(GameTime gameTime, SpriteFont font, int positionX, int positionY, Color color)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                seconds += (int)timer;
                if (timer >= 1.0F)
                    timer = 0F;
                if (seconds >= 60)
                {
                    min++;
                    seconds = 0;
                }
                if (min >= 60)
                {
                    hours++;
                    min = 0;
                }
                if (hours >= 24)
                {
                    hours = min = seconds = 0;
                }

                gtime = hours.ToString() + ":" + min.ToString() + ":" + seconds.ToString();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.DrawString(font, gtime, new Vector2(positionX, positionY), color);
                spriteBatch.End();
            }
            /// <summary>
            /// Allows the game component to update itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            public override void Update(GameTime gameTime)
            {
                // TODO: Add your update code here

                base.Update(gameTime);
            }
        }
        public class Lives : Microsoft.Xna.Framework.GameComponent
        {
            SpriteBatch spriteBatch;
            public int NumberOfPlayers;
            public int[] Players;
            
            public Lives(Game game)
                : base(game)
            {
                // TODO: Construct any child components here
                spriteBatch = new SpriteBatch(game.GraphicsDevice);
                NumberOfPlayers = 0;
               
            }
            public override void Initialize()
            {
                // TODO: Add your initialization code here

                base.Initialize();
            }
            public void DisplayLives(GameTime gameTime, SpriteFont font, int positionX, int positionY, int NumberOfPlayers, Color color)
            {
                
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    spriteBatch.DrawString(font, Players[i].ToString(), new Vector2(positionX, positionY), color);
                }
                spriteBatch.End();
            }
        }
    }
}