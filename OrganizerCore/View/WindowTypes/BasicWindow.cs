using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.WindowTypes
{

    public interface WindowInterface
    {
        bool    IsFocused();
        void    OnAction(ActionEventArgs e);
        string  GetData();
    }

    public abstract class BasicWindow : WindowInterface
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor FontColor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width     { get; set; }
        public int Height    { get; set; }
        public abstract void Draw();
    }
}
