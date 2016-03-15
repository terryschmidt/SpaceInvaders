using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridObserver : CollisionObserver
    {
        public GridObserver()
        {

        }

        public override void Notify()
        {
            //Debug.WriteLine("GridObserver: {0} {1}", this.subject.gameObjA, this.subject.gameObjB);

            Grid grid = (Grid)this.subject.gameObjA;

            WallCategory wall = (WallCategory)this.subject.gameObjB;

            if (wall.GetCategory() == WallCategory.Type.Right)
            {
                grid.delta = -6.5f;

                PCSTreeForwardIterator iter = new PCSTreeForwardIterator(grid);
                Debug.Assert(iter != null);

                PCSNode pNode = iter.First();

                while (!iter.IsDone())
                {
                    // delta
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.x += grid.delta;


                    // Advance
                    pNode = iter.Next();
                }

                
                grid.PushGridDownward();
            }
            else if (wall.GetCategory() == WallCategory.Type.Left)
            {
                grid.delta = 6.5f;

                PCSTreeForwardIterator iter = new PCSTreeForwardIterator(grid);
                Debug.Assert(iter != null);

                PCSNode pNode = iter.First();

                while (!iter.IsDone())
                {
                    // delta
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.x += grid.delta;


                    // Advance
                    pNode = iter.Next();
                }

                
                grid.PushGridDownward();
            }
            else if (wall.GetCategory() == WallCategory.Type.Bottom)
            {
                TimerManager.Wait(99999);
                FontManager.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "GAME OVER", Character.Name.Consolas36pt, 350, 550);
                Ship shippy = ShipManager.GetShip();
                shippy.SetState(ShipManager.State.End);
            }   
        }
    }
}
