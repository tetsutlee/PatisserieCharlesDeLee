using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class Timer
    {
        protected float currentTime;
        public Timer(float time)
        { 
            currentTime = time * 60;
        }

        public void CountDown()
        {
            currentTime--;
            if(currentTime <= 0)
            {
                currentTime = 0;
            }
        }

        public float GetTime()
        {
            return currentTime;
        }

        public void SetTime(float time)
        {
            currentTime = time * 60;
        }

        public float GetSecond()
        {
            return currentTime / 60;
        }
        public bool isTime()
        {
            return currentTime == 0;
        }
    }
}
