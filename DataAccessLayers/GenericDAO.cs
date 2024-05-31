using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
	public class GenericDAO<T> : IGenericDAO<T> where T : class
	{
		protected readonly PetHealthCareContext _context;

		public GenericDAO(PetHealthCareContext context)
		{
			_context = context;
		}

		public T Create(T entity)
		{
			_context.Set<T>().Add(entity);
			if( _context.SaveChanges() > 0)
			{
				return entity;
			}
			return null;
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public bool Update(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			return _context.SaveChanges() > 0;
		}

		public T UpdateNew(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			if (_context.SaveChanges() > 0)
			{
				return entity;
			}
			return null;
		}

		public bool Delete(int id)
		{
			var entity = _context.Set<T>().Find(id);
			if (entity == null)
			{
				return false;
			}
			_context.Set<T>().Remove(entity);
			return _context.SaveChanges() > 0;
		}
	}

}
