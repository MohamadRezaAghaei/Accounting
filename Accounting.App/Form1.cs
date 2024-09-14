using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.Business;
using Accounting.Utility.Convertor;
using Accounting_ViewModels.Accounting;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Hide();
            frmLogin frmLogin=new frmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.ToShamsi();
                lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            }
            else
            {
                Application.Exit();
            }
            report();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            
            frmNewAccounting frmNewAccounting= new frmNewAccounting();
            frmNewAccounting.ShowDialog();

        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void btnReportReceive_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.IsEdit=true;
            frmLogin.ShowDialog();
        }

        private void lblReceive_Click(object sender, EventArgs e)
        {
           
        }

        void report()
        {
            ReportViewModel report = Account.ReportFormMain();
            lblReceive.Text = report.Receive.ToString("#,0");
            lblPay.Text = report.Pay.ToString("#,0");
            lblAccountBalance.Text = report.AccountBalance.ToString("#,0");
        }
    }
}
