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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using WiimoteLib; 

namespace MultiWiiMoteDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
       GraphicsDeviceManager graphics;
       SpriteBatch spriteBatch;

       SpriteFont font;

       WiimoteCollection mWC;
       string error = "";

       Point3F[] point;
       string[] buttonsPressed;
       string[] IRStatus;
       Vector2[] punten;
       Texture2D[] texture;

       Rectangle Aim1;
       Rectangle viewportRect;

       GameObject cannon;
       const int FlakCount = 10;
       GameObject[] Flak;

       const int maxZeppelins = 4;
       const float maxZeppelinHeight = 0.1f;
       const float minZeppelinHeight = 0.5f;
       const float maxZeppelinVelocity = 5.0f;
       const float minZeppelinVelocity = 1.0f;

       Random random = new Random();
       GameObject[] zeppelins;

        int X = 0;
        int Y = 0;

        int oldX;
        int newx;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            
            base.Initialize();
			
            mWC = new WiimoteCollection();
			
            int index = 1;
            
            try
            {
                mWC.FindAllWiimotes();
            }
            catch (WiimoteNotFoundException ex)
            {
                error =  "Wiimote not found error";
            }
            catch (WiimoteException ex)
            {
                 error =  "Wiimote error";
            }
            catch (Exception ex)
            {
                 error =  "Unknown error";
            }

            foreach (Wiimote wm in mWC)
            {
                wm.Connect();
                wm.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);
                wm.SetLEDs(index);
                index++;
            }

            if (index > 0)
            {
                point = new Point3F[index - 1];
                buttonsPressed = new string[index - 1];
                IRStatus = new string[index - 1];
                punten = new Vector2[index - 1];
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            texture = new Texture2D[4];
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Fonts\\GameFont");
            texture[0] = Content.Load<Texture2D>("Sprites\\AimS1");
            texture[1] = Content.Load<Texture2D>("Sprites\\AimS2");
            texture[2] = Content.Load<Texture2D>("Sprites\\AimS3");
            texture[3] = Content.Load<Texture2D>("Sprites\\AimS4");

            zeppelins = new GameObject[maxZeppelins];
            for (int i = 0; i < maxZeppelins; i++)
            {
                zeppelins[i] = new GameObject(
                        Content.Load<Texture2D>("Sprites\\Zeppelin"));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            foreach (Wiimote wm in mWC)
            {
                wm.Disconnect();
            }
            base.OnExiting(sender, args);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int index = 0;
            foreach (Wiimote wm in mWC)
            {
                WiimoteState state = wm.WiimoteState;
                point[index] = state.AccelState.Values;

                buttonsPressed[index] = "";

                if (state.ButtonState.A)
                {
                    buttonsPressed[index] += "A ";
                }
                if (state.ButtonState.B)
                {
                    buttonsPressed[index] += "B ";
                }
                if (state.ButtonState.Down)
                {
                    buttonsPressed[index] += "Down ";
                }
                if (state.ButtonState.Up)
                {
                    buttonsPressed[index] += "Up ";
                }
                if (state.ButtonState.Left)
                {
                    buttonsPressed[index] += "Left ";
                }
                if (state.ButtonState.Right)
                {
                    buttonsPressed[index] += "Right ";
                }
                if (state.ButtonState.Home)
                {
                    buttonsPressed[index] += "Home ";
                    if (!state.Rumble)
                        wm.SetRumble(true);
                }
                else
                {
                    if (state.Rumble)
                        wm.SetRumble(false);
                }
                if (state.ButtonState.Minus)
                {
                    buttonsPressed[index] += "Minus ";
                }
                if (state.ButtonState.Plus)
                {
                    buttonsPressed[index] += "Plus ";
                }
                if (state.ButtonState.One)
                {
                    buttonsPressed[index] += "One ";
                }
                if (state.ButtonState.Two)
                {
                    buttonsPressed[index] += "Two ";
                }
                  
				oldX = state.IRState.RawMidpoint.X - graphics.GraphicsDevice.Viewport.Width / 2;
				newx = state.IRState.RawMidpoint.X - oldX * 2;
				IRStatus[index] = state.IRState.Midpoint.ToString() + "  " + state.IRState.RawMidpoint.ToString();
				punten[index] = new Vector2(newx, state.IRState.RawMidpoint.Y);

				index++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (!error.Equals("")) 
            {
                spriteBatch.DrawString(font, error, new Vector2(10, 10), Color.Red);
            }
                
            int index = 0;
            int y = 10;           
            
            foreach (Wiimote wm in mWC)
            {
                spriteBatch.DrawString(font, "WiiMote:" + (index+1), new Vector2(10, y), Color.Black);
               
                spriteBatch.DrawString(font, "X: " + point[index].X, new Vector2(10, y +20), Color.Red);
                spriteBatch.DrawString(font, "Y: " + point[index].Y, new Vector2(10, y + 40), Color.Red);
                spriteBatch.DrawString(font, "Z: " + point[index].Z, new Vector2(10, y + 60), Color.Red);
                spriteBatch.DrawString(font, "IR: " + punten[index], new Vector2(10, y + 80), Color.Red);
                
                spriteBatch.DrawString(font, "Buttons Pressed: " + buttonsPressed[index], new Vector2(10, y + 110), Color.Green);
               
                y += 150;
                spriteBatch.Draw(texture[index], punten[index],Color.White);
                index++;
            }

            foreach (GameObject Zeppelin in zeppelins)
            {
                if (Zeppelin.alive)
                {
                    spriteBatch.Draw(Zeppelin.sprite,
                        Zeppelin.position, Color.White);

                }

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
