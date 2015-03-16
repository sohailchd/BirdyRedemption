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

    /**
     *     KEEP THE TRACK OF EACH AND EVERY OBJECT
     *     AS A RECTANGLE AND CHANGE THEIR STATE AS THEY COLLIDE 
     *     WITH EACH OTHER.
     *     
     * **/

    class CollisionManager
    {
        //MAINTAIN THE LIST<> OF THE OBJECTS WHOSE COLLISION MATTERS
        public List<BoundingBox> collisionList;
        FireSystem fireSystem;
        CharacterEngine charEngine;
        BirdEnemyEngine birdEngine;


        public CollisionManager(CharacterEngine ceng , FireSystem fsys , BirdEnemyEngine beng) {
            collisionList = new List<BoundingBox>();
            fireSystem = fsys;
            charEngine = ceng;
            birdEngine = beng;
        }

        public void addObject(int type ,Vector2 pos , int width , int height , object o ) {
            collisionList.Add(new BoundingBox(type,new Rectangle((int)pos.X,(int)pos.Y,width,height),o));
        }

        public void addObject(int type ,int posX, int posY , int width , int height , object o) {
            collisionList.Add(new BoundingBox(type,new Rectangle(posX,posX,width,height),o));
        }

        public void addBullets(Bullets b , Vector2 pos , int w , int h) {
 
        }

        /**
         *    TYPE-0 CHARACTER
         *    TYPE-1 BULLETS
         *    TYPE-2 BIRDS-ENEMY
         *    
         *    COLLISION LOGICs:
         *    A. TYPE-0 WITH TYPE-2
         *       -ANIMATION FOR THE CHARACTER PLAYS AND HEALTH DECREASES
         *    B. TYPE-1 WITH TYPE-3   
         *       -ANIMATION FOR THE BIRDS PLAYS AND HEALTH DECREASES
         *       -CHARACTER POINTS ADDED
         */

        public void _maintainRectsScene() {

            collisionList.Clear();

            //Get the fresh list of the enemy
            foreach(Enemy e in birdEngine.enemyList){
                addObject(2,e.enemyPosition,10,10,e);
            }

            //Add the character
            addObject(0,charEngine.position,30,30,charEngine);


            //Add all the fires
            foreach(Bullets b in fireSystem.fireList){
                addObject(1,b.position,10,10,b);
            }
        }



        public void _Oncollision_bird_fire() { 
            foreach(BoundingBox bullets in collisionList){
                if(bullets.type ==1){
                    foreach(BoundingBox bridy in collisionList ){
                        if(bridy.type ==2){
                            if(bullets.rect.Intersects(bridy.rect)){
                                   birdEngine.enemyList.Remove((Enemy)bridy.obj);
                            }
                        }
                    }
                }
            }
        }



        public void _collisionUpdate() {

            _maintainRectsScene();
            
            _OnCollision_char_bird();
            _Oncollision_bird_fire();
        }


        public void _OnCollision_char_bird() { 
            foreach(BoundingBox bird in collisionList){
                if(bird.type==2){
                    foreach(BoundingBox character in collisionList){
                        if(character.type==0){
                            if (bird.rect.Intersects(character.rect))
                            {
                                charEngine._collisionWithBird = 1;
                            }
                                                   
                       }
                    }
                }
            }
            
        }

    }

    class BoundingBox {
        public Rectangle rect;
        public int type;
        public object obj;
        public BoundingBox(int typ,Rectangle ret , object o) {
            rect = ret;
            type = typ;
            obj = o;
        }

    }
}
