﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VaibhavKabeta
{
    public interface  IScreenNode
    {



        void LoadContent();
        void Draw(SpriteBatch batch);
        void Update(GameTime gt);
        void HandleInput();


    }
}
