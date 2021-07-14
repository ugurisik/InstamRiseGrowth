using InstamRise_Growth_Bot.Design;
using InstamRise_Growth_Bot.UserInterfaces;
using System;
using System.Windows.Forms;
using InstagramApiDataProcess.DataProcess;
using InstagramApiDataProcess.Models;

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

        private async void InstamRise_Load(object sender, EventArgs e)
        {
            //InstaProfile i = await ProfileDataProcess.ProfileInfo(firstApi, "ali");  
            //if(i.ExceptionStatus == null)
            //{
                
            //}
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            new ChangeContainer(settingsPage, pnlContainer);
        }

        private void btnFollowPage_Click(object sender, EventArgs e)
        {
            FollowPage followPage = new FollowPage();
            new ChangeContainer(followPage, pnlContainer);
        }

        private void btnListPage_Click(object sender, EventArgs e)
        {
            ListPage listPage = new ListPage();
            new ChangeContainer(listPage, pnlContainer);
        }

        private void btnSharePage_Click(object sender, EventArgs e)
        {
            MediaSharePage mediaSharePage = new MediaSharePage();
            new ChangeContainer(mediaSharePage, pnlContainer);
        }

        private void btnDownloadPage_Click(object sender, EventArgs e)
        {
            DownloadPage downloadPage = new DownloadPage();
            new ChangeContainer(downloadPage, pnlContainer);
        }
    }
}
