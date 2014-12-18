using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.UI
{
    [Flags]
    public enum EGridViewColumnType : uint
    {
        GridViewLabelColumn = 0,
        GridViewComboBoxColumn = 1,
    }
}
