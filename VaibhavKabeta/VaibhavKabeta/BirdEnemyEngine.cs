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
    class BirdEnemyEngine
    {

        int enemyCount = 10;
        public List<Enemy> enemyList;
        Texture2D texHolder;
        Random rnd;
        // RED BIRD
        Rectangle sourceRect = new Rectangle(0,0,125,100);

   


       public BirdEnemyEngine( Texture2D tex ) {
            enemyList = new List<Enemy>();
            texHolder = tex;       
        }


        public void enemyEngineInit() {
            //add some enimies enimies
            rnd = new Random();
            for (int i = 1; i <= enemyCount;i++ )
            {
                enemyList.Add(new Enemy(new Vector2(rnd.Next(30,1000),rnd.Next(20,800)),i));
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch){
           
         foreach(Enemy e in enemyList){
              if(e.flip_horizontally==0){
              spriteBatch.Draw(texHolder,e.enemyPosition,sourceRect,Color.White,0.0f,Vector2.Zero,0.3f,SpriteEffects.None,0);
              }
              else spriteBatch.Draw(texHolder, e.enemyPosition, sourceRect, Color.White, 0.0f, Vector2.Zero, 0.3f, SpriteEffects.FlipHorizontally, 0);
         }
        }

        void loopCreator()
        {
            if (enemyList.Count == 0)
            {
                enemyCount += 10;
                rnd = new Random();
                for (int i = 1; i <= enemyCount; i++)
                {
                    enemyList.Add(new Enemy(new Vector2(rnd.Next(30, 1400), rnd.Next(20, 800)), i));
                }
            }
        }
        //Engine needs to know the player position
        public void enemyUpdate(Vector2 target , GameTime time){
            foreach(Enemy e in enemyList){
                e.enemyUpdate_native(time,target);
            }
            loopCreator();
            
        }

    }

    


    //=========  ENEMY  ============ //
    class Enemy {

        public Vector2 enemyPosition;
        public int enemyDir;
        public float speed;
        public int enemy_id;
        Random rnd;
        // IF 1 THEN NORMAL ELSE FLIP_HOR
        public int flip_horizontally = 0;

        // ----- STATE BECOMES ZERO/0 IF ENEMY HAS BEEN ATTACKED  AND AGAIN ONE/1 IF COLLIDED WITH THE WALL/S.  ----------- //
        // ----- ENEMY ATTACKS ONLY WHEN THE STATE IS ONE/1 ELSE MOVES TOWARDS THE WALLS -----------------------------//
        int stateOf_attack;


        /** 
         * COLLISION STATE VAR
         **/
        public int _collisionWithBullet = 0;
        public int _colllisionWithTarget = 0;
        public float health = 5.0f;






        void testFlip(Vector2 targetPosition) { 
            /** 
             *   LOOK TOWARDS THE TARGET ONLY WHEN IN ATTACK MODE ELSE DEFAULT FLIP
             *   
             ***/
            
                if (enemyPosition.X >= targetPosition.X+10)
                {
                    flip_horizontally = 1;
                }
                else flip_horizontally = 0;
            
        }






        public Enemy( Vector2 pos , int id) {
            enemy_id = id;
            enemyPosition = pos;
            enemyDir = 1;
            speed = 2.0f;
            stateOf_attack = 0; //ATACK MODE
            rnd = new Random();
        }

        public void enemyUpdate_native(GameTime time , Vector2 targetPosition) {
            AI_native(targetPosition);
            testFlip(targetPosition);
        }




        /*
         *    Ad-hoc BIRD's AI implementation
         */

        public void AI_native(Vector2 targetPosition) { 
            /**
             * -IF THE STATE IS 1 , MOVES TOWARDS THE TARGET ELSE MOVE TOWARDS THE WALL
             * -ONCE COLLIDED WITH THE WALL ATTACK AGAIN
             * -UPON COLLIDING WITH THE TARGET BOUNCE RANDOMLY TOWARDS ANY DIRECTION OF WALL
             * **/
  
            if(stateOf_attack==1){
                checkState(targetPosition);
                //MOVE TOWARDS THE TARGET
                if (enemyPosition.X < targetPosition.X)
                {
                    enemyPosition.X += speed;
                }
                else { enemyPosition.X -= speed; }
                if (enemyPosition.Y < targetPosition.Y)
                {
                    enemyPosition.Y += speed;
                }
                else { enemyPosition.Y -= speed; }
            }

             
            if(stateOf_attack==0){
                //MOVE TOWARDS THE WALL and BOUNCE BACK
                //checkState(targetPosition);
                if(enemyPosition.X<0 || enemyPosition.X>1000){
                  
                    stateOf_attack = 1;
                }
                if(enemyPosition.Y<0 || enemyPosition.Y>600){
                
                    stateOf_attack = 1;
                }

                if(enemy_id%5==0){
                    enemyPosition.X += speed*(-1);
                    enemyPosition.Y += speed;
                }
                if(enemy_id%5 ==1){
                    enemyPosition.X += speed;
                    enemyPosition.Y += speed;
                }
                if(enemy_id%3 ==2 ){
                    enemyPosition.X += speed * (-1);
                    enemyPosition.Y += speed*(-1);
                }
                if (enemy_id % 5 == 3)
                {
                    enemyPosition.X += speed;
                    enemyPosition.Y += speed * (-1);
                }
                if (enemy_id % 5 == 4)
                { enemyPosition.X += speed * (-0.5f); enemyPosition.Y -= speed * 0.5f; }
                else
                { enemyPosition.X += speed * (-0.5f); enemyPosition.Y += speed * 0.5f; }
                
            }

        }


        /*
         * Collision detection without the <::: RECTANGLE INTERSECTIONS :::>
         */
        int targetRange = 4; // 20 pixels X,Y from the origin of the target
        public void checkState(Vector2 targetPosition) {
            if (Math.Abs(enemyPosition.X - targetPosition.X) < targetRange && Math.Abs(enemyPosition.Y - targetPosition.Y) < targetRange+10)
            {
                stateOf_attack = 0;
            }
            else
                stateOf_attack = 1;
        }


        public void _OnHit_health() {
            health -= 1.0f;
        }

    } 
}
