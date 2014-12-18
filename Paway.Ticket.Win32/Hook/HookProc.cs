using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Paway.Ticket.Win32
{
    /// <summary>
    /// 钩子委托声明
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

    /// <summary>
    /// 鼠标更新事件委托声明
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="y">y坐标</param>
    public delegate void MouseUpdateEventHandler(int x, int y);

}

