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
    public class SplashScreen : IScreenNode
    {
        Texture2D _gameName0;
        Texture2D _gameName1;
        Texture2D _studioName;
        Texture2D _scrollerBg01;
        Texture2D _scrollerBg02;
        Texture2D _birdy;

        int offset01 = 0;
        int offset02 = 0;
        Vector2 _glowPos;

        AnimationEngine.animationData fudduAnimationData;
        AnimationEngine.AnimationEngine fudduAnmationEngine;

        AnimationEngine.animationData birdyAnimationData;
        AnimationEngine.AnimationEngine birdyAnmationEngine;
        AnimationEngine.AnimationEngine birdyAnmationEngine1;
        AnimationEngine.AnimationEngine birdyAnmationEngine2;


        void initAnimationData()
        {
            fudduAnimationData.addData("simpleIdle", new List<Rectangle>() 
            {
                new Rectangle(0,0+10,200,190),
                new Rectangle(00,200+10,200,190),
                new Rectangle(00,400+10,200,190),
                new Rectangle(00,600+10,200,190)
            });

            birdyAnimationData.addData("fly", new List<Rectangle>() 
            {
                new Rectangle(0,0,175,120),
                new Rectangle(175,0,175,120),
                new Rectangle(350,0,175,120),
            
            });

        }

        public SplashScreen() 
        {
            _glowPos = Vector2.Zero;
            fudduAnimationData = new AnimationEngine.animationData();
            birdyAnimationData = new AnimationEngine.animationData();
            initAnimationData();
            fudduAnmationEngine = new AnimationEngine.AnimationEngine(fudduAnimationData.getData());
            birdyAnmationEngine = new AnimationEngine.AnimationEngine(birdyAnimationData.getData());
            birdyAnmationEngine1 = new AnimationEngine.AnimationEngine(birdyAnimationData.getData());
            birdyAnmationEngine2 = new AnimationEngine.AnimationEngine(birdyAnimationData.getData());
        }
        ~SplashScreen() { }


        public void LoadContent()
        { 
            _gameName0 = Game1.contentMgr.Load<Texture2D>("BirdyRedemp");
            _gameName1 = Game1.contentMgr.Load<Texture2D>("birdyRedemp3");
            _studioName = Game1.contentMgr.Load<Texture2D>("StudioName");
            _scrollerBg01 = Game1.contentMgr.Load<Texture2D>("scrollerBg");
            _scrollerBg02 = Game1.contentMgr.Load<Texture2D>("scrollerBg");
            _birdy = Game1.contentMgr.Load<Texture2D>("blackBirdy");


        }

        public void Draw(SpriteBatch batch) 
        {
            Game1.spriteBatch.Draw(_gameName0,new Rectangle(0,0,1000,600),Color.White);
            //Game1.spriteBatch.Draw(_gameName1,_glowPos , new Rectangle(10, 10, 1000, 600) ,Color.White , 0 , Vector2.Zero,1.0f,SpriteEffects.None,0);
            if(Game1.gt.TotalGameTime.TotalSeconds > 4)
            {
                Game1.spriteBatch.Draw(_studioName, new Rectangle(0, 0, 1000, 600), Color.White);
            
            }
            Game1.spriteBatch.Draw(Game1.characterSheet, new Vector2(380, 70), fudduAnmationEngine.playAnimation("simpleIdle", Game1.gt), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            Game1.spriteBatch.Draw(_birdy, new Vector2(800 + offset01*2, 400), birdyAnmationEngine.playAnimation("fly", Game1.gt), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);
           Game1.spriteBatch.Draw(_birdy, new Vector2(800+200 + offset01 * 2, 500), birdyAnmationEngine1.playAnimation("fly", Game1.gt), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);
           Game1.spriteBatch.Draw(_birdy, new Vector2(800+400 + offset01 * 2, 300), birdyAnmationEngine2.playAnimation("fly", Game1.gt), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);
           
            
            Game1.spriteBatch.Draw(_scrollerBg01, new Rectangle(0+offset01, 400, 1000, 200), Color.White);
            Game1.spriteBatch.Draw(_scrollerBg02, new Rectangle(1000+offset02, 400, 1000, 200), Color.White);
        }
        public void Update(GameTime gt) 
        {
            if(Game1.gt.TotalGameTime.TotalSeconds > 50)
            {
                GameStateManager.setGameState(GameStates._menuScreen);
            }


            //Scroller   ---------------------------------
            offset01 -= 1;
            offset02 -= 1;

            if(offset01<-1000)
            {
                offset01 = 0;
            }
            if (offset02 < -1000) { offset02 = 0; }
            // end scroller  ------------------------------



        }

        public void HandleInput() 
        {

        }
    }
}
