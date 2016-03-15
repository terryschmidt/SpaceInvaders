using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : MLink
    {
        // data: 

        public Name name;
        public Azul.Texture azulTexture;

        public enum Name
        {
            Aliens,
            Stitch,
            AngryBirds,
            NullObject,
            Shield,
            Consolas36pt,
            SpaceInvaders,
            Uninitialized
        }

        public Texture()
            : base()
        {
            name = Texture.Name.Uninitialized;
            azulTexture = new Azul.Texture();
            Debug.Assert(azulTexture != null);
        }

        public void Set(Texture.Name nameToSet, String path, Azul.Texture_Filter min, Azul.Texture_Filter mag)
        {
            Debug.Assert(path != null);
            Debug.Assert(this.azulTexture != null);
            name = nameToSet;
            azulTexture.Set(path, min, mag);
        }

        // print function

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("\t\t   pAzulTexture: {0}", this.azulTexture.GetHashCode());
        }
    }
}
