using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Repositories;
using Accounting_ViewModels.Customers;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities db;

        public CustomerRepository(Accounting_DBEntities context)
        {
            this.db = context;
        }
        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public List<ListCustomerViewModel> GetNameCustomers(string filter = "")
        {
            if (filter == "")
            {
                return db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName
                }).ToList();
            }

            return db.Customers.Where(b => b.FullName.Contains(filter)).Select(d => new ListCustomerViewModel()
            {
                CustomerID = d.CustomerID,
                FullName = d.FullName
            }).ToList().ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCustomer(Customers customer)
        {
            var local = db.Set<Customers>()
                .Local
                .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(customer).State = EntityState.Modified;
            return true;

        }

        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCustomerIdByName(string name)
        {
            return db.Customers.First(c => c.FullName == name).CustomerID;
        }

        public string GetCustomerNameByID(int customerId)
        {
            return db.Customers.Find(customerId).FullName;
        }


        public IEnumerable<Customers> GetCustomersByFilter(string parameters)
        {
            return db.Customers.Where(c =>
               c.FullName.Contains(parameters) || c.Mobile.Contains(parameters) || c.Email.Contains(parameters)).ToList();


        }
    }
}
