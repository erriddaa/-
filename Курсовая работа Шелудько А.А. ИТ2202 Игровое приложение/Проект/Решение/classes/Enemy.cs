using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ДУДЛ.classes
{
    public class Enemy : Player
    {
        public Enemy(PointF pos, int type)
        {
            switch (type)
            {
                case 1:
                    sprite = Properties.Resources.enemy1r;
                    physics = new Physics(pos, new Size(45, 50));
                    break;
                case 2:
                    sprite = Properties.Resources.enemy2r;
                    physics = new Physics(pos, new Size(53, 60));
                    break;
                case 3:
                    sprite = Properties.Resources.enemy3r;
                    physics = new Physics(pos, new Size(77, 77));
                    break;

            }

        }
    }
}
