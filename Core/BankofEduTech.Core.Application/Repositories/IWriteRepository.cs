﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : class
	{
		Task<bool> AddAsync(T model);
		bool Remove(int id);
		bool Update(T model);
		Task<int> SaveAsync();
	}
}
