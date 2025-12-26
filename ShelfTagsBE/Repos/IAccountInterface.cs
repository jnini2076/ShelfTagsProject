using System;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Repos;

public interface IAccountInterface
{
        Task<Account> GetAccountAsync(string username, string password);

        Task<Account> FindByUsernameAsync(string username);

        Task<Account> FindByPasswordAsync(string password);
}
