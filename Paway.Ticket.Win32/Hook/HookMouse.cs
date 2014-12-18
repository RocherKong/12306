using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace Paway.Ticket.Win32
{
    public class HookMouse
    {
        #region 私有变量
        /// <summary>
        /// 鼠标钩子句柄
        /// </summary>
        private IntPtr _pMouseHook = IntPtr.Zero;
        /// <summary>
        /// 鼠标钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量，否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手(MDA）。
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private HookProc _mouseHookProcedure;

        #endregion

        #region 构造函数
        public HookMouse()
        { 
        }
        #endregion

        #region 事件定义
        /// <summary>
        /// 鼠标事件
        /// </summary>
        public event MouseEventHandler OnMouseActivity;
        #endregion

        #region 私有方法

        /// <summary>
        /// 鼠标钩子处理函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (this.OnMouseActivity != null))
            {
                //Marshall the date from callback.
                MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                //detect button clicked
                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                switch (wParam)
                {
                    case (int)WM_MOUSE.WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        break;
                    case (int)WM_MOUSE.WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        break;
                    case (int)WM_MOUSE.WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.MouseDate >> 16) & 0xffff);
                        break;
                }
                //double click
                int clickCount = 0;
                if (button != MouseButtons.None)
                {
                    if (wParam == (int)WM_MOUSE.WM_LBUTTONDBLCLK || wParam == (int)WM_MOUSE.WM_RBUTTONDBLCLK)
                        clickCount = 2;
                    else
                        clickCount = 1;
                }
                //generate event
                MouseEventArgs e = new MouseEventArgs(button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y, mouseDelta);

                //raise it
                this.OnMouseActivity(this, e);
            }
            return NativeMethods.CallNextHookEx(this._pMouseHook, nCode, wParam, lParam);
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <returns></returns>
        public bool InstallHook()
        {
            //Assembly.GetExecutingAssembly()
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetEntryAssembly().ManifestModule);
            //假如没有安装鼠标钩子
            if (this._pMouseHook == IntPtr.Zero)
            {
                this._mouseHookProcedure = new HookProc(this.MouseHookProc);
                this._pMouseHook = NativeMethods.SetWindowsHookEx(WM_Codes.WH_MOUSE_LL, this._mouseHookProcedure, IntPtr.Zero, 0);
                if (this._pMouseHook == IntPtr.Zero)
                {
                    this.UnInstallHook();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <returns></returns>
        public bool UnInstallHook()
        {
            bool result = true;
            if (this._pMouseHook != IntPtr.Zero)
            {
                result = (NativeMethods.UnhookWindowsHookEx(this._pMouseHook) && result);
                this._pMouseHook = IntPtr.Zero;
            }
            return result;
        }
        #endregion
    }
}
