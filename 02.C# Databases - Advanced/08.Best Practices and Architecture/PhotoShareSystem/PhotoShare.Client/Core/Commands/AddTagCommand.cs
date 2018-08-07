using System;
using PhotoShare.Client.Utilities;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand : ICommand
    {
        private const string TagAlreadyExists = "Tag {0} already exists!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyAddedTag = "Tag {0} was added successfully!";

        private readonly ITagService _tagService;
        private readonly IUserSessionService _userSessionService;

        public AddTagCommand(ITagService tagService, IUserSessionService userSessionService)
        {
            this._tagService = tagService;
            this._userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isLoggedIn)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            string tagName = args[0];

            bool tagExists = this._tagService.Exists(tagName);

            if (tagExists)
            {
                throw new ArgumentException(string.Format(TagAlreadyExists, tagName));
            }

            tagName = tagName.ValidateOrTransform();

            this._tagService.AddTag(tagName);

            return string.Format(SuccessfullyAddedTag, tagName);
        }
    }
}
