using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstamRise_Growth_Bot.Design
{
    class FormMove
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public Control Component { get; set; }
        public FormMove(Control component,Control form) {
            this.Component = component;
            Component.MouseMove += (sender, eventArgs) =>
            {
                if (dragging)
                {
                    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    form.Location = Point.Add(dragFormPoint, new Size(dif));
                }
            };
            Component.MouseDown += (sender, eventArgs) => {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = form.Location;
            };
            Component.MouseUp += (sender, eventArgs) =>
            {
                dragging = false;
            };
        }
    
    }
}
