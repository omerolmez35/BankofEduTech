using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : class
	{
		private readonly BankofEduTechContext _context;

		public ReadRepository(BankofEduTechContext context)
		{
			_context = context;
		}
        public  DbSet<T> Table => _context.Set<T>();

		public IQueryable<T> GetAll()
		{
			return Table.AsQueryable();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await Table.FindAsync(id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = Table.AsNoTracking();
			return await query.FirstOrDefaultAsync(method);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
		{
			var query = Table.Where(method);
			if (!tracking)
				query = query.AsNoTracking();
			return query;
		}
	}
}
