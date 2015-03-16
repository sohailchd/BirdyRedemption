using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VaibhavKabeta.Screens
{
    class HUDHandler
    {

        static SpriteFont spriteFont;
        static int intialized = 0;

        public static void showTextAt(string text, Vector2 at, Color c)
        {
            if (HUDHandler.intialized == 0)
            {
                spriteFont = Game1.contentMgr.Load<SpriteFont>("SpriteText");
                HUDHandler.intialized = 1;
            }
            Game1.spriteBatch.DrawString(spriteFont, text, at, c, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

    }
}
