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

namespace VaibhavKabeta
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
         
        public static Texture2D enemySheet;
        public static Texture2D characterSheet;
        public static Texture2D fireSheet;
        
      
        

        //New
        IScreenNode sceneInGame;
        IScreenNode sceneSplash;
        IScreenNode sceneMenu;

        public static ContentManager contentMgr { get; set; }
        public static GameTime gt;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
           
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f/60.0f);

           
            
            Content.RootDirectory = "Content";
        }

        


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
  
            base.Initialize();
        }




       
        protected override void LoadContent()
        {
            contentMgr = Content;

            // Create a new SpriteBatch, which can be used to draw textures.
            Game1.spriteBatch = new SpriteBatch(GraphicsDevice);
           
            Game1.enemySheet = Content.Load<Texture2D>("enemy");
            Game1.characterSheet = Content.Load<Texture2D>("fuddu4");
            Game1.fireSheet = Content.Load<Texture2D>("fires");
            
           
            sceneInGame = new Screens.InGameScreen();
            sceneSplash = new Screens.SplashScreen();
            sceneMenu = new Screens.MenuScreen();

            sceneSplash.LoadContent();
            sceneMenu.LoadContent();
            sceneInGame.LoadContent();
        }




        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

           
           KeyboardState keyState = Keyboard.GetState();
        

                if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            Game1.gt = gameTime;
            switch(GameStateManager.getCurrentState())
            {
                case GameStates._splashScreen:
                    sceneSplash.Update(gt);
                    break;
                case GameStates._inGameScreen:
                    sceneInGame.Update(gt);
                    break;
                case GameStates._menuScreen:
                    sceneMenu.Update(Game1.gt);
                    break;
                default:
                    break;
            }

            InputSystem.KeyboardInterface.getKeyboardInstance().KB_InputUpdate();
           //Console.WriteLine("+currentState: "+GameStateManager.getCurrentState().ToString()+gt.TotalGameTime.TotalSeconds.ToString()+" \n");
            base.Update(gameTime);
        }


        Vector2 vect = new Vector2(600, 400);
          
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);
            spriteBatch.Begin();
     
            
            switch(GameStateManager.getCurrentState())
            {
                case GameStates._splashScreen:
                    sceneSplash.Draw(Game1.spriteBatch);
                    break;
                case GameStates._menuScreen:
                    sceneMenu.Draw(Game1.spriteBatch);
                    break;
                case GameStates._inGameScreen:
                    sceneInGame.Draw(Game1.spriteBatch);
                    break;

                default:
                    break;
            }

           //cEngine.DrawCharacter(spriteBatch);
           //enemyEngine.DrawEnemies(spriteBatch);





            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
