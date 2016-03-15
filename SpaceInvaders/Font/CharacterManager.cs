using System;
using System.Diagnostics;
using System.Xml;

namespace SpaceInvaders
{
    class CharacterManager : Manager
    {
        // data:
        private static CharacterManager instance = null;
        private Character referenceNode;

        private CharacterManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (Character)this.CreateNode();
        }

        ~CharacterManager()
        {
            this.referenceNode = null;
            CharacterManager.instance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new CharacterManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            CharacterManager inst = CharacterManager.getInstance();
            inst.baseDestroy();
        }

        public static Character Add(Character.Name charNameArg, int keyArg, Texture.Name textNameArg, float xArg, float yArg, float widthArg, float heightArg)
        {
            CharacterManager inst = CharacterManager.getInstance();
            Character node = (Character)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(charNameArg, keyArg, textNameArg, xArg, yArg, widthArg, heightArg);
            return node;
        }

        public static void AddXML(Character.Name charNameArg, String asset, Texture.Name textNameArg)
        {
            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(asset);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // have all the data... so now create a character
                            Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            CharacterManager.Add(charNameArg, key, textNameArg, x, y, width, height);
                        }
                        break;
                }
            }
        }

        public static void Remove(Character nodeArg)
        {
            Debug.Assert(nodeArg != null);
            CharacterManager inst = CharacterManager.getInstance();
            inst.baseRemove(nodeArg);
        }

        public static Character Find(Character.Name charNameArg, int keyArg)
        {
            CharacterManager inst = CharacterManager.getInstance();
            inst.referenceNode.name = charNameArg;
            inst.referenceNode.key = keyArg;
            Character nodeToReturn = (Character)inst.baseFind(inst.referenceNode);
            return nodeToReturn;
        }

        public static void Dump()
        {
            CharacterManager inst = CharacterManager.getInstance();
            inst.baseDump("CharacterManager");
        }

        public static void DumpStats()
        {
            CharacterManager inst = CharacterManager.getInstance();
            inst.baseDumpStats("CharacterManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            Character firstChar = (Character)first;
            Character secondChar = (Character)second;

            if (firstChar.name == secondChar.name && firstChar.key == secondChar.key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override MLink CreateNode()
        {
            MLink node = new Character();
            Debug.Assert(node != null);
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            Character node = (Character)link;
            Debug.Assert(node != null);
            node.Dump();
        }

        // private

        private static CharacterManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
