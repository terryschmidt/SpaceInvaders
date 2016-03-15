using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienRemovalObserver : CollisionObserver
    {
        public AlienRemovalObserver()
        {

        }

        public override void Notify()
        {
            GameObject a = this.subject.gameObjA;
            GameObject b = this.subject.gameObjB;

            if (a is AlienCategory)
            {
                a.Remove();
                //if (a.pParent.pChild == null && a.pParent is Column)
                //{
                //    Debug.WriteLine("Removing column.");
                //    Values.columnCount--;
                //    GameObjectManager.Remove((GameObject)a.pParent);

                //    if (Values.columnCount == 0)
                //    {
                //        Debug.WriteLine("Removing Grid");
                //        GameObject grid = GameObjectManager.Find(GameObject.Name.Grid);
                //        grid.Remove();
                //        Values.columnCount = 11;
                //    }
                //}
            }
            else if (b is AlienCategory)
            {
                b.Remove();
                //if (b.pParent == null && a.pParent is Column)
                //{
                //    Debug.WriteLine("Removing column.");
                //    Values.columnCount--;
                //    GameObjectManager.Remove((GameObject)b.pParent);

                //    if (Values.columnCount == 0)
                //    {
                //        Debug.WriteLine("Removing Grid");
                //        GameObject grid = GameObjectManager.Find(GameObject.Name.Grid);
                //        grid.Remove();
                //        Values.columnCount = 11;
                //    }
                //}
            }
        }
    }
}
