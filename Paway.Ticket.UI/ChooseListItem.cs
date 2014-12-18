using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Paway.Ticket.UI.Enums;

namespace Paway.Ticket.UI
{
    [Serializable]
    public class ChooseListItem
    {
        private Rectangle _bounds = Rectangle.Empty;
        private EMouseState _mouseState = EMouseState.Normal;
        private string _text = "item";
        private object _tag = null;

        internal Rectangle Bounds
        {
            get { return this._bounds; }
            set { this._bounds = value; }
        }
        internal EMouseState MouseState
        {
            get { return this._mouseState; }
            set { this._mouseState = value; }
        }
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        public object Tag
        {
            get { return this._tag; }
            set { this._tag = value; }
        }
    }
}
