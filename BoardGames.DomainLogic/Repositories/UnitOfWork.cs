using System;
using System.Collections.Generic;
using System.Text;
using BoardGames.DataAccess.Contexts;
using BoardGames.DomainLogic.Repositories.Interfaces;

namespace BoardGames.DomainLogic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IGameRepository _gameRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository => _gameRepository ?? (_gameRepository = new GameRepository(_context));
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IGameRepository GameRepository { get; }
        IUserRepository UserRepository { get; }
        int SaveChanges();
    }
}
