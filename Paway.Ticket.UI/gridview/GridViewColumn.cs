using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paway.Ticket.UI
{
    [Serializable]
    public abstract class GridViewColumn
    {
        private string _text = string.Empty;

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
    }
}
