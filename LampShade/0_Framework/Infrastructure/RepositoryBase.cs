using System;
using System.Linq.Expressions;
using _0_Framework.Domain;
using Emplement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RedisDatabase.Infrastructure;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext _context;
        public RepositoryBase(DbContext context)
        {
            
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add<T>(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression); // ||
            //return _context.Set<T>().All(expression); &&

        }

        public T Get(TKey id)
        {
            return _context.Find<T>(id);
        }

        public List<T> Get()
        {
            
            return _context.Set<T>().ToList();  // .Set??
            
            // _context.Products.Tolist();
            // _context.ProductCategories.Tolist();
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}    

