using Pik.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pik.UI
{
    public class PikSelectedEventArgs : EventArgs
    {
        public PikColor SelectedPikColor { get; set; }
    }
}
