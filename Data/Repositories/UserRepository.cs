using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ZwajApp.API.Data.DAL;
using ZwajApp.API.Data.DAL.Models;
using ZwajApp.API.Data.Infrastructure;
using ZwajApp.API.Data.Interfaces;

namespace ZwajApp.API.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        { }
    }
}
