using System;
using System.Collections.Generic;


namespace VaibhavKabeta
{

    enum GameStates { _splashScreen , _menuScreen , _inGameScreen  }

    class GameStateManager
    {
        GameStateManager() 
        {
         
        }
        ~GameStateManager() { }

        
        static GameStates _currentState = GameStates._splashScreen;
        public static void setGameState(GameStates gt)
        {
            GameStateManager._currentState = gt;
        }

        public static GameStates getCurrentState()
        {
            return GameStateManager._currentState;
        }


    }
}
