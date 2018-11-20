using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoardGames.DataAccess.Contexts;
using BoardGames.DataAccess.Entities.Base;
using BoardGames.DomainLogic.Infrastructure.ErrorHandler;
using BoardGames.DomainLogic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.DomainLogic.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DataContext _context;
        private DbSet<T> _set;

        public Repository(DataContext context)
        {
            _context = context;
            _set = context.Set<T>();
            //_errorHandler = errorHandler;
        }

        protected DbSet<T> Set => _set ?? (_set = _context.Set<T>());

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _set.SingleOrDefaultAsync(s => s.Id == id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _set.Where(exp);
        }

        public async void Insert(T entity)
        {
            //if (entity == null)
            //    throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            entity.DateCreated = DateTime.Now;
            await _set.AddAsync(entity);
        }

        public async void Update(T entity)
        {
            //if (entity == null)
            //    throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            entity.DateModified = DateTime.Now;
            var oldEntity = await _context.FindAsync<T>(entity.Id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(T entity)
        {
            //if (entity == null)
            //    throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));

            _set.Remove(entity);
        }
    }
}
