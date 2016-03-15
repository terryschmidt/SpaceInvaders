using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombObserver : CollisionObserver
    {
        public override void Notify()
        {
            Bomb b = (Bomb)this.subject.gameObjA;
            //b.Reset();
            b.Remove();
        }
    }
}
