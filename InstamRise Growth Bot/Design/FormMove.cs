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
        public FormMove(Control component) {
            this.Component = component;
        }
        public void Move() {
            Component.MouseMove += (sender, eventArgs) =>
            {
                if (dragging)
                {
                    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    Component.Location = Point.Add(dragFormPoint, new Size(dif));
                }
            };
        }
        public void Down()
        {
            Component.MouseDown += (sender, eventArgs) => {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = Component.Location;
            };
        }
        public void Up() {
            Component.MouseUp += (sender, eventArgs) =>
            {
                dragging = false;
            };
        }

    }
}
