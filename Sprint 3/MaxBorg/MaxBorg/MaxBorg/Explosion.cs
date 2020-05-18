using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxBorg
{
    class Explosion
    {
        public Rectangle frame;
        public int timer;

        public Explosion(Rectangle f)
        {
            frame = f;
            timer = 120;
        }
    }
}
