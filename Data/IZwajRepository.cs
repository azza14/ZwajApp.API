using System.Collections.Generic;
using System.Threading.Tasks;
using ZwajApp.API.Data.DAL.Models;
using ZwajApp.API.Dtos;
using ZwajApp.API.Helpers;

namespace ZwajApp.API.Data
{
    public interface IZwajRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id,bool isCurrentUser);
        Task<IEnumerable<User>> GetUsersTest(Userdto userParams);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams );
        Task<IEnumerable<Message>> GetConversation(int userId, int recipientId);
        Task<int> GetUnreadMessagesForUser(int userId);
        Task<Payment> GetPaymentForUser(int userId);
        Task<ICollection<User>> GetLikersOrLikees(int userId,string type);
        Task<ICollection<User>> GetAllUsersExceptAdmin();
    }
}