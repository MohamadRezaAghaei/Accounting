using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;
using ValidationComponents;
using Accounting.DataLayer;
 

namespace Accounting.App.Customers
{
    public partial class frmAddOrEditCustomer : Form
    {
        public int customerId = 0;
        private UnitOfWork db = new UnitOfWork();
        public frmAddOrEditCustomer()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation=openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
            string path = Application.StartupPath + "/Images/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            pcCustomer.Image.Save(path+ImageName);
            if (BaseValidator.IsFormValid(this.components))
            {
                DataLayer.Customers customers = new DataLayer.Customers()
                {
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    CustomerImage = ImageName
                };
                if (customerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(customers);
                }
                else
                {
                    customers.CustomerID=customerId;
                    db.CustomerRepository.UpdateCustomer(customers);
                }
                
                db.Save();
                DialogResult= DialogResult.OK;
                
            }
        }

        private void frmAddOrEditCustomer_Load(object sender, EventArgs e)
        {
            if (customerId != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomerById(customerId);
                txtName.Text = customer.FullName;
                txtMobile.Text = customer.Mobile;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
            }

            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
