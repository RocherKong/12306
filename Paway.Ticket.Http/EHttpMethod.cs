using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.Http
{
    [Flags]
    public enum EHttpMethod : uint
    {
        Post = 0,
        Get = 1,
    }
}
