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

    enum _MenuState { _start , _about , _exit  }

    public class MenuScreen : IScreenNode
    {

        AnimationEngine.animationData fudduAnimationData;
        AnimationEngine.AnimationEngine fudduAnmationEngine;

       public MenuScreen() 
       {
           _positions = new List<Vector2>();
           _positions.Add(new Vector2(720 , 250 ));
           _positions.Add( new Vector2(400 , 400) );
           _positions.Add(new Vector2(820, 500));

           fudduAnimationData = new AnimationEngine.animationData();
           initAnimationData();
           fudduAnmationEngine = new AnimationEngine.AnimationEngine(fudduAnimationData.getData());
       }
        ~MenuScreen(){}


        Texture2D _menuBg;
        Texture2D _menuSelector;
        Texture2D _name;
        List<Vector2> _positions;
        int counter = 0;
        float rot_val = 0.0f;

        static _MenuState _currentMenuState = _MenuState._start;


        void initAnimationData()
        {
            fudduAnimationData.addData("simpleIdle", new List<Rectangle>() 
            {
                new Rectangle(0,0+10,200,190),
                new Rectangle(00,200+10,200,190),
                new Rectangle(00,400+10,200,190),
                new Rectangle(00,600+10,200,190)
            });
        }

        public void LoadContent() 
        {
            _menuBg = Game1.contentMgr.Load<Texture2D>("menuSystem45");
            _menuSelector = Game1.contentMgr.Load<Texture2D>("menuSelector3");
            //Console.WriteLine("MenuSystem : _position<List> : "+_positions[1].ToString());
            _name = Game1.contentMgr.Load<Texture2D>("BirdyRedempNew");
        }
        
        public void Draw(SpriteBatch batch) 
        {
            Game1.spriteBatch.Draw(_name, new Rectangle(0, 0, 1000, 600), Color.White);
            Game1.spriteBatch.Draw(_menuBg, new Rectangle(0, 0, 1000, 600), Color.White);
            Game1.spriteBatch.Draw(Game1.characterSheet, new Vector2(580,350), fudduAnmationEngine.playAnimation("simpleIdle", Game1.gt), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
     
            Game1.spriteBatch.Draw(_menuSelector, _positions[counter], new Rectangle(0, 0, 100, 100), Color.White, rot_val, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
             
        }
        
        public void Update(GameTime gt)
        {
            rot_val += 0.05f;
           
            HandleInput();
            setState();
           
            if(_currentMenuState == _MenuState._start && InputSystem.KeyboardInterface.getKeyboardInstance().isNewKeyPressed(Keys.Enter))
            {
                GameStateManager.setGameState(GameStates._inGameScreen);
            }
        }
        
        public void HandleInput()
        {


            if (InputSystem.KeyboardInterface.getKeyboardInstance().isNewKeyPressed(Keys.Down))
            {
                counter++;
                counter = counter % 3;
                Console.WriteLine("Menu_ counter Down: "+counter.ToString());
            }
            if (InputSystem.KeyboardInterface.getKeyboardInstance().isNewKeyPressed(Keys.Up))
            {
                if (counter != 0) { counter--; }else { counter = 2; }
                
            }
        }

        void setState()
        {
            switch(counter)
            {
                case 0:
                    _currentMenuState = _MenuState._start;
                    break;
                case 1:
                    _currentMenuState = _MenuState._about;
                    break;
                case 2:
                    _currentMenuState = _MenuState._exit;
                    break;
                default:
                    break;
            }
        }
       
    }
}
