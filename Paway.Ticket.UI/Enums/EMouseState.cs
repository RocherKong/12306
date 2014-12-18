using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket.UI.Enums
{
    /// <summary>
    /// 鼠标状态
    /// </summary>
    [Serializable]
    public enum EMouseState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 鼠标滑过
        /// </summary>
        MouseMove,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        MouseDown,
        /// <summary>
        /// 鼠标释放
        /// </summary>
        MouseUp,
        /// <summary>
        /// 鼠标离开
        /// </summary>
        MouseLeave,
    }
}
