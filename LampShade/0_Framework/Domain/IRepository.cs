using System;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
	public interface IRepository<TKey, T> where T : class
	{
		List<T> Get();
		T Get(TKey id);
		void SaveChanges();
		void Create(T entity);
		bool Exists(Expression<Func<T, bool>> expression);
	}
}

