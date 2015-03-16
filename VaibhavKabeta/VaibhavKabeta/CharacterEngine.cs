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
    class CharacterEngine
    {
        const float charScale = 0.6f;
        public Vector2 position;
        int frames = 3;
        int currentFrame = 0;
        Texture2D characterTexture;
        int steps = 8;
        int frameW = 200;
        int frameH = 200;
        public  float runningSpeed = 6.0f;
        const float FIREDELAY = 0.20f;
        public int state = 0;
        float fireDelay;
        float hitDelay = 1.0f;
        
        //public  enum characterState  { stand , rightWalk , leftWalk , upWalk , downWalk };

        GameTime gt;
        bool fireState = false;
        FireSystem fireSystem;
      

        public int _collisionWithBird = 0;
        
     
        public CharacterEngine(Texture2D tex , Vector2 pos , int wd , int ht , Texture2D fireTexture , FireSystem fsys) {
            characterTexture = tex;
            position = pos;
            frameH = ht;
            frameW = wd;
            fireSystem = fsys;

        }

        public void DrawCharacter(SpriteBatch spriteBatch) {
            //float rot =MathHelper.Pi/4 / 20.0f;
            Rectangle dest = new Rectangle((int)position.X , (int)position.Y , frameW,frameH);
            Rectangle source ;
            --steps;
            if(steps<0){
                steps = 5;
                FrameUpdate();
            }
            if(state == 5){
                source = new Rectangle( 0,currentFrame*2 ,  frameW, frameH);
            }else
            source = new Rectangle(currentFrame * frameW, state * frameH, frameW, frameH);
            Game1.spriteBatch.Draw(characterTexture ,position,source,Color.White,0.0f,Vector2.Zero ,charScale ,SpriteEffects.None,0);             
            fireSystem.drawFireSystem(spriteBatch);
            
        }

        
        public void updateCollisionWithBird() {
            float delay = (float)gt.ElapsedGameTime.TotalSeconds;
            hitDelay -= delay;
           if(_collisionWithBird==1){
                  if(hitDelay <=0){
                   state = 5;
                   _collisionWithBird = 0;
                   hitDelay = 1.0f;
                  }
                  state = 3;
           }     
        }


        public void  FrameUpdate(){
            ++currentFrame;
            if(currentFrame>frames)
                currentFrame = 0;

            }



        public void characterInputUpdate(GameTime gameTime)
        {
            gt = gameTime;
            KeyboardState keyboardState = Keyboard.GetState();
           
            state = 5;
          
            fireState = false;
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= runningSpeed;
                state = 1;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += runningSpeed;
                state = 0;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= runningSpeed;
                state = 2;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += runningSpeed;
                state = 2;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                //cEngine.position.Y += cEngine.runningSpeed;
                state = 3;
            }

            
            //bullet fires
            float delay = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fireDelay -= delay;

            
            fireSystem.updateFireSystem();
            if(keyboardState.IsKeyDown(Keys.Left)){
              
                if(fireDelay<=0){
                state = 1;
                fireState = true;
                if(fireState){
                    fireSystem.addFire(position,0);
                    //set the position and type and wd-ht
                }
                fireDelay = FIREDELAY;
                }
                  
            } //end A
            if (keyboardState.IsKeyDown(Keys.Right))
            {

                if (fireDelay <= 0)
                {
                    state = 1;
                    fireState = true;
                    if (fireState)
                    {
                        fireSystem.addFire(position, 2);
                    }
                    fireDelay = FIREDELAY;
                }

            }//end D
            if (keyboardState.IsKeyDown(Keys.Up))
            {

                if (fireDelay <= 0)
                {
                    state = 2;
                    fireState = true;
                    if (fireState)
                    {
                        fireSystem.addFire(position, 3);
                    }
                    fireDelay = FIREDELAY;
                }

            }//end W
            if (keyboardState.IsKeyDown(Keys.Down))
            {

                if (fireDelay <= 0)
                {
                    state = 2;
                    fireState = true;
                    if (fireState)
                    {
                        fireSystem.addFire(position, 1);
                    }
                    fireDelay = FIREDELAY;
                }

            }//end S

            updateCollisionWithBird();
        }

        }

    
}
