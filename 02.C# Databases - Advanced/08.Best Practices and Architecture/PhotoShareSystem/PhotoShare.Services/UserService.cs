using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext _dbContext;

        public UserService(PhotoShareContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
        {
            return this._dbContext
                .Users
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
        }

        public TModel ById<TModel>(int id)
        {
            return this.By<TModel>(x => x.Id == id)
                .SingleOrDefault();
        }

        public TModel ByUsername<TModel>(string username)
        {
            return this.By<TModel>(x => x.Username == username)
                .SingleOrDefault();
        }

        public bool Exists(int id)
        {
            return this.ById<User>(id) != null;
        }

        public bool Exists(string username)
        {
            return this.ByUsername<User>(username) != null;
        }

        public User Register(string username, string password, string email)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email
            };

            this._dbContext
                .Users
                .Add(user);

            this._dbContext.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            User user = this._dbContext
                .Users
                .FirstOrDefault(u => u.Username == username);

            user.IsDeleted = true;

            this._dbContext.SaveChanges();
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            Friendship friendship = new Friendship()
            {
                UserId =  userId,
                FriendId = friendId
            };

            this._dbContext
                .Friendships
                .Add(friendship);

            this._dbContext.SaveChanges();

            return friendship;
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            Friendship friendship = new Friendship()
            {
                UserId = userId,
                FriendId = friendId
            };

            this._dbContext
                .Friendships
                .Add(friendship);

            this._dbContext.SaveChanges();

            return friendship;
        }

        public void ChangePassword(int userId, string password)
        {
            //User user = this.ById<User>(userId); //SaveChanges() will not make any changes as By method returns new objects which are not tracked!
            //user.Password = password;

            var currentUser = this._dbContext.Users.Find(userId);
            currentUser.Password = password;

            this._dbContext.SaveChanges();
        }

        public void SetBornTown(int userId, int townId)
        {
            //User user = this.ById<User>(userId);
            //user.BornTownId = townId;

            var currentUser = this._dbContext.Users.Find(userId);
            currentUser.BornTownId = townId;
            
            this._dbContext.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            //User user = this.ById<User>(userId);
            //user.CurrentTownId = townId;

            var currentUser = this._dbContext.Users.Find(userId);
            currentUser.CurrentTownId = townId;

            this._dbContext.SaveChanges();
        }
    }
}