using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		Task<T> GetByIdAsync(int id);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
	}
}
