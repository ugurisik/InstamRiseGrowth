using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstamRise_Growth_Bot.Design
{
    class ChangeContainer
    {
        public ChangeContainer(UserControl control,Panel Container) {
            Container.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            Container.Controls.Add(control);
        }
    }
}
