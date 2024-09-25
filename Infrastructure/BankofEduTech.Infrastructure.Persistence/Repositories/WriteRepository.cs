using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : class
	{
		private readonly BankofEduTechContext _context;

		public WriteRepository(BankofEduTechContext context)
		{
			_context = context;
		}
        public  DbSet<T> Table => _context.Set<T>();
       

		public async Task<bool> AddAsync(T model)

		{
			EntityEntry entityEntry = await Table.AddAsync(model);
			await SaveAsync();
			return entityEntry.State == EntityState.Added;
		}


		public bool Remove(int id)
		{
			var data = Table.Find(id);
			if (data == null)
				return false;
			EntityEntry entityEntry = Table.Remove(data);
			Save();

			return entityEntry.State == EntityState.Deleted;
		}
		public bool Update(T model)
		{
			EntityEntry entityEntry = Table.Update(model);
			Save();
			return entityEntry.State == EntityState.Modified;

		}
		public Task<int> SaveAsync()
		{ return _context.SaveChangesAsync(); }
		public int Save()
		{ return _context.SaveChanges(); }
	}
}
