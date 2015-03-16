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
         *     Contains the list of all the shots and update fires and draw them 
         ***/

    class FireSystem
    {
        public List<Bullets> fireList;
        
        int currentFrame = 0;
        int sheetStepper = 2;
        Texture2D bulletTexture;

        public FireSystem(Texture2D bulTex ) {
            fireList = new List<Bullets>();
            bulletTexture = bulTex;
            
        }


        public void addFire(Vector2 initPos , int dir) {
            Vector2 pos = new Vector2(initPos.X+60, initPos.Y + 40);
            Bullets b = new Bullets(bulletTexture , pos , dir , 6.0f);
            fireList.Add(b);

        }


        public void drawFireSystem(SpriteBatch spriteBatch){

            --sheetStepper;
            if(sheetStepper<0){
                sheetStepper = 20;
                frameUpdate();
            }
            Rectangle rect = new Rectangle(0*50,1*50,50,50);
            //default bulletType-0
            
            foreach(Bullets b in fireList){
            spriteBatch.Draw(bulletTexture,b.position,rect,Color.White,0.0f,Vector2.Zero,0.5f,SpriteEffects.None,0);
            }
        }



        public void updateFireSystem()
        { 
            //case for left shot
            if(fireList.Count>0){
            foreach(Bullets b in fireList){
                if (b.direction == 0 && b.maxDis > 0)
                {
                    b.position.X -= b.speed*2;
                    b.maxDis -= b.speed*2;
                }
                if (b.direction == 1 && b.maxDis > 0)
                {
                    b.position.Y += b.speed * 2;
                    b.maxDis -= b.speed * 2;
                } 
                if (b.direction == 2 && b.maxDis > 0)
                {
                    b.position.X += b.speed * 2;
                    b.maxDis -= b.speed * 2;
                }
                if (b.direction == 3 && b.maxDis > 0)
                {
                    b.position.Y -= b.speed * 2;
                    b.maxDis -= b.speed * 2;
                }
                //else fireList.Remove(b);
            }
            }
            emptyList();
        }


        public void emptyList() {
            for (int i = 0; fireList.Count > i;i++ )
            {
                if(fireList[i].maxDis < 0){
                    fireList.RemoveAt(i);
                }
            }
        }


        void frameUpdate() {
            ++currentFrame;
            if(currentFrame>4){
                currentFrame = 0;
            }
        }

    }


    
    class Bullets {
        Texture2D shotTexture;
        public Vector2 position;
        public int direction;
        public float speed;
        public float maxDis = 700.0f;

        //STATES
        public int _collisionWithBird = 0;


        public Bullets(Texture2D tex ,Vector2 pos , int dir , float sp) {
            shotTexture = tex;
            position = pos;
            direction = dir;
            speed = sp;
        }
    }

}
