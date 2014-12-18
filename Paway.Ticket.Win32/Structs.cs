using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Paway.Ticket.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HDITEM
    {
        public int mask;
        public int cxy;
        public string pszText;
        public IntPtr hbm;
        public int cchTextMax;
        public int fmt;
        public IntPtr lParam;
        public int iImage;
        public int iOrder;
        public uint type;
        public IntPtr pvFilter;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle rect)
        {
            Left = rect.Left;
            Top = rect.Top;
            Right = rect.Right;
            Bottom = rect.Bottom;
        }

        public Rectangle Rect
        {
            get { return new Rectangle(Left, Top, Right - Left, Bottom - Top); }
        }

        public Size Size
        {
            get { return new Size(Right - Left, Bottom - Top); }
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x, y, x + width, y + height);
        }

        public static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct API_MSG
    {
        public IntPtr Hwnd;
        public int Msg;
        public IntPtr WParam;
        public IntPtr LParam;
        public int Time;
        public POINT Pt;
        public Message ToMessage()
        {
            Message message = new Message();
            message.HWnd = this.Hwnd;
            message.Msg = this.Msg;
            message.WParam = this.WParam;
            message.LParam = this.LParam;
            return message;
        }

        public void FromMessage(ref Message msg)
        {
            this.Hwnd = msg.HWnd;
            this.Msg = msg.Msg;
            this.WParam = msg.WParam;
            this.LParam = msg.LParam;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
        public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
        {
            this.BlendOp = op;
            this.BlendFlags = flags;
            this.SourceConstantAlpha = alpha;
            this.AlphaFormat = format;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        internal IntPtr hWnd;
        internal IntPtr hWndInsertAfter;
        internal int x;
        internal int y;
        internal int cx;
        internal int cy;
        internal int flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public RECT rcNormalPosition;
        public static WINDOWPLACEMENT Default
        {
            get
            {
                WINDOWPLACEMENT structure = new WINDOWPLACEMENT();
                structure.length = Marshal.SizeOf(structure);
                return structure;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        internal uint cbSize;
        internal RECT rcWindow;
        internal RECT rcClient;
        internal uint dwStyle;
        internal uint dwExStyle;
        internal uint dwWindowStatus;
        internal uint cxWindowBorders;
        internal uint cyWindowBorders;
        internal IntPtr atomWindowType;
        internal ushort wCreatorVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TT_HITTESTINFO
    {
        internal IntPtr hwnd;
        internal POINT pt;
        internal TOOLINFO ti;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TRACKMOUSEEVENT
    {
        internal uint cbSize;
        internal uint dwFlags;
        internal IntPtr hwndTrack;
        internal uint dwHoverTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TOOLINFO
    {
        public int cbSize;
        public int uFlags;
        public IntPtr hwnd;
        public IntPtr uId;
        public RECT rect;
        public IntPtr hinst;
        public IntPtr lpszText;
        public IntPtr lParam;
        internal TOOLINFO(int flags)
        {
            this.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            this.uFlags = flags;
            this.hwnd = IntPtr.Zero;
            this.uId = IntPtr.Zero;
            this.rect = new RECT(0, 0, 0, 0);
            this.hinst = IntPtr.Zero;
            this.lpszText = IntPtr.Zero;
            this.lParam = IntPtr.Zero;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TCHITTESTINFO
    {
        public System.Drawing.Point Point;
        public int Flags;
        public TCHITTESTINFO(System.Drawing.Point location)
        {
            this.Point = location;
            this.Flags = 6;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STYLESTRUCT
    {
        internal int styleOld;
        internal int styleNew;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCROLLINFO
    {
        internal uint cbSize;
        internal uint fMask;
        internal int nMin;
        internal int nMax;
        internal uint nPage;
        internal int nPos;
        internal int nTrackPos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCROLLBARINFO
    {
        public int cbSize;
        public RECT rcScrollBar;
        public int dxyLineButton;
        public int xyThumbTop;
        public int xyThumbBottom;
        public int reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        internal int[] rgstate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public RECT rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTDISPINFO
    {
        internal NMHDR hdr;
        internal IntPtr lpszText;
        internal IntPtr szText;
        internal IntPtr hinst;
        internal int uFlags;
        internal IntPtr lParam;
        internal NMTTDISPINFO(int flags)
        {
            this.hdr = new NMHDR(0);
            this.lpszText = IntPtr.Zero;
            this.szText = IntPtr.Zero;
            this.hinst = IntPtr.Zero;
            this.uFlags = 0;
            this.lParam = IntPtr.Zero;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTCUSTOMDRAW
    {
        internal NMCUSTOMDRAW nmcd;
        internal uint uDrawFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        internal IntPtr hwndFrom;
        internal int idFrom;
        internal int code;
        internal NMHDR(int flag)
        {
            this.hwndFrom = IntPtr.Zero;
            this.idFrom = 0;
            this.code = 0;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMCUSTOMDRAW
    {
        internal NMHDR hdr;
        internal uint dwDrawStage;
        internal IntPtr hdc;
        internal RECT rc;
        internal IntPtr dwItemSpec;
        internal uint uItemState;
        internal IntPtr lItemlParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NCCALCSIZE_PARAMS
    {
        internal RECT rgrc0;
        internal RECT rgrc1;
        internal RECT rgrc2;
        internal IntPtr lppos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MOUSEHOOKSTRUCTEX
    {
        public MOUSEHOOKSTRUCT Mouse;
        public int mouseData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCT
    {
        public POINT Pt;
        public IntPtr hwnd;
        public uint wHitTestCode;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public Point reserved;
        public Size maxSize;
        public Point maxPosition;
        public Size minTrackSize;
        public Size maxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INITCOMMONCONTROLSEX
    {
        public int dwSize;
        public int dwICC;
        public INITCOMMONCONTROLSEX(int flags)
        {
            this.dwSize = Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX));
            this.dwICC = flags;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class CWPSTRUCT
    {
        public IntPtr lParam;
        public IntPtr wParam;
        public int message;
        public IntPtr hwnd;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CWPRETSTRUCT
    {
        public IntPtr lResult;
        public IntPtr lParam;
        public IntPtr wParam;
        public int message;
        public IntPtr hwnd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ComboBoxInfo
    {
        public int cbSize;
        public RECT rcItem;
        public RECT rcButton;
        public ComboBoxButtonState stateButton;
        public IntPtr hwndCombo;
        public IntPtr hwndEdit;
        public IntPtr hwndList;
    }

    /// <summary>
    /// 鼠标钩子事件结构定义
    /// </summary>
    /// <remarks>详细说明请参考MSDN中关于 MSLLHOOKSTRUCT 的说明</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseHookStruct
    {
        /// <summary>
        /// Specifies a POINT structure that contains the x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public POINT Point;

        public UInt32 MouseDate;
        public UInt32 Flags;
        public UInt32 Time;
        public UInt32 ExtraInfo;
    }
    /// <summary>
    /// 存储一个有序整数对，通常为矩形的宽度和高度。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        #region 变量

        /// <summary>
        /// 获取或设置此 SIZE 的水平分量。
        /// </summary>
        public int Width;
        /// <summary>
        /// 获取或设置此 SIZE 的垂直分量。
        /// </summary>
        public int Height;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化 Paway.Windows.Win32.SIZE 结构的新实例。
        /// </summary>
        /// <param name="width">此 SIZE 的水平分量</param>
        /// <param name="height">此 SIZE 的垂直分量。</param>
        public SIZE(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        #endregion
    }
}
