using System;
using Microsoft.EntityFrameworkCore;
using ShelfTagsBE.Data;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Repos;

public class AccountRepository(DBmodel context) : IAccountInterface
{
        

        public async Task<Account> FindByUsernameAsync(string username)
    {
        return await context.Accounts.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Account> FindByPasswordAsync(string password)
    {
        return await context.Accounts.FirstOrDefaultAsync(p => p.Password == password);
    }

    public async Task<Account> GetAccountAsync(string username, string password)
{
    return await context.Accounts
        .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
}
}
