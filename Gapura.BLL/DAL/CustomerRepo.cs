using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gapura.BLL.Models;

namespace Gapura.BLL.DAL
{
    public class CustomerRepo : ICustomerRepo, IDisposable
    {
        private YSIDGAEntitiesConn _dbConn = null;

        public CustomerRepo(YSIDGAEntitiesConn _dbConn)
        {
            this._dbConn = _dbConn;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _dbConn.Customers.ToList();
        }

        public Customer GetCustomerById(string customerId)
        {
            //return _dbConn.Customers.Find(customerId);        // Old Ways
            return _dbConn.Customers.FirstOrDefault(c => string.Compare(c.CustomerID, customerId, true) == 0);
        }

        public void InsertCustomer(Customer customer)
        {
            _dbConn.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbConn.Entry(customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(string customerId)
        {
            Customer customer = _dbConn.Customers.Find(customerId);
            _dbConn.Customers.Remove(customer);
        }

        public void Save()
        {
            _dbConn.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                _dbConn.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}