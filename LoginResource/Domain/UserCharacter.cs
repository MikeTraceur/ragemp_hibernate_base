using System;
using System.Collections.Generic;
using System.Text;

namespace LoginResource.Domain
{
    public class UserCharacter
    {
        public virtual int ID { get; set; }
        public virtual User user { get; set; } //Correct
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }
        public virtual float RX { get; set; }
        public virtual float RY { get; set; }
        public virtual float RZ { get; set; }
        public virtual int CSlot1 { get; set; }
    }
}
