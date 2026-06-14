using Application.Functions;
using Archive_DbContext.Entities;
using Archive_DbContext.Repositories;
using Domain.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JWTFunc _jwt;
        private readonly IHttpContextAccessor _context;
        public UserService(IUserRepository userRepository, JWTFunc jwt, IHttpContextAccessor httpContextAccessor)
        {
            _repository = userRepository;
            _jwt = jwt;
            _context = httpContextAccessor;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _repository.GetAll();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Guid> Registration(User user)
        {
            user.Password = HashPassword.CreateHashPass(user.Password);
            return await _repository.CreateNewUser(user);
        }

        public async Task<string> Login(string login, string password)
        {
            var user = await _repository.GetByLogin(login);
            if (user == null)
            {
                return "Неверный логин или пароль";
            }

            if (HashPassword.VerifyPassword(password, user.Password))
            {
                string jwt = _jwt.CreateJwt(user);
                _context.HttpContext.Response.Cookies.Append("MCook", jwt);

                return jwt;
            }
            else
            {
                return "Неверный логин или пароль";
            }
        }

        public string Loguot()
        {
            _context.HttpContext.Response.Cookies.Delete("MCook");
            return "Выход из системы";
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
