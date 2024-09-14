using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork db = new UnitOfWork();
            var list= db.CustomerRepository.GetAllCustomers().ToList();
            db.Dispose();
             

        }
    }
}
