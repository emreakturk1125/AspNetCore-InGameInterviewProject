using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InGameProject.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        List<T> List();
        void Insert(T item);
        T Get(Expression<Func<T, bool>> filter);
        void Update(T item);
        void Delete(T item);
        List<T> List(Expression<Func<T, bool>> filter); 

    }
}
