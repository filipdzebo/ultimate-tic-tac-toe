using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOks.Models
{
    public enum ZNAK { X, O }
    public class Player
    {
        public ZNAK Znak { get; internal set; }
        public String Name { get; internal set; }

        public Player(ZNAK znak, string name)
        {
            Znak = znak;
            Name = name;
        }

        public override string ToString()
        {
            return Name + "(" + Znak + ")";
        }
    }
}
