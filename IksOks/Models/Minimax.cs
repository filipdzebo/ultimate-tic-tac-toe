using IksOks.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static IksOks.Models.IksOksIgra;

namespace IksOks.Models
{
    class Minimax
    {
        public const int MAX_DEPTH = 6;
        public long Posjeceno = 0;
        public TimeSpan vrijemeRacunanja;
        public Mjesto bestMjesto = null;
        private static Stopwatch stoperica = new Stopwatch();
         
        public   int doMinimax(UltimateIksOks igra, int dubina,   Player MaximizingPlayer, int alpha, int beta )
        {
            if (dubina == 0)
            {
                stoperica.Reset();
                stoperica.Start();
                bestMjesto = null;
                Posjeceno = 0;
            } 

            if (igra.DostupnaMjesta.Count == 0 || dubina > MAX_DEPTH || igra.Pobjednik!=null)
            { 
                return heuristic(igra, MaximizingPlayer,   dubina);
            } 
             
            if (igra.PlayerPlaying == MaximizingPlayer)
            {
                int max = Int32.MinValue;
                foreach (var mjesto in igra.DostupnaMjesta)
                {
                    var nextGame = igra.NextIgraToBePlayed;
                    igra.MakeMove(mjesto );
                    var value = doMinimax(igra, dubina + 1,   MaximizingPlayer, alpha, beta);
                    
                    if(value> max && dubina == 0)
                    {
                        bestMjesto = mjesto; 
                    }
                    max = Math.Max(value, max);
                    alpha = Math.Max(alpha, max);
                    igra.UndoMove(mjesto.X, mjesto.Y, mjesto.Parent.XuUIO, mjesto.Parent.YuUIO);
                    igra.NextIgraToBePlayed = nextGame;
                    if (beta <= alpha) { break;  }
                }
                if (dubina == 0)
                {
                    stoperica.Stop();
                    vrijemeRacunanja = stoperica.Elapsed;
                }
                return max;
            }
            else
            {
                int min = Int32.MaxValue;
                foreach (var mjesto in igra.DostupnaMjesta)
                {
                    var nextGame = igra.NextIgraToBePlayed;
                    igra.MakeMove(mjesto );
                    var value = doMinimax(igra, dubina + 1,   MaximizingPlayer, alpha, beta); 
                    min = Math.Min(min, value);
                    beta = Math.Min(beta, min);
                    igra.UndoMove(mjesto.X, mjesto.Y, mjesto.Parent.XuUIO, mjesto.Parent.YuUIO);
                    igra.NextIgraToBePlayed = nextGame;
                    if (beta <= alpha) { break; }
                }
                if (dubina == 0)
                {
                    stoperica.Stop();
                    vrijemeRacunanja = stoperica.Elapsed;
                }
                return min;
            }
        }
        private int heuristic(UltimateIksOks igra, Player MaximizingPlayer,   int dubina)
        {
            int bodoviZaMaks = 0;
            if (igra.Pobjednik != null)
            {
                bodoviZaMaks = igra.Pobjednik == MaximizingPlayer ? 100 : -100;
                bodoviZaMaks += igra.Pobjednik == MaximizingPlayer ? -dubina : dubina;
            }
            else
            {
                foreach (var malaigra in igra.igre)
                {
                    if(malaigra.Pobjednik != null)
                    {
                        bodoviZaMaks += (malaigra.Pobjednik == MaximizingPlayer) ? 20 : -20;
                    }
                }
            }
            Posjeceno++;
            //Console.WriteLine("Bodovi " + bodoviZaMaks + " Dubina " + dubina + "; Posjeceno :" + Posjeceno); 
            return bodoviZaMaks;
        }
    }
}
