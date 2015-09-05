using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using HUD;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont scoreText;

        HUD.Timer timer;

        Rectangle[] pts;
        Rectangle[] rects;
        Rectangle tunnel1;
        Rectangle tunnel2;
        Rectangle closedoor;
        int totalGhosts = 2;
        int possiblePoints;
        int noRects;
        int noPts;
        int Score;
        int Level;
        int Lives;

        Texture2D spriteClosed;
        Texture2D spriteUp;
        Texture2D spriteDown;
        Texture2D spriteLeft;
        Texture2D spriteRight;
        Texture2D ghost;
        Texture2D ghostEye;
        Texture2D ghostPupil;
        Texture2D rect;
        Texture2D door;
        Texture2D smPoint;
        Texture2D gameover;
        Texture2D background;

        int[] ghostX; // Arrays to hold the X,Y co-ordinate for each ghost
        int[] ghostY;
        int[] ghostmoveX; // Arrays to hold the new X,Y co-ordinate for each ghost
        int[] ghostmoveY;
        int[] ghostLastDirection; // Holds a number representing the last direction of a ghost
        int noGhosts; // Holds the number of ghosts in the game
        int randomSeed = 0;
        double storedSecs = 0;

        int spriteDirection = 3; // Starts pacman facing right
        int spriteX = 463;  // Starting positions for the Pacman character
        int spriteY = 491;  // (just above the ghost's door!!!! :)
        int moveY = 0;
        int moveX = 0;
        bool mouthOpen = false;
        bool[] changeDirection;
        bool[] gtChange;
        bool[] forceGhosts;
        bool forced = false;
        bool endGame = false;
        double firstRun = 0; // gameTime seconds at the time the game started
        bool firstCheck = false; // check to see if the game was started
        bool flashDoor = false;
        bool shutDoor = false;
        bool dying = false;
        bool gameRunning = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 960;
            this.graphics.PreferredBackBufferHeight = 720;

            Score = 0; // Reset the score
            Level = 1; // Reset the level
            Lives = 3; // Reset the lives

            CreateGhosts(totalGhosts); // Create all the ghosts

            CreateOutOfBounds(); // Create all the world-boundaries

            CreatePoints(); // Create all the points
        }

        void CreateGhosts(int howMany)
        {
            noGhosts = howMany;
            ghostX = new int[noGhosts];
            ghostY = new int[noGhosts];
            ghostmoveX = new int[noGhosts];
            ghostmoveY = new int[noGhosts];
            ghostLastDirection = new int[noGhosts];
            gtChange = new bool[noGhosts];
            changeDirection = new bool[noGhosts];
            forceGhosts = new bool[noGhosts];

            for (int i = 0; i < noGhosts; i++)
            {
                ghostX[i] = 400;
                ghostY[i] = 300;
                ghostmoveX[i] = 0;
                ghostmoveY[i] = 0;
                ghostLastDirection[i] = -1;
                gtChange[i] = false;
                changeDirection[i] = false;
                forceGhosts[i] = false;
            }

        }

        void CreateOutOfBounds()
        {
            noRects = 55;
            rects = new Rectangle[noRects];
            rects[0] = new Rectangle(138, 15, 25, 207);
            rects[1] = new Rectangle(200, 66, 78, 42);
            rects[2] = new Rectangle(320, 66, 102, 42);
            rects[3] = new Rectangle(536, 66, 102, 42);
            rects[4] = new Rectangle(680, 66, 78, 42);
            rects[5] = new Rectangle(200, 150, 78, 21);
            rects[6] = new Rectangle(392, 150, 174, 21);
            rects[7] = new Rectangle(680, 150, 78, 21);
            rects[8] = new Rectangle(138, 0, 687, 25);
            rects[9] = new Rectangle(800, 15, 25, 207);
            rects[10] = new Rectangle(464, 15, 30, 93);
            rects[11] = new Rectangle(138, 213, 140, 21);
            rects[12] = new Rectangle(320, 213, 102, 21);
            rects[13] = new Rectangle(536, 213, 102, 21);
            rects[14] = new Rectangle(680, 213, 145, 21);
            rects[15] = new Rectangle(138, 414, 25, 249);
            rects[16] = new Rectangle(800, 414, 25, 249);
            rects[17] = new Rectangle(320, 150, 30, 147);
            rects[18] = new Rectangle(608, 150, 30, 147);
            rects[19] = new Rectangle(464, 150, 30, 84);
            rects[20] = new Rectangle(608, 339, 30, 84);
            rects[21] = new Rectangle(320, 339, 30, 84);
            rects[22] = new Rectangle(138, 654, 687, 25);
            rects[23] = new Rectangle(392, 402, 174, 21);
            rects[24] = new Rectangle(200, 465, 78, 21);
            rects[25] = new Rectangle(680, 465, 78, 21);
            rects[26] = new Rectangle(138, 276, 140, 21);
            rects[27] = new Rectangle(680, 276, 145, 21);
            rects[28] = new Rectangle(138, 339, 140, 21);
            rects[29] = new Rectangle(680, 339, 145, 21);
            rects[30] = new Rectangle(138, 402, 140, 21);
            rects[31] = new Rectangle(680, 402, 145, 21);
            rects[32] = new Rectangle(248, 339, 30, 84);
            rects[33] = new Rectangle(680, 339, 30, 84);
            rects[34] = new Rectangle(248, 213, 30, 84);
            rects[35] = new Rectangle(680, 213, 30, 84);
            rects[36] = new Rectangle(320, 465, 102, 21);
            rects[37] = new Rectangle(536, 465, 102, 21);
            rects[38] = new Rectangle(536, 591, 222, 21);
            rects[39] = new Rectangle(200, 591, 222, 21);
            rects[40] = new Rectangle(392, 528, 174, 21);
            rects[41] = new Rectangle(150, 528, 56, 21);
            rects[42] = new Rectangle(752, 528, 56, 21);
            rects[43] = new Rectangle(248, 469, 30, 80);
            rects[44] = new Rectangle(680, 469, 30, 80);
            rects[45] = new Rectangle(464, 406, 30, 80);
            rects[46] = new Rectangle(464, 532, 30, 80);
            rects[47] = new Rectangle(320, 528, 30, 80);
            rects[48] = new Rectangle(608, 528, 30, 80);
            rects[49] = new Rectangle(392, 276, 9, 84);
            rects[50] = new Rectangle(557, 276, 9, 84);
            rects[51] = new Rectangle(392, 351, 170, 9);
            rects[52] = new Rectangle(392, 276, 66, 9);
            rects[53] = new Rectangle(500, 276, 66, 9);
            rects[54] = new Rectangle(458, 276, 42, 9); // this is the door!

            tunnel1 = new Rectangle(200, 297, 10, 42);
            tunnel2 = new Rectangle(750, 297, 10, 42);
            closedoor = new Rectangle(458, 276, 42, 9);

        }

        void CreatePoints()
        {
            noPts = 783; // 29 * 27 [grid of points]
            pts = new Rectangle[noPts];

            int counter = -1;
            int x = 177;
            int y = 42;
            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    counter++;

                    Rectangle tempRect = new Rectangle(x + (j * 24), y + (i * 21), 8, 7);

                    bool flag = false;

                    for (int t = 0; t < noRects; t++)
                    {
                        if (tempRect.Intersects(rects[t]))
                            flag = true;
                    }

                    if (!flag)
                        pts[counter] = tempRect;
                    else
                        counter--;
                }
            }

            // kill some values from the points array:
            pts[101] = new Rectangle(1, 1, 1, 1);
            pts[102] = new Rectangle(1, 1, 1, 1);
            pts[105] = new Rectangle(1, 1, 1, 1);
            pts[106] = new Rectangle(1, 1, 1, 1);
            pts[108] = new Rectangle(1, 1, 1, 1);
            pts[109] = new Rectangle(1, 1, 1, 1);
            pts[110] = new Rectangle(1, 1, 1, 1);
            pts[112] = new Rectangle(1, 1, 1, 1);
            pts[113] = new Rectangle(1, 1, 1, 1);
            pts[114] = new Rectangle(1, 1, 1, 1);
            pts[115] = new Rectangle(1, 1, 1, 1);
            pts[116] = new Rectangle(1, 1, 1, 1);
            pts[117] = new Rectangle(1, 1, 1, 1);
            pts[118] = new Rectangle(1, 1, 1, 1);
            pts[119] = new Rectangle(1, 1, 1, 1);
            pts[120] = new Rectangle(1, 1, 1, 1);
            pts[121] = new Rectangle(1, 1, 1, 1);
            pts[123] = new Rectangle(1, 1, 1, 1);
            pts[124] = new Rectangle(1, 1, 1, 1);
            pts[125] = new Rectangle(1, 1, 1, 1);
            pts[126] = new Rectangle(1, 1, 1, 1);
            pts[128] = new Rectangle(1, 1, 1, 1);
            pts[129] = new Rectangle(1, 1, 1, 1);
            pts[132] = new Rectangle(1, 1, 1, 1);
            pts[133] = new Rectangle(1, 1, 1, 1);
            pts[134] = new Rectangle(1, 1, 1, 1);
            pts[135] = new Rectangle(1, 1, 1, 1);
            pts[136] = new Rectangle(1, 1, 1, 1);
            pts[137] = new Rectangle(1, 1, 1, 1);
            pts[138] = new Rectangle(1, 1, 1, 1);
            pts[139] = new Rectangle(1, 1, 1, 1);
            pts[141] = new Rectangle(1, 1, 1, 1);
            pts[142] = new Rectangle(1, 1, 1, 1);
            pts[143] = new Rectangle(1, 1, 1, 1);
            pts[144] = new Rectangle(1, 1, 1, 1);
            pts[145] = new Rectangle(1, 1, 1, 1);
            pts[147] = new Rectangle(1, 1, 1, 1);
            pts[148] = new Rectangle(1, 1, 1, 1);
            pts[149] = new Rectangle(1, 1, 1, 1);
            pts[150] = new Rectangle(1, 1, 1, 1);
            pts[151] = new Rectangle(1, 1, 1, 1);
            pts[152] = new Rectangle(1, 1, 1, 1);
            pts[153] = new Rectangle(1, 1, 1, 1);
            pts[154] = new Rectangle(1, 1, 1, 1);
            pts[155] = new Rectangle(1, 1, 1, 1);
            pts[156] = new Rectangle(1, 1, 1, 1);
            pts[157] = new Rectangle(1, 1, 1, 1);
            pts[158] = new Rectangle(1, 1, 1, 1);
            pts[160] = new Rectangle(1, 1, 1, 1);
            pts[161] = new Rectangle(1, 1, 1, 1);
            pts[162] = new Rectangle(1, 1, 1, 1);
            pts[163] = new Rectangle(1, 1, 1, 1);
            pts[164] = new Rectangle(1, 1, 1, 1);
            pts[165] = new Rectangle(1, 1, 1, 1);
            pts[167] = new Rectangle(1, 1, 1, 1);
            pts[168] = new Rectangle(1, 1, 1, 1);
            pts[169] = new Rectangle(1, 1, 1, 1);
            pts[170] = new Rectangle(1, 1, 1, 1);
            pts[171] = new Rectangle(1, 1, 1, 1);
            pts[172] = new Rectangle(1, 1, 1, 1);
            pts[173] = new Rectangle(1, 1, 1, 1);
            pts[174] = new Rectangle(1, 1, 1, 1);
            pts[177] = new Rectangle(1, 1, 1, 1);
            pts[178] = new Rectangle(1, 1, 1, 1);
            pts[180] = new Rectangle(1, 1, 1, 1);
            pts[181] = new Rectangle(1, 1, 1, 1);
            pts[182] = new Rectangle(1, 1, 1, 1);
            pts[184] = new Rectangle(1, 1, 1, 1);
            pts[185] = new Rectangle(1, 1, 1, 1);
            pts[186] = new Rectangle(1, 1, 1, 1);
            pts[187] = new Rectangle(1, 1, 1, 1);
            pts[188] = new Rectangle(1, 1, 1, 1);
            pts[189] = new Rectangle(1, 1, 1, 1);
            pts[190] = new Rectangle(1, 1, 1, 1);
            pts[191] = new Rectangle(1, 1, 1, 1);
            pts[192] = new Rectangle(1, 1, 1, 1);
            pts[193] = new Rectangle(1, 1, 1, 1);
            pts[195] = new Rectangle(1, 1, 1, 1);
            pts[196] = new Rectangle(1, 1, 1, 1);
            pts[197] = new Rectangle(1, 1, 1, 1);
            pts[198] = new Rectangle(1, 1, 1, 1);
            pts[200] = new Rectangle(1, 1, 1, 1);
            pts[201] = new Rectangle(1, 1, 1, 1);
            pts[204] = new Rectangle(1, 1, 1, 1);
            pts[205] = new Rectangle(1, 1, 1, 1);
            pts[253] = new Rectangle(1, 1, 1, 1);
            pts[254] = new Rectangle(1, 1, 1, 1);

            possiblePoints = 244;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            timer = new HUD.Timer(this);
            timer.timer = 0;
            timer.pauseTime = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scoreText = Content.Load<SpriteFont>("font");
            spriteClosed = Content.Load<Texture2D>("pacman2");
            spriteUp = Content.Load<Texture2D>("pacmanUp");
            spriteDown = Content.Load<Texture2D>("pacmanDown");
            spriteLeft = Content.Load<Texture2D>("pacmanLeft");
            spriteRight = Content.Load<Texture2D>("pacmanRight");
            ghost = Content.Load<Texture2D>("ghost");
            //ghostEye = Content.Load<Texture2D>("eye");
            //ghostPupil = Content.Load<Texture2D>("pupil");
            gameover = Content.Load<Texture2D>("gameover");
            background = Content.Load<Texture2D>("level");
            rect = Content.Load<Texture2D>("rect");
            door = Content.Load<Texture2D>("door");
            smPoint = Content.Load<Texture2D>("point");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (randomSeed > 2147483) // if the Randomseed value is approaching it's maximum, reset it to
                randomSeed = 0;       // prevent crashing! :)

            if (firstCheck == false)
            {
                firstCheck = true;
                firstRun = gameTime.TotalGameTime.TotalSeconds;
            }

            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();

            foreach (Keys keys in currentKeys)
            {
                if (keys == Keys.Escape)
                    this.Exit();
                if (keys == Keys.F2)
                {
                    timer.timer = 0;
                    timer.pauseTime = false;
                    StartGame(0, 1, 3, true);
                }
            }

            shutDoor = false;

            if (forced == true)
            {
                shutDoor = true;

                for (int i = 0; i < noGhosts; i++)
                {
                    if (forceGhosts[i] == true)
                        shutDoor = false;
                }

                if (shutDoor == true)
                {
                    // all the ghosts are out of the middle, lets
                    // shut the door now.

                    rects[54] = new Rectangle(458, 276, 42, 9); // shuts the door

                }

            }

            if (!endGame)
            {
                // Otherwise proceed to interpret user input:

                if (forced == false)
                {
                    if (gameTime.TotalGameTime.TotalSeconds > firstRun + 2)
                    {

                        if (storedSecs == 0)
                            storedSecs = gameTime.TotalGameTime.TotalSeconds;

                        if (gameTime.TotalGameTime.TotalSeconds < storedSecs + 2)
                        {
                            if (gameTime.TotalGameTime.Milliseconds % 250 == 0)
                            {
                                if (flashDoor)
                                {
                                    flashDoor = false;
                                }
                                else
                                {
                                    flashDoor = true;
                                }
                            }
                        }
                        else
                        {
                            rects[54] = new Rectangle(1, 1, 1, 1); // the door is open!

                            // need to FORCE ghosts to exit the central section,
                            // otherwise it can take ages for them to leave...
                            for (int i = 0; i < noGhosts; i++)
                            {
                                forceGhosts[i] = true;
                            }
                            forced = true;
                        }
                    }
                }

                AnimateGhosts();
                MoveGhosts();

                int startsecs = 2000;
                int[] moveSecs = new int[noGhosts];

                for (int i = 0; i < noGhosts; i++)
                {
                    moveSecs[i] = startsecs + i * 100;
                }

                for (int i = 0; i < noGhosts; i++)
                {
                    if (gameTime.TotalGameTime.Milliseconds % moveSecs[i] == 0)
                    {
                        gtChange[i] = true;
                    }
                }

                // only check for keys pressed while
                // the game is actually running

                if (gameRunning)
                {

                    foreach (Keys keys in currentKeys)
                    {
                        if (keys == Keys.Up)
                        {
                            spriteDirection = 0;
                            moveY = -5;
                            moveX = 0;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteUp, true))
                                spriteY = spriteY + moveY;

                        }
                        if (keys == Keys.Down)
                        {
                            spriteDirection = 1;
                            moveY = 5;
                            moveX = 0;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteDown, true))
                                spriteY = spriteY + moveY;

                        }
                        if (keys == Keys.Left)
                        {
                            spriteDirection = 2;
                            moveY = 0;
                            moveX = -5;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteLeft, true))
                            {
                                Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteLeft.Width, spriteLeft.Height);
                                if (tempRect.Intersects(tunnel1))
                                    spriteX = tunnel2.Left - tempRect.Width - 1;
                                else if (tempRect.Intersects(tunnel2))
                                    spriteX = tunnel1.Left + tunnel1.Width + 1;
                                else
                                    spriteX = spriteX + moveX;
                            }
                        }
                        if (keys == Keys.Right)
                        {
                            spriteDirection = 3;
                            moveY = 0;
                            moveX = 5;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteRight, true))
                            {
                                Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteRight.Width, spriteRight.Height);
                                if (tempRect.Intersects(tunnel1))
                                    spriteX = tunnel2.Left - tempRect.Width - 1;
                                else if (tempRect.Intersects(tunnel2))
                                    spriteX = tunnel1.Left + tunnel1.Width + 1;
                                else
                                    spriteX = spriteX + moveX;
                            }
                        }
                    }
                }

                if (gameTime.TotalGameTime.Milliseconds % 500 == 0)
                {
                    if (mouthOpen)
                    {
                        mouthOpen = false;
                    }
                    else
                    {
                        mouthOpen = true;
                    }
                }

                if (spriteX < 0)
                    spriteX = 0;
                if (spriteY < 0)
                    spriteY = 0;
                if (spriteX + moveX > graphics.GraphicsDevice.Viewport.Width)
                    spriteX = graphics.GraphicsDevice.Viewport.Width - moveX;
                if (spriteY + moveY > graphics.GraphicsDevice.Viewport.Height)
                    spriteY = graphics.GraphicsDevice.Viewport.Height - moveY;

                // do bounds checking
            }

            base.Update(gameTime);
        }

        void StartGame(int newScore, int newLevel, int newLives, bool killPoints)
        {
            shutDoor = true;
            flashDoor = false;
            gameRunning = false;
            storedSecs = 0;
            Score = newScore;
            Level = newLevel;
            Lives = newLives;
            spriteX = 463;
            spriteY = 491;
            spriteDirection = 3;
            moveY = 0;
            moveX = 0;
            forced = false;
            endGame = false;
            firstCheck = false;
            CreateGhosts(noGhosts);
            CreateOutOfBounds();
            if (killPoints)
                CreatePoints();
            gameRunning = true;
        }

        void MoveGhosts()
        {
            // Now we need to firstly move each ghost so that the X co-ordinate
            // is in line with the door + 1px.

            int doorLeft = 458;
            int doorWidth = 42;
            int doorTop = 276;

            for (int i = 0; i < noGhosts; i++)
            {
                if (forceGhosts[i] == true)
                {
                    if ((ghostX[i] > doorLeft) && (ghostX[i] + ghost.Width < doorLeft + doorWidth))
                    {
                        // the ghost is in line with the door, what next?
                        // = move the ghost out of the cage.
                        ghostmoveX[i] = 0;
                        ghostmoveY[i] = -5;

                        // now we need to re-enable collision, so the ghost collides
                        // with the wall outside the cage, causing the ghost to move randomly
                        // around the world.
                    }
                    else if (ghostX[i] > doorLeft)
                    {
                        ghostmoveX[i] = -5;
                        ghostmoveY[i] = 0;
                    }
                    else if (ghostX[i] < doorLeft)
                    {
                        ghostmoveX[i] = 5;
                        ghostmoveY[i] = 0;
                    }

                    ghostX[i] = ghostX[i] + ghostmoveX[i];
                    ghostY[i] = ghostY[i] + ghostmoveY[i];

                    if (ghostY[i] + ghost.Height < doorTop)
                    {
                        ghostLastDirection[i] = 0;
                        forceGhosts[i] = false;
                    }
                }
            }

        }

        void AnimateGhosts()
        {
            // need to select random direction,
            // move in that direction until collision
            // then select a new random direction that isn't the last direction
            // then move in that direction until collision

            for (int i = 0; i < noGhosts; i++)
            {
                if (forceGhosts[i] == false)
                {
                    if (changeDirection[i])
                    {
                        changeDirection[i] = false; // makes sure we can still move

                        randomSeed++;

                        Random rnd = new Random(randomSeed);
                        // 0 up 1 down 2 left 3 right

                        int randomDirection = rnd.Next(0, 4);

                        if ((ghostLastDirection[i] == 0) || (ghostLastDirection[i] == 1))
                            randomDirection = rnd.Next(2, 4);
                        else
                            randomDirection = rnd.Next(0, 2);

                        if (randomDirection == 0)
                        {
                            ghostmoveY[i] = -5;
                            ghostmoveX[i] = 0;
                        }
                        if (randomDirection == 1)
                        {
                            ghostmoveY[i] = 5;
                            ghostmoveX[i] = 0;
                        }
                        if (randomDirection == 2)
                        {
                            ghostmoveY[i] = 0;
                            ghostmoveX[i] = -5;
                        }
                        if (randomDirection == 3)
                        {
                            ghostmoveY[i] = 0;
                            ghostmoveX[i] = 5;
                        }

                        ghostLastDirection[i] = randomDirection;
                    }

                    if (CheckBounds(ghostX[i], ghostY[i], ghostmoveX[i], ghostmoveY[i], ghost, false))
                    {
                        Rectangle tempRect = new Rectangle(ghostX[i], ghostY[i], ghost.Width, ghost.Height);
                        if (tempRect.Intersects(tunnel1))
                            ghostX[i] = tunnel2.Left - tempRect.Width - 1;
                        else if (tempRect.Intersects(tunnel2))
                            ghostX[i] = tunnel1.Left + tunnel1.Width + 1;
                        else
                            ghostX[i] = ghostX[i] + ghostmoveX[i];

                        ghostY[i] = ghostY[i] + ghostmoveY[i];
                    }
                    else // The ghost just collided with something - mark it for a direction change next time!
                    {
                        changeDirection[i] = true;
                    }

                    if (gtChange[i])
                    {
                        gtChange[i] = false;
                        randomSeed++;
                        Random rndShallWe = new Random(randomSeed);
                        if (rndShallWe.Next(0, 100) > 50)
                        {
                            changeDirection[i] = true;
                        }
                    }
                }
            }
        }

        bool CheckBounds(int CurrentX, int CurrentY, int AddX, int AddY, Texture2D character, bool isSprite)
        {
            /* need to check here to see if our character rectangle falls within
             * any of our array-ed rectangles! if it does, return false so the
             * character is unable to move.
             */

            // Also, if the character isn't sprite, then check if we're colliding with the sprite.

            Rectangle tempRect = new Rectangle(CurrentX + AddX, CurrentY + AddY, character.Width, character.Height);

            bool tempReturn = true;

            if (isSprite)
            {
                for (int i = 0; i < noPts; i++)
                {
                    if (tempRect.Intersects(pts[i]))
                    {
                        if (gameRunning)
                        {
                            pts[i] = new Rectangle(1, 1, 1, 1);
                            Score += (Level * 10);
                            possiblePoints--;
                            if (possiblePoints == 0)  // Level complete, we need to advance!
                            {
                                noGhosts++;
                                Level++;
                                possiblePoints = 244;

                                StartGame(Score, Level, Lives, true); // Advance to next level.
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < noRects; i++)
            {
                if (tempRect.Intersects(rects[i]))
                    tempReturn = false;
            }

            if (!isSprite)
            {
                if (tempRect.Intersects(new Rectangle(spriteX, spriteY, spriteUp.Width, spriteUp.Height)))
                {
                    tempReturn = false;
                    if (!dying)
                    {
                        Lives--;
                        dying = true;
                        StartGame(Score, Level, Lives, false);
                        dying = false;
                        if (Lives == 0)
                            endGame = true;
                    }
                }

            }

            return tempReturn;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            timer.DisplayTime(gameTime, scoreText, Window.ClientBounds.Width/2-50, Window.ClientBounds.Height-30, Color.White);

            if (flashDoor || forced)
            {
                if (!shutDoor)
                    spriteBatch.Draw(door, closedoor, Color.White);
            }

            for (int i = 0; i < noPts; i++)
            {
                Rectangle tester = new Rectangle(1, 1, 1, 1); // this checks to see if the rectangle should be drawn.
                if (pts[i] != tester)
                    spriteBatch.Draw(smPoint, pts[i], Color.White); // this is the points.
            }

            Rectangle spriteRectangle = new Rectangle(spriteX, spriteY, spriteClosed.Width, spriteClosed.Height);

            if (mouthOpen)
            {
                if (spriteDirection == 0)
                    spriteBatch.Draw(spriteUp, spriteRectangle, Color.White);
                else if (spriteDirection == 1)
                    spriteBatch.Draw(spriteDown, spriteRectangle, Color.White);
                else if (spriteDirection == 2)
                    spriteBatch.Draw(spriteLeft, spriteRectangle, Color.White);
                else if (spriteDirection == 3)
                    spriteBatch.Draw(spriteRight, spriteRectangle, Color.White);
            }
            else
                spriteBatch.Draw(spriteClosed, spriteRectangle, Color.White);

            // need to draw ghosts in different colours
            //
            // the maximum number of ghosts is 5, so we need
            // 5 different colours
            // RGB
            // 87, 171, 255 : light blue
            // 255, 171, 255: light red
            // 255, 131, 3  : light orange
            // 255, 255, 255: light white
            // 25, 255, 0   : light green

            Color[] ghostColours = new Color[5];
            ghostColours[0] = new Color(87, 171, 255);
            ghostColours[1] = new Color(255, 171, 255);
            ghostColours[2] = new Color(255, 131, 3);
            ghostColours[3] = new Color(255, 255, 255);
            ghostColours[4] = new Color(25, 255, 0);

            int tempCounter = -1;

            for (int i = 0; i < noGhosts; i++)
            {
                tempCounter++;

                spriteBatch.Draw(ghost, new Rectangle(ghostX[i], ghostY[i], ghost.Width, ghost.Height), ghostColours[tempCounter]);
                //spriteBatch.Draw(ghostEye, new Rectangle(ghostX[i] + 5, ghostY[i] + 4, ghostEye.Width, ghostEye.Height), Color.White);
                //spriteBatch.Draw(ghostEye, new Rectangle(ghostX[i] + 17, ghostY[i] + 4, ghostEye.Width, ghostEye.Height), Color.White);

                if ((ghostLastDirection[i] == 0) || (ghostLastDirection[i] == -1)) // up (or starting)
                {
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 7, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 19, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 1) // down
                {
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 7, ghostY[i] + 11, ghostPupil.Width, ghostPupil.Height), Color.White);
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 19, ghostY[i] + 11, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 2) // left
                {
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 4, ghostY[i] + 7, ghostPupil.Width, ghostPupil.Height), Color.White);
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 16, ghostY[i] + 7, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 3) // right
                {
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 10, ghostY[i] + 7, ghostPupil.Width, ghostPupil.Height), Color.White);
                    //spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 22, ghostY[i] + 7, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (tempCounter == 4)
                    tempCounter = -1;
            }

            for (int i = 0; i < noRects; i++)
            {
                //spriteBatch.Draw(rect, rects[i], Color.White); // this line shows the walls!
            }

            if (endGame)
            {
                spriteBatch.Draw(gameover, new Rectangle(294, 225, gameover.Width, gameover.Height), Color.White);
                noGhosts = totalGhosts;
            }

            spriteBatch.Draw(rect, tunnel1, Color.White);
            spriteBatch.Draw(rect, tunnel2, Color.White);

            string scoreString = Score.ToString("0\n0\n0\n0\n0");
            spriteBatch.DrawString(scoreText, scoreString, new Vector2(60.0f, 110.0f), Color.White);

            for (int i = 0; i < (Lives - 1); i++)
            {
                float tempTop = (float)spriteRight.Height * (float)i * 1.3f;
                spriteBatch.Draw(spriteRight, new Rectangle(53, 570 + (int)tempTop, spriteRight.Width, spriteRight.Height), Color.White);
            }
            
                
//#if DEBUG
//            spriteBatch.DrawString(scoreText, debug, new Vector2(1.0f, 1.0f), Color.White);
//#endif

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}