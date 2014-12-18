using System;
using System.Collections.Generic;
using System.Text;

namespace Paway.Ticket.Win32
{
    /// <summary>
    /// Win32 常量
    /// </summary>
    public class Consts
    {
        public const long PRF_CLIENT = 0x00000004L;
        public const long PRF_ERASEBKGND = 0x00000008L;

        public const int EM_POSFROMCHAR = 0x00D6;

        public const int WM_PRINT = 0x0317;

        public static readonly IntPtr FALSE = IntPtr.Zero;
        public static readonly IntPtr TRUE = new IntPtr(1);

        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;//16
        public const int HTBOTTOMRIGHT = 17;
        public const int HTCAPTION = 2;

        public const int VK_LBUTTON = 0x1;
        public const int VK_RBUTTON = 0x2;

        #region mouse_event
        /// <summary>
        /// 移动鼠标
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001;
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        /// <summary>
        /// 释放鼠标中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        /// <summary>
        /// 标示是否采用绝对坐标 
        /// </summary>
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        #endregion

        #region keybd_event
        /// <summary>
        /// 抬起按键
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x0002;
        /// <summary>
        /// 按下按键
        /// </summary>
        public const int KEYEVENTF_KEYDOWN = 0x0000;

        #endregion

        #region OpenProcess
        /// <summary>
        /// 所有可能的进程对象的访问权限
        /// </summary>
        public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        /// <summary>
        /// 需要在内存中读取进程应使用ReadProcessMemory
        /// </summary>
        public const int PROCESS_VM_READ = 0x0010;
        /// <summary>
        /// 需要在需要在内存中写入进程应使用WriteProcessMemory
        /// </summary>
        public const int PROCESS_VM_WRITE = 0x0020;
        #endregion

        #region ULW
        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;
        #endregion

        #region AC_SRC
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
        #endregion

        #region ShowWindow Cmd
        /// <summary>
        /// 隐藏窗口并激活其他窗口。
        /// </summary>
        public const int SW_HIDE = 0;
        /// <summary>
        /// 激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。
        /// 应用程序在第一次显示窗口的时候应该指定此标志。
        /// </summary>
        public const int SW_NORMAL = 1;
        /// <summary>
        /// 激活窗口并将其最小化。
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;
        /// <summary>
        /// 激活窗口并将其最大化。
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;
        /// <summary>
        /// 最大化指定的窗口。
        /// </summary>
        public const int SW_MAXIMIZE = 3;
        /// <summary>
        /// 以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;
        /// <summary>
        /// 在窗口原来的位置以原来的尺寸激活和显示窗口。
        /// </summary>
        public const int SW_SHOW = 5;
        /// <summary>
        /// 最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        /// </summary>
        public const int SW_MINIMIZE = 6;
        /// <summary>
        /// 窗口最小化，激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;
        /// <summary>
        /// 以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWNA = 8;
        /// <summary>
        /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。
        /// 在恢复最小化窗口时，应用程序应该指定这个标志。
        /// </summary>
        public const int SW_RESTORE = 9;
        /// <summary>
        /// 依据在 STARTUPINFO 结构中指定的 SW_FLAG 标志设定显示状态，STARTUPINFO 结构是
        /// 由启动应用程序的程序传递给CreateProcess函数的。
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;
        /// <summary>
        /// 在WindowNT5.0 中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程
        /// 最小化窗口时才使用这个参数。
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;
        #endregion
    }
    public static class ClassStyles
    {
        public static readonly Int32
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_SAVEBITS = 0x0800,
        CS_VREDRAW = 0x0001;
    }
}
