//using System;
//using System.Linq;
//using System.Data.Entity;
//using System.Collections.Generic;

//namespace WebApplication.Core
//{
//    public class SentencesService
//    {
//        // ...

//        // Отобразить всех покупателей
//        public IQueryable<Customer> GetCustomers()
//        {
//            // Используем LINQ-запрос для извлечения данных
//            return dbContext.CustomerSet.AsQueryable<Customer>();
//        }

//        // Редактировать данные покупателя
//        public void EditCustomer(int? customerId)
//        {
//            Customer customer = dbContext.CustomerSet
//                .Where(c => c.CustomerId == customerId).FirstOrDefault();

//            if (customer != null && TryUpdateModel<Customer>(customer))
//            {
//                // Обновить данные в БД с помощью Entity Framework
//                dbContext.Entry<Customer>(customer).State = EntityState.Modified;
//                dbContext.SaveChanges();
//            }
//        }

//        // Удалить покупателя
//        public void DeleteCustomer()
//        {
//            Customer customer = new Customer();

//            if (TryUpdateModel<Customer>(customer))
//            {
//                dbContext.Entry<Customer>(customer).State = EntityState.Deleted;
//                dbContext.SaveChanges();
//            }
//        }

//        // Вставить нового покупателя
//        public void InsertCustomer()
//        {
//            Customer customer = new Customer();

//            if (TryUpdateModel<Customer>(customer))
//            {
//                dbContext.Entry<Customer>(customer).State = EntityState.Added;
//                dbContext.SaveChanges();
//            }
//        }
//    }
//}