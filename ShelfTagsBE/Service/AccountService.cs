using System;
using Microsoft.VisualBasic;
using ShelfTagsBE.Models;
using ShelfTagsBE.Repos;

namespace ShelfTagsBE.Service;

public class AccountService(IAccountInterface accountInterface,ILogger<AccountService>logger)
{



        public async Task<Account> Login(string username,string password)
    {
        var findUsername = await accountInterface.FindByUsernameAsync(username);

        if(findUsername == null)
        {
                logger.LogWarning("Username doesnt exist");
                throw new InvalidOperationException("Username doesnt exitst");

        }

        var findPassword = await accountInterface.FindByPasswordAsync(password);

        if(findPassword == null)
        {
            logger.LogInformation("password didnt match");
            throw new InvalidOperationException("password wasnt correct");
        }

        
        logger.LogInformation("passed the validations");
        var accountLocated = await accountInterface.GetAccountAsync(username,password);
        return accountLocated;
    }
}
