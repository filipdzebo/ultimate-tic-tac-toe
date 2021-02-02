namespace IksOks.Models
{
    public partial class IksOksIgra
    {
        public class Mjesto
        {
            public IksOksIgra Parent { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public Player player = null;
        }

        
    }


}
