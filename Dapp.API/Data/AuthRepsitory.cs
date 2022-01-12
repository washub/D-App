using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapp.API.Data
{
    public class AuthRepsitory : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepsitory(DataContext context)
        {
            _context = context;
            
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if(user==null)
                return null;
            if(!verifyUserPassword(password, user.SaltPassword, user.HashPassword))
                return null;
            return user;
        }

        private bool verifyUserPassword(string password, byte[] saltPassword, byte[] hashPassword)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(saltPassword)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i< computedHash.Length;i++)
                    if(computedHash[i]!=hashPassword[i])
                         return false;
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreateHashedPassword(password, out passwordHash, out passwordSalt);
            user.HashPassword = passwordHash;
            user.SaltPassword = passwordSalt;
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreateHashedPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512()){
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

        public async Task<bool> UserExists(string username)
        {
            if( await _context.Users.AnyAsync(x=> x.UserName==username))
                return true;
            else
                return false;
        }
    }
}