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
            CornerRadius borderRadius = new CornerRadius(this, 30);
            FormMove formMove = new FormMove(this);
            formMove.Down();
            formMove.Move();
            formMove.Up();
        }
      
        private void InstamRise_Load(object sender, EventArgs e)
        {

        }
    }
}
