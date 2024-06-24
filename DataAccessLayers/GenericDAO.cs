using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

		public T GetById(int id, string includeProperties = "")
		{
			IQueryable<T> query = _context.Set<T>();

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			var entityType = typeof(T);
			var keyProperty = _context.Model.FindEntityType(entityType)?.FindPrimaryKey()?.Properties.FirstOrDefault();

			if (keyProperty == null)
				throw new InvalidOperationException("No primary key defined for this entity.");

			var keyName = keyProperty.Name;

			return query.SingleOrDefault(entity => EF.Property<int>(entity, keyName) == id);
		}



		public List<T> GetAll(string includeProperties = "")
		{
			IQueryable<T> query = _context.Set<T>();
			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
			return query.ToList();
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
