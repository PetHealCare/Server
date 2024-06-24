using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
	public interface IGenericDAO<T> where T : class
	{
		T Create(T entity);
		T GetById(int id, string includeProperties = "");
		List<T> GetAll(string includeProperties = "");
		bool Update(T entity);
		bool Delete(int id);
		T UpdateNew(T entity);
	}
}
