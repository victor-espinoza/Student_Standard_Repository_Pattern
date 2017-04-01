//Team Members: Victor Espinoza, Jeff Yoshida
//CECS 475 - Application Programming using .NET
//Assignment #5 - Repository Pattern
//Due: 3/17/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _475_Lab_4_Part_3 {
   public interface IRepository<T> {
      void Insert(T entity);
      void Delete(T entity);
      void Update(T entity);
      T GetById(int id);
      IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
      IEnumerable<T> GetAll();
      T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
   } //close interface IRepository<T> : IDisposable
} //close namespace _475_Lab_4_Part_3
