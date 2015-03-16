using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VaibhavKabeta.AnimationEngine
{


    public class animationData
    {
        public Dictionary<string, List<Rectangle>> _frameList;
        public animationData()
        {
            _frameList = new Dictionary<string, List<Rectangle>>();

        }

        public void addData(string s, List<Rectangle> _l)
        {
            if (_frameList != null)
            {
                _frameList.Add(s, _l);
            }
        }

        public Dictionary<string, List<Rectangle>> getData()
        {
            return _frameList;
        }
    }


    /*
     *     Gets the animation data for a partiluar state and plays it withing the constrained FPS
     *     using the ELAPSEDTIME
     */
    public class AnimationEngine
    {
        static float timePerFrame = 0.20f;
        public Dictionary<string, List<Rectangle>> _frameList;
        public AnimationEngine(Dictionary<string, List<Rectangle>> _list)
        {
            _frameList = _list;
        }

        public Rectangle playAnimation(string _name, GameTime gt)
        {
            return getSrcRect(_frameList[_name], (float)gt.ElapsedGameTime.TotalSeconds);
        }


        int frameCount = 0;
        float tspan;
        Rectangle getSrcRect(List<Rectangle> _fList, float elaspsed)
        {

            tspan += elaspsed;
            if (tspan > timePerFrame)
            {
                frameCount++;
                frameCount = frameCount % _fList.Count;
                tspan -= timePerFrame;
                return _fList[frameCount];
            }

            return _fList[frameCount % _fList.Count];
        }
    }


}
