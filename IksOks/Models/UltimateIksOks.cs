using IksOks.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static IksOks.Models.IksOksIgra;

namespace IksOks.Models
{
    public class UltimateIksOks
    { 
        public IksOksIgra[,] igre = new IksOksIgra[3, 3];

        public IksOksIgra NextIgraToBePlayed { get; set; } = null;

        public Player PlayerHuman { get; internal set; }
        public Player PlayerAI { get; internal set; }
        public Player PlayerPlaying { get; internal set; }

        public Player Pobjednik { get
            {
                for (int i = 0; i < 3; i++)
                {
                    if ((igre[i, 0].Pobjednik == igre[i, 1].Pobjednik) && (igre[i, 1].Pobjednik == igre[i, 2].Pobjednik) && igre[i, 0].Pobjednik != null)
                    {
                        return igre[i, 0].Pobjednik; 
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((igre[0, j].Pobjednik == igre[1, j].Pobjednik) && (igre[1, j].Pobjednik == igre[2, j].Pobjednik) && igre[0, j].Pobjednik != null)
                    {
                        return igre[0, j].Pobjednik;  
                    }
                }
                if ((igre[0, 0].Pobjednik == igre[1, 1].Pobjednik) && (igre[1, 1].Pobjednik == igre[2, 2].Pobjednik) && igre[0, 0].Pobjednik != null) { return igre[0, 0].Pobjednik;   }
                if ((igre[0, 2].Pobjednik == igre[1, 1].Pobjednik) && (igre[1, 1].Pobjednik == igre[2, 0].Pobjednik) && igre[2, 0].Pobjednik != null) { return igre[2, 0].Pobjednik;  }
                return null;
            }

            }

        public UltimateIksOks(Player playerHuman, Player PlayerAI)
        {
            PlayerHuman = playerHuman;
            this.PlayerAI = PlayerAI;

            PlayerPlaying = playerHuman.Znak == ZNAK.X ? playerHuman : PlayerAI;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    IksOksIgra igra = new IksOksIgra(i, j, this);
                    igre[i, j] = igra;
                }
            }

        }
        public void MakeMove(Mjesto mjesto)
        {
            if (Pobjednik != null) { throw new Exception("Pokusaj igranja na zavrsenu Ultimate igru."); };

            if (NextIgraToBePlayed != null && NextIgraToBePlayed != mjesto.Parent) { throw new Exception("Pokusaj igranja na drugu igru, a ne obaveznu."); };


            mjesto.Parent.OdigrajKorak(mjesto, PlayerPlaying);

            switchPlayer();
            NextIgraToBePlayed = igre[mjesto.X, mjesto.Y].Pobjednik == null ? igre[mjesto.X, mjesto.Y] : null;
        }

        internal void UndoMove(int x, int y, int xuUIO, int yuUIO)
        {
            switchPlayer();
            igre[xuUIO, yuUIO].UndoMove(x, y, PlayerPlaying); 
        }

        public List<Mjesto> DostupnaMjesta
        {
            get
            {
                if (NextIgraToBePlayed != null)
                {
                    var nextNaOsnovuIgre = NextIgraToBePlayed.DostupnaMjesta;
                    if (nextNaOsnovuIgre.Count > 0)
                    {
                        return nextNaOsnovuIgre;
                    }
                }
                List<Mjesto> dostupna = new List<Mjesto>();
                foreach(var i in igre)
                {
                    if(i.Pobjednik==null)
                    dostupna.AddRange(i.DostupnaMjesta);
                }
                return dostupna;
            }
        }
         
        private void switchPlayer()
        {
            PlayerPlaying = (PlayerPlaying == PlayerHuman ? PlayerAI : PlayerHuman);
        }

         

     

     
    }
}
