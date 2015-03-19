using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;

namespace Windows.Forms.Controls.Metros
{

    [Designer(typeof(ScrollbarControlDesigner))]
    public class MyBookScrollbar : UserControl {
        //���������������
        private Rectangle bounds;
        public new Rectangle Bounds {
            get { return bounds; }
        }
        //�ϱ߼�ͷ����
        private Rectangle upBounds;
        public Rectangle UpBounds {
            get { return upBounds; }
        }
        //�±߼�ͷ����
        private Rectangle downBounds;
        public Rectangle DownBounds {
            get { return downBounds; }
        }
        //��������
        private Rectangle sliderBounds;
        public Rectangle SliderBounds {
            get { return sliderBounds; }
        }

        private Color backColor;
        public override Color BackColor {
            get { return backColor; }
            set { backColor = value; }
        }

        private Color sliderDefaultColor;
        public Color SliderDefaultColor {
            get { return sliderDefaultColor; }
            set {
                if (sliderDefaultColor == value)
                    return;
                sliderDefaultColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color sliderDownColor;
        public Color SliderDownColor {
            get { return sliderDownColor; }
            set {
                if (sliderDownColor == value)
                    return;
                sliderDownColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color arrowBackColor;
        public Color ArrowBackColor {
            get { return arrowBackColor; }
            set {
                if (arrowBackColor == value)
                    return;
                arrowBackColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }

        private Color arrowColor;
        public Color ArrowColor {
            get { return arrowColor; }
            set {
                if (arrowColor == value)
                    return;
                arrowColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }
        //�󶨵Ŀؼ�
        private Control ctrl;
        public Control Ctrl {
            get { return ctrl; }
            set { ctrl = value; }
        }
        //�����һ���߶�(�ؼ������ݵĸ߶�)
        private int virtualHeight;
        public int VirtualHeight {
            get { return virtualHeight; }
            set {
                if (value <= ctrl.Height) {
                    if (shouldBeDraw == false)
                        return;
                    shouldBeDraw = false;
                    if (this.value != 0) {
                        this.value = 0;
                        ctrl.Invalidate();
                    }
                } else {
                    shouldBeDraw = true;
                    if (value - this.value < ctrl.Height) {
                        this.value -= ctrl.Height - value + this.value;
                        ctrl.Invalidate();
                    }
                }
                virtualHeight = value;
            }
        }
        //��ǰ������λ��
        private int value;
        public int Value {
            get { return value; }
            set {
                if (!shouldBeDraw)
                    return;
                if (value < 0) {
                    if (this.value == 0)
                        return;
                    this.value = 0;
                    ctrl.Invalidate();
                    return;
                }
                if (value > virtualHeight - ctrl.Height) {
                    if (this.value == virtualHeight - ctrl.Height)
                        return;
                    this.value = virtualHeight - ctrl.Height;
                    ctrl.Invalidate();
                    return;
                }
                this.value = value;
                ctrl.Invalidate();
            }
        }
        //�Ƿ��б�Ҫ�ڿؼ��ϻ��ƹ�����
        private bool shouldBeDraw;
        public bool ShouldBeDraw {
            get { return shouldBeDraw; }
        }

        private bool isMouseDown;
        public bool IsMouseDown {
            get { return isMouseDown; }
            set {
                if (value) {
                    m_nLastSliderY = sliderBounds.Y;
                }
                isMouseDown = value;
            }
        }

        private bool isMouseOnSlider;
        public bool IsMouseOnSlider {
            get { return isMouseOnSlider; }
            set {
                if (isMouseOnSlider == value)
                    return;
                isMouseOnSlider = value;
                ctrl.Invalidate(this.SliderBounds);
            }
        }

        private bool isMouseOnUp;
        public bool IsMouseOnUp {
            get { return isMouseOnUp; }
            set {
                if (isMouseOnUp == value)
                    return;
                isMouseOnUp = value;
                ctrl.Invalidate(this.UpBounds);
            }
        }

        private bool isMouseOnDown;
        public bool IsMouseOnDown {
            get { return isMouseOnDown; }
            set {
                if (isMouseOnDown == value)
                    return;
                isMouseOnDown = value;
                ctrl.Invalidate(this.DownBounds);
            }
        }
        //����ڻ������ʱ���y����
        private int mouseDownY;
        public int MouseDownY {
            get { return mouseDownY; }
            set { mouseDownY = value; }
        }
        //�����ƶ�ǰ�� �����y����
        private int m_nLastSliderY;

        public MyBookScrollbar(Control c)
        {
            this.ctrl = c;
            virtualHeight = 400;
            bounds = new Rectangle(0, 0, 10, 10);
            upBounds = new Rectangle(0, 0, 10, 10);
            downBounds = new Rectangle(0, 0, 10, 10);
            sliderBounds = new Rectangle(0, 0, 10, 10);
            this.backColor = Color.Transparent;
            this.sliderDefaultColor = Color.LightGray;
            this.sliderDownColor = Color.Gainsboro;
            this.arrowBackColor = Color.Silver;
            this.arrowColor = Color.White;
        }

        public void ClearAllMouseOn() {
            if (!this.isMouseOnDown && !this.isMouseOnSlider && !this.isMouseOnUp)
                return;
            this.isMouseOnDown =
                this.isMouseOnSlider =
                this.isMouseOnUp = false;
            ctrl.Invalidate(this.bounds);
        }
        //������������һ���ط�
        public void MoveSliderToLocation(int nCurrentMouseY) {
            if (nCurrentMouseY - sliderBounds.Height / 2 < 11)
                sliderBounds.Y = 11;
            else if (nCurrentMouseY + sliderBounds.Height / 2 > ctrl.Height - 11)
                sliderBounds.Y = ctrl.Height - sliderBounds.Height - 11;
            else
                sliderBounds.Y = nCurrentMouseY - sliderBounds.Height / 2;
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));
            ctrl.Invalidate();
        }
        //�������λ���ƶ�����
        public void MoveSliderFromLocation(int nCurrentMouseY) {
            //if (!this.IsMouseDown) return;
            if (m_nLastSliderY + nCurrentMouseY - mouseDownY < 11) {
                if (sliderBounds.Y == 11)
                    return;
                sliderBounds.Y = 11;
            } else if (m_nLastSliderY + nCurrentMouseY - mouseDownY > ctrl.Height - 11 - SliderBounds.Height) {
                if (sliderBounds.Y == ctrl.Height - 11 - sliderBounds.Height)
                    return;
                sliderBounds.Y = ctrl.Height - 11 - sliderBounds.Height;
            } else {
                sliderBounds.Y = m_nLastSliderY + nCurrentMouseY - mouseDownY;
            }
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));
            ctrl.Invalidate();
        }
        //���ƹ�����
        public void ReDrawScroll(Graphics g) {
            if (!shouldBeDraw)
                return;
            bounds.X = 0;
            bounds.Height = ctrl.Height;
            upBounds.X = downBounds.X = bounds.X;
            downBounds.Y = ctrl.Height - 10;
            //���㻬��λ��
            sliderBounds.X = bounds.X;
            sliderBounds.Height = (int)(((double)ctrl.Height / virtualHeight) * (ctrl.Height - 22));
            if (sliderBounds.Height < 3) sliderBounds.Height = 3;
            sliderBounds.Y = 11 + (int)(((double)value / (virtualHeight - ctrl.Height)) * (ctrl.Height - 22 - sliderBounds.Height));
            SolidBrush sb = new SolidBrush(this.backColor);
            try {
                g.FillRectangle(sb, bounds);
                sb.Color = this.arrowBackColor;
                g.FillRectangle(sb, upBounds);
                g.FillRectangle(sb, downBounds);
                if (this.isMouseDown || this.isMouseOnSlider)
                    sb.Color = this.sliderDownColor;
                else
                    sb.Color = this.sliderDefaultColor;
                if (sliderBounds.Height<8)
                {
                    sliderBounds.Height = 8;
                }
                g.FillRectangle(sb, sliderBounds);
                sb.Color = this.arrowColor;
                if (this.isMouseOnUp)
                    g.FillPolygon(sb, new Point[]{
                    new Point(ctrl.Width - 5,3),
                    new Point(ctrl.Width - 9,7),
                    new Point(ctrl.Width - 2,7)
                });
                if (this.isMouseOnDown)
                    g.FillPolygon(sb, new Point[]{
                    new Point(ctrl.Width - 5,ctrl.Height - 4),
                    new Point(ctrl.Width - 8,ctrl.Height - 7),
                    new Point(ctrl.Width - 2,ctrl.Height - 7)
                });
            } finally {
                sb.Dispose();
            }
        }
    }

    internal class ScrollbarControlDesigner : System.Windows.Forms.Design.ControlDesigner {

        

        public override SelectionRules SelectionRules {
            get {
                SelectionRules selectionRules = base.SelectionRules;
                PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(this.Component)["AutoSize"];
                if (propDescriptor != null) {
                    bool autoSize = (bool)propDescriptor.GetValue(this.Component);
                    if (autoSize) {
                        selectionRules = SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.TopSizeable;
                    }
                    else {
                        selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
                    }
                }
                return selectionRules;
            }
        } 
    }
}