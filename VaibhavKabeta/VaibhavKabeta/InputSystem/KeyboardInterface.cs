using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VaibhavKabeta.InputSystem
{
    public class KeyboardInterface
    {
         KeyboardState _last_KB_State;
        KeyboardState _current_KB_State;

        static KeyboardInterface kbInstance = null;
        
        KeyboardInterface()
        {
 
        }

        public static KeyboardInterface getKeyboardInstance()
        {
            if(kbInstance==null)
            {
                kbInstance = new KeyboardInterface();
            }

            return kbInstance;
        }




        public void KB_InputUpdate()
        {
            _last_KB_State = _current_KB_State;
            _current_KB_State = Keyboard.GetState(PlayerIndex.One);


        
        }

        public bool isLastKeyUp(Keys key)
        {
            return _current_KB_State.IsKeyUp(key);


        }

        public bool isKeyPressed(Keys key)
        {
            return _current_KB_State.IsKeyDown(key);
        }

        public bool isNewKeyPressed(Keys key)
        {
            return ( _current_KB_State.IsKeyDown(key) && _last_KB_State.IsKeyUp(key));
        }


    
    }
}
