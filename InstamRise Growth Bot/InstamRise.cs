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
using InstamRise_Growth_Bot.UserInterfaces;
namespace InstamRise_Growth_Bot
{
    public partial class InstamRise : Form
    {
        public InstamRise()
        {
            InitializeComponent();
            new CornerRadius(this, 30);
            new FormMove(pnlLeft, this);
            new FormMove(pnlTop, this);
            new FormMove(pnlContainer, this);
            new ShadowForm().ApplyShadows(this);
            
        }
     
        private void InstamRise_Load(object sender, EventArgs e)
        {

        }

        private void btnHomePage_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            new ChangeContainer(homePage, pnlContainer);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccountPage_Click(object sender, EventArgs e)
        {
            AccountPage accountPage = new AccountPage();
            new ChangeContainer(accountPage, pnlContainer);
        }
    }
}
