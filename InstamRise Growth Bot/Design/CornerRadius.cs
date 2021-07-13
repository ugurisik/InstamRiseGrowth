using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InstamRise_Growth_Bot.Design
{
    class CornerRadius 
    {
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
           (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse
           );
        public CornerRadius(Control component, int radius)
        {
            if (component.GetType().Name == "Button")
            {
                Button btn = (Button)component;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
               
            }
            else if (component.GetType().Name == "TextBox")
            {
                TextBox txt = (TextBox)component;
                txt.BorderStyle = BorderStyle.None;
            }


            this.TargetControl_ = component;
            this.CornerRadius_ = radius;
        }



        private Control _cntrl;
        private int _CornerRadius = 30;

        public Control TargetControl_
        {
            get { return _cntrl; }
            set
            {
                _cntrl = value;
                _cntrl.SizeChanged += (sender, eventArgs) => _cntrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, _cntrl.Width, _cntrl.Height, _CornerRadius, _CornerRadius));
            }
        }

        public int CornerRadius_
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                if (_cntrl != null)
                    _cntrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, _cntrl.Width, _cntrl.Height, _CornerRadius, _CornerRadius));
            }
        }
    }
}
