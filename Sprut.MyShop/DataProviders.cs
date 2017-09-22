using System;
using System.Collections.Generic;

namespace Sprut.MyShop
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id=Guid.Empty;
        }

        public Guid Id { get; set; }
    }

    public interface IDataProvider<T> where T : DomainObject
    {
        T Find(Guid id);
        void Save(T subj);
        void Delete(T subj);
        List<T> Select(string query, dynamic param);
    }

    public class SQLDataProvider<T> : IDataProvider<T> where T : DomainObject
    {
        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(T subj)
        {
            
            throw new NotImplementedException();
        }

        public void Delete(T subj)
        {
            throw new NotImplementedException();
        }

        public List<T> Select(string query, dynamic param)
        {
            throw new NotImplementedException();
        }
    }

    public class DataProviders
    {
        private static DataProviders _current;
        public static DataProviders Current => _current ?? (_current = new DataProviders());

        public SQLDataProvider<Product> Products = new SQLDataProvider<Product>();
        public SQLDataProvider<Category> Categories = new SQLDataProvider<Category>();


    }

    public class CartProviders
    {
        private static CartProviders _current;
        public static CartProviders Current => _current ?? (_current = new CartProviders());

        public CartProviders()
        {
            Cart = new CartProvider();
        }

        public ICartProvider Cart { get; set; }

    }

    public class OrderProviders
    {
        private static OrderProviders _current;
        public static OrderProviders Current => _current ?? (_current = new OrderProviders());

        public OrderProviders()
        {
            Order = new OrderProvider();
        }

        public IOrderProvider Order { get; set; }

    }

    public class CategoryProviders
    {
        private static CategoryProviders _current;
        public static CategoryProviders Current => _current ?? (_current = new CategoryProviders());

        public CategoryProviders()
        {
            Category = new CategoryProvider();
        }

        public ICategoryProvider Category { get; set; }

    }
}