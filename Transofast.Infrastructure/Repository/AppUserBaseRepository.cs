using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;
using Transofast.Domain.IRepositories;
using Transofast.Infrastructure.DataAccess;

namespace Transofast.Infrastructure.Repository
{
    public class AppUserBaseRepository<T>: IAppUserRepository<T> where T : class, IUser
    {
        protected readonly TransofastDbContext _dbContext;
        protected readonly UserManager<AppUser> _userManager;
        protected DbSet<T> _table;

        public AppUserBaseRepository(TransofastDbContext DbContext, UserManager<AppUser> userManager)
        {
            _dbContext = DbContext;
            _table = _dbContext.Set<T>();
            _userManager = userManager;
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task Delete(T item)
        {
            item.Status = Status.Deleted;//Oluşturduğun entitynin status propertysinin değerini deleted yap
            await Update(item);
        }


        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).FirstAsync();
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task Update(T item)
        {
            _dbContext.Entry<T>(item).State = EntityState.Modified;//Güncelleme Yap
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GeneratePassword()
        {
            const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string SpecialChars = "!@#$%&*()_-+=<>?";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Bir büyük harf ekle
            password.Append(UppercaseChars[random.Next(UppercaseChars.Length)]);

            // Beş küçük harf ekle
            for (int i = 0; i < 5; i++)
            {
                password.Append(LowercaseChars[random.Next(LowercaseChars.Length)]);
            }

            // Bir özel sembol ekle
            password.Append(SpecialChars[random.Next(SpecialChars.Length)]);

            // Karakterleri karıştır
            string shuffledPassword = new string(password.ToString().OrderBy(c => random.Next()).ToArray());

            return await Task.FromResult(shuffledPassword);
        }
    }
}
