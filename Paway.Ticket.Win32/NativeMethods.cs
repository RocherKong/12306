using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Paway.Ticket.Win32
{
    /// <summary>
    /// Windows API，不能在本类中直接定义C#方法，
    /// 需要时可以在向Win32Helper.cs、IconsHelper等文件中调用该类中API方法，
    /// 或者直接调用本类中的DllImport方法（但不建议）
    /// </summary>
    public class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_T
        {
            public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public RECT rect;
            public IntPtr hinst = IntPtr.Zero;
            public string lpszText;
            public IntPtr lParam = IntPtr.Zero;
        }
        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, int hMod, int dwThreadId);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetWindowsHookEx(WM_Codes idHook, HookProc lpfn, IntPtr pInstance, int threadId);

        /// <summary>
        /// 设置窗口的区域的窗口。窗口区域决定在窗户上的地区——该系统允许绘画。该系统不显示任何部分是一个窗口,窗户外面地区
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hRgn">处理区域</param>
        /// <param name="bRedraw">重绘窗体选项</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 创建一个圆角矩形区域
        /// </summary>
        /// <param name="nLeftRect">x坐标左上角</param>
        /// <param name="nTopRect">y坐标左上角</param>
        /// <param name="nRightRect">x坐标右上角</param>
        /// <param name="nBottomRect">y坐标右上角</param>
        /// <param name="nWidthEllipse">椭圆的宽度</param>
        /// <param name="nHeightEllipse">椭圆的高度</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int up, int lp);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, ref HDITEM lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, ref RECT lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, int flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool OffsetRect(ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT rect);

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BLENDFUNCTION blendFunction);
        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        /// <summary>
        /// 传递钩子
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(IntPtr pHookHandle, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int CombineRgn(IntPtr hRgn, IntPtr hRgn1, IntPtr hRgn2, int nCombineMode);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, int lpInitData);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDCA([MarshalAs(UnmanagedType.LPStr)] string lpszDriver, [MarshalAs(UnmanagedType.LPStr)] string lpszDevice, [MarshalAs(UnmanagedType.LPStr)] string lpszOutput, int lpInitData);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDCW([MarshalAs(UnmanagedType.LPWStr)] string lpszDriver, [MarshalAs(UnmanagedType.LPWStr)] string lpszDevice, [MarshalAs(UnmanagedType.LPWStr)] string lpszOutput, int lpInitData);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(int exstyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hwndParent, IntPtr Menu, IntPtr hInstance, IntPtr lpParam);
        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern void DisableProcessWindowsGhosting();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
        [DllImport("user32.dll")]
        public static extern bool EnableScrollBar(IntPtr hWnd, int wSBflags, int wArrows);
        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("user32.dll")]
        public static extern bool EqualRect([In] ref RECT lprc1, [In] ref RECT lprc2);
        [DllImport("gdi32.dll")]
        public static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int FindExecutable(string filename, string directory, ref string result);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetClassLongPtr64(hWnd, nIndex);
            }
            return GetClassLongPtr32(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern IntPtr GetClassLongPtr32(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetCurrentThreadId();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("USER32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int GetScrollBarInfo(IntPtr hWnd, uint idObject, ref SCROLLBARINFO psbi);
        [DllImport("user32.dll")]
        public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO scrollInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("comctl32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool InitCommonControlsEx(ref INITCOMMONCONTROLSEX iccex);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, ref RECT rect, bool erase);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("uxtheme.dll")]
        public static extern bool IsAppThemed();
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool KillTimer(IntPtr hWnd, uint uIDEvent);
        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);
        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(int uType);
        [DllImport("kernel32.dll")]
        public static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool PtInRect(ref RECT lprc, Point pt);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, ref RECT rectUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(IntPtr destination, ref NMTTDISPINFO Source, int length);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(ref NMCUSTOMDRAW destination, IntPtr Source, int length);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(ref NMHDR destination, IntPtr source, int length);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(ref NMTTCUSTOMDRAW destination, IntPtr Source, int length);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(ref NMTTDISPINFO destination, IntPtr source, int length);
        [DllImport("kernel32.dll")]
        public static extern int RtlMoveMemory(ref POINT destination, ref RECT Source, int length);
        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPTStr)] string lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMHDR lParam);
        [DllImport("user32.dll")]
        public static extern int SetFocus(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, int nIDEvent, uint uElapse, IntPtr lpTimerFunc);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int cx, int cy, uint flags);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hDest, int X, int Y, int nWidth, int nHeight, IntPtr hdcSrc, int sX, int sY, int nWidthSrc, int nHeightSrc, int dwRop);
        [DllImport("user32.dll")]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);
        [DllImport("user32.dll")]
        public static extern IntPtr TrackPopupMenu(IntPtr hMenu, int uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr par);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool TrackPopupMenuEx(IntPtr hMenu, uint uFlags, int x, int y, IntPtr hWnd, IntPtr tpmParams);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);
        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string lpCmdLine, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);
        [DllImport("USER32.DLL", EntryPoint = "GetCaretBlinkTime")]
        public static extern uint GetCaretBlinkTime();
        #region Other Methods
        /// <summary>
        /// 创建一个无符号的32位值作为lParam参数中使用一个消息
        /// </summary>
        public static int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int HIWORD(int value)
        {
            return value >> 16;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hdcDst"></param>
        /// <param name="pptDst"></param>
        /// <param name="psize"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="ppSrc"></param>
        /// <param name="crKey"></param>
        /// <param name="pblend"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT ppSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
    }
}
