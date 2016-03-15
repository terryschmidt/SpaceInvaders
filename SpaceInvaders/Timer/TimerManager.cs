using System;
using System.Diagnostics;

// Terry Schmidt, PA4, SE456

namespace SpaceInvaders
{
    class TimerManager : Manager
    {
        // data:
        private static TimerManager instance = null;
        private TimerEvent referenceNode;
        protected float currentTime;

        private TimerManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (TimerEvent)this.CreateNode();
            this.currentTime = 0.0f;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new TimerManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            TimerManager inst = TimerManager.getInstance();
            inst.baseDestroy();
        }

        public static void Add(TimerEvent.Name imageName, Command comm, float deltaTimeToTrigger)
        {
            TimerManager inst = TimerManager.getInstance();
            TimerEvent node = (TimerEvent)inst.pullAndReturn();
            node.Set(imageName, comm, deltaTimeToTrigger);
            inst.baseInsertSorted(node);   
        }

        public static void Remove(TimerEvent node)
        {
            Debug.Assert(node != null);
            TimerManager inst = TimerManager.getInstance();
            inst.baseRemove(node);
        }

        public static void Wait(float timeToWait)
        {
            TimerManager inst = TimerManager.getInstance();
            TimerEvent node = (TimerEvent)inst.active;

            while (node != null)
            {
                //if (node.name == TimerEvent.Name.SplatRemove || node.name == TimerEvent.Name.MissileWallSplatRemove || node.name == TimerEvent.Name.MissileBombSplatRemove)
                //{
                //    node = (TimerEvent)node.next;
                //}
                //else
                //{
                    node.triggerEventTime += timeToWait;
                    node = (TimerEvent)node.next;
                //}
            }
        }

        public static TimerEvent Find(TimerEvent.Name tName)
        {
            TimerManager inst = TimerManager.getInstance();
            inst.referenceNode.name = tName;
            TimerEvent data = (TimerEvent)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            TimerManager inst = TimerManager.getInstance();
            inst.baseDump("TimerManager");
        }

        public static float getCurrentTime()
        {
            TimerManager inst = TimerManager.getInstance();
            return inst.currentTime;
        }

        public static void Update(float totalTime)
        {
            TimerManager inst = TimerManager.getInstance();
            inst.currentTime = totalTime;
            TimerEvent node = (TimerEvent)inst.active;
            TimerEvent next = null;
            
            // OPTIMIZED VERSION:

            while ((node != null) && (inst.currentTime >= node.triggerEventTime))
            {
                /*next = (TimerEvent)node.next;

                if (inst.currentTime >= node.triggerEventTime)
                {
                    node.Process();
                    inst.baseRemove(node);
                }

                node = next;*/

                next = (TimerEvent)node.next;
                node.Process();
                inst.baseRemove(node);
                node = next;
            }

            // FIXES RUN TIME BUG FOR THE MOMENT:

            //while (node != null)
            //{
            //    next = (TimerEvent)node.next;

            //    if (inst.currentTime >= node.triggerEventTime)
            //    {
            //        node.Process();
            //        inst.baseRemove(node);
            //    }

            //    node = next;

            //    //next = (TimerEvent)node.next;
            //    //node.Process();
            //    //inst.baseRemove(node);
            //    //node = next;
            //}
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            TimerEvent firstTE = (TimerEvent)first;
            TimerEvent secondTE = (TimerEvent)second;

            if (firstTE.name == secondTE.name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override Boolean Sort(TimerEvent nodeArg)
        {
            TimerEvent newEventToAdd = (TimerEvent)nodeArg;
            TimerEvent currentNode = (TimerEvent)this.active;
            
            // case where the list is empty
            if (currentNode == null)
            {
                this.active = newEventToAdd;
                newEventToAdd.next = null;
                newEventToAdd.prev = null;
            }
            // case where node is smaller than the head (then we add it as the head)
            else if (newEventToAdd.triggerEventTime <= currentNode.triggerEventTime)
            {
                this.active = newEventToAdd;
                newEventToAdd.next = currentNode;
                newEventToAdd.prev = null;
                currentNode.prev = newEventToAdd;
            }
            else
            {
                while (currentNode.next != null)
                {
                    TimerEvent nxtAsTimerEvent = (TimerEvent)currentNode.next;

                    if (newEventToAdd.triggerEventTime <= nxtAsTimerEvent.triggerEventTime)
                    {
                        // found a bigger node
                        break;
                    }

                    currentNode = (TimerEvent)currentNode.next;
                }

                if (currentNode.next != null)
                {
                    newEventToAdd.next = currentNode.next;
                    newEventToAdd.prev = currentNode;
                    currentNode.next = newEventToAdd;
                    newEventToAdd.next.prev = newEventToAdd;
                }
                else
                {
                    // there aren't any bigger nodes, now it gets put at the end of the list
                    currentNode.next = newEventToAdd;
                    newEventToAdd.prev = currentNode;
                    newEventToAdd.next = null;
                }
            }
            
            return true;
        }

        protected override MLink CreateNode()
        {
            MLink node = new TimerEvent();
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            TimerEvent node = (TimerEvent)link;
            // node.dump();
        }

        // private

        private static TimerManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
