using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver : CollisionObserver
    {
        public override void Notify()
        {
            GameObject ufoToRemove = GameObjectManager.Find(GameObject.Name.UFO);
            ufoToRemove.Remove();
            GameObject boxToRemove = GameObjectManager.Find(GameObject.Name.UFORoot);
            boxToRemove.Remove();
        }
    }
}
