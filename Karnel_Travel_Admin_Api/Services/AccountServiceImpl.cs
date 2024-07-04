using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class AccountServiceImpl : IAccountService
{
    private DatabaseContext db;
    public AccountServiceImpl(DatabaseContext _db) { 
        db = _db;
    }
    public bool Login(string username, string password)
    {
        var account =  db.AdminAccounts.SingleOrDefault(a => a.Username == username);
        if (account == null)
        {
            return false;
        }
        return BCrypt.Net.BCrypt.Verify(password, account.Password);
        
    }
}
