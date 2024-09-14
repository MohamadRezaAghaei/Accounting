using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Context;
using Accounting_ViewModels.Accounting;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportFormMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,01);
                DateTime EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31);
                var receive= db.AccountingRepository.Get(c =>c.TypeID==1 && c.DateTime >= StartDate && c.DateTime <= EndDate).Select(a=>a.Amount).ToList();
                var pay = db.AccountingRepository
                    .Get(d => d.TypeID == 2 && d.DateTime >= StartDate && d.DateTime <= EndDate).Select(a=>a.Amount).ToList();

                rp.Receive = receive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountBalance = (receive.Sum() - pay.Sum());
            }
            return rp;
        }
    }
}
