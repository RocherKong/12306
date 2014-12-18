using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Paway.Ticket.UI
{
    [ToolboxItem(false)]
    public class BaseStyledPanel : ContainerControl
    {
        #region 变量
        private static ToolStripProfessionalRenderer _renderer;
        #endregion

        #region 自定义事件
        public event EventHandler ThemeChanged;

        protected virtual void OnThemeChanged(EventArgs e)
        {
            if (this.ThemeChanged != null)
                this.ThemeChanged(this, e);
        }
        #endregion

        #region 构造函数
        static BaseStyledPanel()
        {
            _renderer = new ToolStripProfessionalRenderer();
        }

        public BaseStyledPanel()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
        }
        #endregion

        #region 属性
        [Browsable(false)]
        public ToolStripProfessionalRenderer ToolStripRenderer
        {
            get { return _renderer; }
        }
        [Browsable(false)]
        public bool UseThemes
        {
            get { return VisualStyleRenderer.IsSupported && VisualStyleInformation.IsSupportedByOS && Application.RenderWithVisualStyles; }
        }
        #endregion

        #region Override Methods
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            this.UpdateRenderer();
            this.Invalidate();
        }

        #endregion

        #region 方法
        private void UpdateRenderer()
        {
            if (!UseThemes)
                _renderer.ColorTable.UseSystemColors = true;
            else
                _renderer.ColorTable.UseSystemColors = false;
        }
        #endregion
    }
}
