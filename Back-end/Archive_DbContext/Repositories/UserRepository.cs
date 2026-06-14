using Archive_DbContext.Entities;
using Domain.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive_DbContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<User>> GetAll()
        {
            var userEntitiey = await _context.User.AsNoTracking().ToListAsync();
            var users = userEntitiey.Select(u => User.Create(u.Id, u.Login, u.Password, u.Email).user).ToList();
            return users;
        }

        public async Task<User?> GetById(Guid id)
        {
            var userEntity = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null) { return null; }
            var user = User.Create(userEntity.Id, userEntity.Login, userEntity.Password, userEntity.Email).user;
            return user;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var userEntity = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
            if (userEntity == null) { return null; }
            var user = User.Create(userEntity.Id, userEntity.Login, userEntity.Password, userEntity.Email).user;
            return user;
        }


        public async Task<Guid> CreateNewUser(User user)
        {
            UserEntity entity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
            };

            await _context.User.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.User.Where(u => u.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
