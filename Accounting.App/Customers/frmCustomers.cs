using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.App.Customers;
using Accounting.DataLayer.Context;

namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        void BindGrid()
        {
            using (UnitOfWork db= new UnitOfWork())
            {
                dgCustomers.AutoGenerateColumns = false;
                dgCustomers.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtFilter.Text = "";
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db=new UnitOfWork())
            {
                dgCustomers.DataSource= db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);
            }
        }

        private void btnٍDeleteCustomer_Click(object sender, EventArgs e)
        {
            
            if (dgCustomers.CurrentRow != null)
            {
                using (UnitOfWork db=new UnitOfWork())
                {
                    string name= dgCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"آیا از حذف {name} مطمئن هستید؟", "توجه", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) ==
                        DialogResult.Yes)
                    {
                        int CustomerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(CustomerId);
                        db.Save();
                    }
                    
                    BindGrid();
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید");
            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEditCustomer frmAdd = new frmAddOrEditCustomer();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
            
        }

        private void btnٍEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgCustomers.CurrentRow != null)
            {
                int customerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEditCustomer frmEdit=new frmAddOrEditCustomer();
                frmEdit.customerId= customerId;
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }


        }
    }
}
