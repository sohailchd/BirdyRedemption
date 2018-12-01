using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VaibhavKabeta.Screens
{
    public class InGameScreen : IScreenNode
    {
        CharacterEngine cEngine;
        private FireSystem fireSystem;
        BirdEnemyEngine enemyEngine;
        CollisionManager collisionManager;




       public InGameScreen() 
       {
           fireSystem = new FireSystem(Game1.fireSheet);
           cEngine = new CharacterEngine(Game1.characterSheet, new Vector2(200, 200), 200, 200, Game1.fireSheet, fireSystem);
           enemyEngine = new BirdEnemyEngine(Game1.enemySheet);
           enemyEngine.enemyEngineInit();
           collisionManager = new CollisionManager(cEngine, fireSystem, enemyEngine);

       }
       ~InGameScreen() { }



       public void LoadContent()
       {
       }


        public void Draw(SpriteBatch batch) 
        {
            HUDHandler.showTextAt("WASD Move, Arrows fire)", new Vector2(100, 80), Color.Black);

            HUDHandler.showTextAt("Press Esc to exit (test)",new Vector2(100,100),Color.Black);

            cEngine.DrawCharacter(Game1.spriteBatch);
            enemyEngine.DrawEnemies(Game1.spriteBatch);
        }
        public void Update(GameTime gameTime) 
        {
            KeyboardState keyState = Keyboard.GetState();
            cEngine.characterInputUpdate(gameTime);


            //enemies
            enemyEngine.enemyUpdate(cEngine.position, gameTime);
            collisionManager._collisionUpdate();

        
                

            //
            collisionManager._collisionUpdate();
        }
        public void HandleInput() 
        {
        }

    }
}
