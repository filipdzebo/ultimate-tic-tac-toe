using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOks.Models
{
    public partial class IksOksIgra
    {
        private Mjesto[,] matrica;
        public int XuUIO { get; set; }
        public int YuUIO { get; set; } 
        public UltimateIksOks Parent { get; internal set; }

        public IksOksIgra( int XuUIO,int  YuUIO,   UltimateIksOks parent = null)
        {
            matrica = new Mjesto[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrica[i, j] = new Mjesto()
                    {
                        X = i,
                        Y = j,
                        Parent = this,
                        player = null
                    };
                }
            }
            this.Parent = parent;
            this.XuUIO = XuUIO;
            this.YuUIO = YuUIO; 

        }
        public Player Pobjednik   {

            get{
                for (int i = 0; i < 3; i++)
                {
                    if ((matrica[i, 0].player == matrica[i, 1].player) && (matrica[i, 1].player == matrica[i, 2].player) && matrica[i, 0].player != null)
                    {
                        return matrica[i, 0].player;  
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((matrica[0, j].player == matrica[1, j].player) && (matrica[1, j].player == matrica[2, j].player) && matrica[0, j].player != null)
                    {
                        return matrica[0, j].player;  
                    }
                }
                if ((matrica[0, 0].player == matrica[1, 1].player) && (matrica[1, 1].player == matrica[2, 2].player) && matrica[0, 0].player != null) { return matrica[0, 0].player;  }
                if ((matrica[0, 2].player == matrica[1, 1].player) && (matrica[1, 1].player == matrica[2, 0].player) && matrica[2, 0].player != null) { return matrica[2, 0].player;  }
                return null;
            }
        
        }

        internal void UndoMove(int x, int y, Player playerPlaying)
        {
            if (matrica[x, y].player == playerPlaying)
            {
                matrica[x, y].player = null;
            }
            else
            {
                throw new Exception("Pokusaj undo operacije nije legalan.");
            }
        }
        public void OdigrajKorak(Mjesto mjesto, Player p)
        {
            if (Pobjednik == null)
            {
                if (DostupnaMjesta.Contains(mjesto))
                {
                    mjesto.player = p;
                    return;
                }
                else
                {
                    throw new Exception("Pokusaj igranja na vec zauzeto mjesto!");
                }
            }
            else
            {
                throw new Exception("Pokusaj igranja na vec zavrsenu igru!");
            }
        }
         
        public List<Mjesto> DostupnaMjesta
        {
            get 
            {
                var mjesta = new List<Mjesto>();
                foreach(var m in matrica)
                {
                    if (m.player==null) {
                        mjesta.Add(m);
                    }
                }
                return mjesta;
            }
        }
         
      
    }


}
