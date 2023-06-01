using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace The_Bomb_4
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bomb;
        Texture2D giphy;
        SoundEffect explode;
        SpriteFont file;
        Rectangle bombR;
        Rectangle wire1, wire2;
        MouseState mouse;
        double seconds, startTime = 15;
        bool explosion = true, timer = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            bombR = new Rectangle(50, 50, 700, 400);
            wire1 = new Rectangle(492, 156, 757 - 492, 211 - 156);
            wire2 = new Rectangle(660, 209, 755 - 660, 255 - 209);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bomb = Content.Load<Texture2D>("bomb");
            explode = Content.Load<SoundEffect>("explosion");
            file = Content.Load<SpriteFont>("File");
            giphy = Content.Load<Texture2D>("giphy");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (timer)
                seconds = Math.Round((double)gameTime.TotalGameTime.TotalSeconds, 2);

            if (explosion)
            {
                if (seconds >= 15)
                {
                    explode.Play();
                    explosion = false;
                }
            }
            if (seconds >= 15 + (double)explode.Duration.TotalSeconds)
                Environment.Exit(0);
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (wire1.Contains(mouse.X, mouse.Y))
                    timer = false;
                if (wire2.Contains(mouse.X, mouse.Y))
                    timer = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bomb, bombR, Color.White);
            if (seconds < 15)
                _spriteBatch.DrawString(file, (startTime - seconds).ToString("00.00"), new Vector2(253, 205), Color.Black);
            else
                _spriteBatch.DrawString(file, "00.00", new Vector2(253, 205), Color.Black);

            if (!explosion)
            { 
                    _spriteBatch.Draw(giphy, new Rectangle(0, 0, 800, 500), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}