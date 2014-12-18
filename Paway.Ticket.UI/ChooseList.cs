using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paway.Ticket.Entity;
using System.IO;
using Paway.Ticket.UI.Enums;
using Paway.Ticket.Win32;

namespace Paway.Ticket.UI
{
    public partial class ChooseList : Form
    {
        #region 常量
        public const int MAX_SHOW_COUNT = 6;
        #endregion

        #region 变量
        new private static Control Owner = null;
        private ChooseListItemCollection _items = null;
        private string _searchText = string.Empty;
        private static Dictionary<string, City> _cityList = null;
        private List<City> _lastSearchResult = null;
        private bool _isSearchData = false;
        private static ChooseList Self = null;
        #endregion

        #region 构造函数

        public ChooseList(Control owner)
            : base()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ContainerControl |
                ControlStyles.FixedHeight |
                ControlStyles.FixedWidth |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.UpdateStyles();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        #region 属性
        public static Dictionary<string, City> CityList
        {
            get
            {
                if (_cityList == null)
                {
                    _cityList = new Dictionary<string, City>();
                    string text = null;
                    using (Stream stream = AppResource.GetObject("jsons.citys.txt"))
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            text = sr.ReadToEnd();
                        }
                    }
                    if (!string.IsNullOrEmpty(text))
                    {
                        _cityList = new Dictionary<string, City>();
                        string[] array = text.Split('@');
                        foreach (string item in array)
                        {
                            if (item != string.Empty)
                            {
                                string[] cArray = item.Split('|');
                                string key = cArray[5];
                                if (!_cityList.ContainsKey(key))
                                {
                                    _cityList.Add(key, new City()
                                    {
                                        chineseName = cArray[1],
                                        code = cArray[2],
                                        allPin = cArray[3],
                                        simplePin = cArray[4],
                                        No = cArray[5]
                                    });
                                }
                            }
                        }
                    }
                }
                return _cityList;
            }
        }
        public override Size MinimumSize
        {
            get { return new Size(110, 200); }
        }
        public override Size MaximumSize
        {
            get { return new Size(200, 200); }
        }
        protected override Size DefaultSize
        {
            get { return new Size(200, 200); }
        }
        public ChooseListItemCollection Items
        {
            get
            {
                if (this._items == null)
                    this._items = new ChooseListItemCollection(this);
                return this._items;
            }
        }
        protected virtual Rectangle SearchTextBound
        {
            get
            {
                return new Rectangle(2, 2, this.Width - 4, 20);
            }
        }
        protected virtual Rectangle ButtonBound
        {
            get
            {
                Rectangle bound = Rectangle.Empty;
                if (this.Items.Count > MAX_SHOW_COUNT)
                {
                    bound.X = 2;
                    bound.Y = this.Height - 21;
                    bound.Width = this.Width - 4;
                    bound.Height = 20;
                }
                return bound;
            }
        }
        protected virtual Rectangle ContentBound
        {
            get
            {
                Rectangle bound = new Rectangle();
                bound.X = this.SearchTextBound.X;
                bound.Y = this.SearchTextBound.Bottom;
                bound.Width = this.SearchTextBound.Width;
                bound.Height = this.Height - this.SearchTextBound.Bottom - this.ButtonBound.Height;
                return bound;
            }
        }
        public string SearchText
        {
            set
            {
                if (this._searchText == value)
                    return;
                this._searchText = value;
                this.Invalidate(this.SearchTextBound);
            }
        }
        #endregion

        #region 自定义事件
        public event SelectedItemHandler SelectedItem;
        protected void OnSelectedItem(SelectedItemEventArgs e)
        {
            if (this.SelectedItem != null)
                this.SelectedItem(this, e);
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            #region 绘制 检查栏文字
            if (this._searchText != string.Empty)
            {
                // 按""检索：
                if (this._isSearchData)
                {
                    const string L_TEXT = "按\"";
                    const string R_TEXT = "\"检索";
                    Size l_size = Size.Ceiling(TextRenderer.MeasureText(L_TEXT, this.Font));
                    Size text_size = Size.Ceiling(TextRenderer.MeasureText(this._searchText, this.Font));
                    Size r_size = Size.Ceiling(TextRenderer.MeasureText(R_TEXT, this.Font));
                    TextRenderer.DrawText(
                        g, L_TEXT, this.Font, this.SearchTextBound, Color.FromArgb(132, 132, 132), flags);
                    Rectangle rect = this.SearchTextBound;
                    rect.X += l_size.Width;
                    rect.Width -= l_size.Width;
                    TextRenderer.DrawText(g, this._searchText, this.Font, rect, Color.Red, flags);
                    rect.X += text_size.Width;
                    rect.Width = r_size.Width;
                    TextRenderer.DrawText(g, R_TEXT, this.Font, rect, Color.FromArgb(132, 132, 132), flags);
                }
                else
                {
                    const string NOTIFY_TEXT = "无法匹配：";
                    Size size = Size.Ceiling(TextRenderer.MeasureText(NOTIFY_TEXT, this.Font));
                    TextRenderer.DrawText(
                        g,
                        NOTIFY_TEXT,
                        this.Font,
                        this.SearchTextBound,
                        Color.FromArgb(132, 132, 132),
                        flags);
                    Rectangle rect = this.SearchTextBound;
                    rect.X += (size.Width - 10);
                    rect.Width -= size.Width;
                    TextRenderer.DrawText(g, this._searchText, this.Font, rect, Color.Red, flags);
                }
            }
            // 检索栏下边虚线
            using (Pen pen = new Pen(Color.FromArgb(132, 132, 132)))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawLine(pen,
                    this.SearchTextBound.Left,
                    this.SearchTextBound.Bottom - 1,
                    this.SearchTextBound.Right,
                    this.SearchTextBound.Bottom - 1);
            }
            #endregion

            #region 绘制检索结果
            if (this.Items.Count > 0)
            {
                Font itemFont = new Font("黑体", 10.0f);
                for (int i = 0; i < this.Items.Count; i++)
                {
                    ChooseListItem item = this.Items[i];
                    if (item.MouseState == EMouseState.MouseMove)
                    {
                        using (Brush brush = new SolidBrush(Color.FromArgb(200, 227, 252)))
                        {
                            g.FillRectangle(brush, item.Bounds);
                        }
                        using (Pen pen = new Pen(Color.FromArgb(104, 167, 246)))
                        {
                            g.DrawLine(
                                pen,
                                item.Bounds.X,
                                item.Bounds.Y,
                                item.Bounds.Right - 1,
                                item.Bounds.Y);
                            g.DrawLine(
                                pen,
                                item.Bounds.X,
                                item.Bounds.Bottom - 1,
                                item.Bounds.Right - 1,
                                item.Bounds.Bottom - 1);
                        }
                    }
                    TextRenderer.DrawText(
                        g,
                        this.Items[i].Text,
                        itemFont,
                        item.Bounds,
                        Color.FromArgb(0, 85, 170),
                        flags);
                }
            }
            #endregion
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            Rectangle bounds = this.ClientRectangle;
            bounds.Width--;
            bounds.Height--;
            using (Pen pen = new Pen(Color.FromArgb(127, 157, 185)))
            {
                g.DrawRectangle(pen, bounds);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.ContentBound.Contains(e.Location))
            {
                Point Point = Point.Empty;
                foreach (ChooseListItem item in this.Items)
                {
                    if (item.MouseState != EMouseState.MouseDown)
                    {
                        if (item.Bounds.Contains(e.Location))
                        {
                            item.MouseState = EMouseState.MouseMove;
                        }
                        else
                        {
                            item.MouseState = EMouseState.Normal;
                        }
                    }
                }
                this.Invalidate();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.ContentBound.Contains(e.Location))
            {
                Point point = Point.Empty;
                foreach (ChooseListItem item in this.Items)
                {
                    if (item.Bounds.Contains(e.Location))
                    {
                        item.MouseState = EMouseState.MouseDown;
                    }
                    else
                    {
                        item.MouseState = EMouseState.Normal;
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.ContentBound.Contains(e.Location))
            {
                foreach (ChooseListItem item in this.Items)
                {
                    if (item.Bounds.Contains(e.Location) && item.MouseState == EMouseState.MouseDown)
                    {
                        if (Owner != null && Owner is CityTextBox)
                        {
                            CityTextBox textBox = Owner as CityTextBox;
                            textBox.SetText(item.Text);
                            textBox.Tag = (item.Tag as City).code;
                            textBox.FindForm().Activate();
                            HideSelf();
                        }
                        this.OnSelectedItem(new SelectedItemEventArgs(item.Tag));
                    }
                    item.MouseState = EMouseState.Normal;
                }
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
        #endregion

        #region 方法
        public void Search(string text)
        {
            List<City> list = new List<City>();
            this.SearchText = text;
            this._lastSearchResult = (from item in CityList.Values
                                      where (item.allPin.IndexOf(text) > -1 ||
                                      item.simplePin.IndexOf(text) > -1 ||
                                      item.chineseName.IndexOf(text) > -1)
                                      select item).ToList();
            int count = this._lastSearchResult.Count > MAX_SHOW_COUNT ?
                                            MAX_SHOW_COUNT :
                                            this._lastSearchResult.Count;
            if (count > 0)
            {
                this._isSearchData = true;
                this.Items.Clear();
            }
            else
            {
                this._isSearchData = false;
            }
            for (int i = 0; i < count; i++)
            {
                City city = this._lastSearchResult[i];
                this.Items.Add(new ChooseListItem()
                {
                    Text = city.chineseName,
                    Tag = city,
                    Bounds = new Rectangle(2, (i + 1) * (this.SearchTextBound.Bottom + 2), this.Width - 4, 24)
                });
            }
            this.Invalidate(this.ContentBound);
        }
        #endregion

        #region Static
        public static bool IsShow
        {
            get
            {
                return (Self != null && Self.Visible);
            }
        }
        public static Rectangle Bound
        {
            get
            {
                return Self == null ? Rectangle.Empty : Self.Bounds ;
            }
        }
        public static void Show(Control control, string text)
        {
            if (text == string.Empty)
            {
                HideSelf();
            }
            else
            {
                if (Self == null)
                {
                    Self = new ChooseList(control);
                    control.GotFocus -= new EventHandler(control_GotFocus);
                    control.GotFocus += new EventHandler(control_GotFocus);
                    control.LostFocus -= new EventHandler(control_LostFocus);
                    control.LostFocus += new EventHandler(control_LostFocus);
                    Self.ShowIcon = false;
                    Self.ShowInTaskbar = false;
                    Self.TopMost = true;
                    Self.Visible = false;
                }
                Owner = control;
                Self.Size = new Size(control.Width, 200);
                if (!Self.Visible)
                    Self.Show();
                Point point = control.PointToScreen(Point.Empty);
                Self.Location = new Point(point.X, point.Y + control.Height);
                Self.Search(text);
            }
            Form form = control.FindForm();
            if (form != null)
            {
                form.Activate();
                control.Select();
            }
        }
        public static void HideSelf()
        {
            if (Self != null && !Self.Focused && Self.Visible)
            {
                Self.Hide();
            }
        }
        static void control_LostFocus(object sender, EventArgs e)
        {
            if (Self != null)
            {
                if (!Self.Focused && !(sender as Control).Focused)
                {
                    Self.Hide();
                }
            }
        }
        static void control_GotFocus(object sender, EventArgs e)
        {
            //if (Self != null)
            //{
            //    Control owner = sender as Control;
            //    ChooseList.Show(owner, owner.Text);
            //}
        }
        #endregion
    }
    public delegate void SelectedItemHandler(object sender, SelectedItemEventArgs e);
    public class SelectedItemEventArgs : EventArgs
    {
        private object _value;

        public SelectedItemEventArgs() : base() { }
        public SelectedItemEventArgs(object value)
            : this()
        {
            this._value = value;
        }

        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}
