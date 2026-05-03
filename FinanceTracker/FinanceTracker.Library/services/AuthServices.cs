using FinanceTracker.Library.Data;
using FinanceTracker.Library.Models; 

namespace FinanceTracker.Library.Services;

public class AuthManager
{
    private readonly JsonRepository<User> _repository;
    private List<User> _users;
    
}