//Team Members: Victor Espinoza, Jeff Yoshida
//CECS 475 - Application Programming using .NET
//Assignment #5 - Repository Pattern
//Due: 3/17/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace _475_Lab_4_Part_3 {
   public class Repository<T> : IRepository<T> where T : class {
      protected DbContext _context;
      protected DbSet<T> DbSet;

      public Repository(DbContext datacontext) {
         if (datacontext == null) throw new ArgumentNullException("dbContext");
         _context = datacontext;
         DbSet = _context.Set<T>();
      } //close Repository(...) constructor


      public void Insert(T entity) {
         if (entity == null) throw new ArgumentNullException("entity");
         //DbSet.Add(entity);
         DbSet.Attach(entity);
         _context.Entry(entity).State = System.Data.Entity.EntityState.Added;
         _context.SaveChanges();
      } //close Insert(...) 


      public void Delete(T entity) {
         if (entity == null) throw new ArgumentNullException("entity");
         //DbSet.Remove(entity);
         DbSet.Attach(entity);
         _context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
         _context.SaveChanges();
      } //close Delete(...)


      public void Update(T entity) {
         if (entity == null) throw new ArgumentNullException("entity");
         DbSet.Attach(entity);
         _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
         _context.SaveChanges();
      } //close Update(...)


      public T GetById(int id) {
         return DbSet.Find(id);
      } //close GetById(...)


      public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate) {
         return DbSet.Where(predicate).AsQueryable<T>();
      } //close SearchFor(...) 


      public IEnumerable<T> GetAll() {
         return DbSet.AsEnumerable();
      } //close GetAll() 


      public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties) {
         T item = null;
         IQueryable<T> dbQuery = null;
         foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            dbQuery = DbSet.Include<T, object>(navigationProperty);
         
         item = dbQuery.AsNoTracking().FirstOrDefault(where);
         return item;
      } //close GetSingle(...) 

   } //close class Repository<T>

} //close namespace _475_Lab_4_Part_3
