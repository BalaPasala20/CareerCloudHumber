using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
	public abstract class BaseLogic<TPoco>
		where TPoco : IPoco
	{
		protected IDataRepository<TPoco> _repository;
		public BaseLogic(IDataRepository<TPoco> repository)
		{
			_repository = repository;
		}

		protected virtual void Verify(TPoco[] pocos)
		{
			return;
		}

		public virtual TPoco Get(Guid id)
		{
			return _repository.GetSingle(c => c.Id == id);
		}

		public virtual List<TPoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public virtual void Add(TPoco[] pocos)
		{
			foreach (TPoco poco in pocos)
			{
				if (poco.Id == Guid.Empty)
				{
					poco.Id = Guid.NewGuid();
				}
			}

			_repository.Add(pocos);
		}

		public virtual void Update(TPoco[] pocos)
		{
			_repository.Update(pocos);
		}

		public void Delete(TPoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
