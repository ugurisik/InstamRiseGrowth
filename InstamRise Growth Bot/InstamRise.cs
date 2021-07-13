using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstamRise_Growth_Bot.Design;
namespace InstamRise_Growth_Bot
{
    public partial class InstamRise : Form
    {
        public InstamRise()
        {
            InitializeComponent();
            BorderRadius borderRadius = new BorderRadius(this,25);

        }

        private void InstamRise_Load(object sender, EventArgs e)
        {

        }
    }
}
