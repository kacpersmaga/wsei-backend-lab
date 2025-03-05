using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using BackendLab01;

namespace ApplicationCore.UserService;

public class ChatUserService : IChatUserService
{
    private readonly IGenericRepository<ChatUser, int> _repository;
    
    public ChatUserService(IGenericRepository<ChatUser, int> repository)
    {
        _repository = repository;
    }

    public void Add(string connectionId, string username)
    {
        var chatUser = new ChatUser
        {
            ConnectionId = connectionId,
            Username = username
        };
        
        _repository.Add(chatUser);
    }

    public IEnumerable<(string ConnectionId, string Username)> GetAll()
    {
        return _repository.FindAll()
            .Select(user => (user.ConnectionId, user.Username));
    }

    public string GetConnectionIdByName(string username)
    {
        var user = _repository.FindAll()
            .FirstOrDefault(u => u.Username == username);
            
        return user?.ConnectionId;
    }

    public void RemoveByName(string username)
    {
        var user = _repository.FindAll()
            .FirstOrDefault(u => u.Username == username);
            
        if (user != null)
        {
            _repository.RemoveById(user.Id);
        }
    }
}