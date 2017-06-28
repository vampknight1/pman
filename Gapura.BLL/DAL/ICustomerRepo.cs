using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gapura.BLL.Models;

namespace Gapura.BLL.DAL
{
    public interface ICustomerRepo : IDisposable
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(string customerId);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string customerId);
        void Save();
    }
}
