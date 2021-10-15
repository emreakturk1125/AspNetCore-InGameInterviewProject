using InGameProject.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InGameProject.DataAccess.Concrete.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = c.Set<T>();

        }
        public void Delete(T item)
        {
            var deleteItem = c.Entry(item);
            deleteItem.State = EntityState.Deleted;
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter).FirstOrDefault();
        }

        public void Insert(T item)
        {
            using var c = new Context();
            c.Add(item);
            c.SaveChanges(); 
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {

            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
        }

        public void Update(T item)
        {
            using var c = new Context();
            c.Update(item);
            c.SaveChanges();
        }
    }
}
