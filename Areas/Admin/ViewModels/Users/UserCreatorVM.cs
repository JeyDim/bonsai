﻿using System;
using System.Text.RegularExpressions;
using AutoMapper;
using Bonsai.Areas.Front.ViewModels.Auth;
using Bonsai.Code.Utils.Helpers;
using Bonsai.Data.Models;

namespace Bonsai.Areas.Admin.ViewModels.Users
{
    /// <summary>
    /// VM for creating a new password-authorized user.
    /// </summary>
    public class UserCreatorVM : RegisterUserVM
    {
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password copy for typo checking.
        /// </summary>
        public string PasswordCopy { get; set; }

        /// <summary>
        /// Assigned role.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// ID of the personal page.
        /// </summary>
        public Guid? PersonalPageId { get; set; }

        /// <summary>
        /// Configures automatic mapping.
        /// </summary>
        public override void Configure(IProfileExpression profile)
        {
            profile.CreateMap<UserEditorVM, AppUser>()
                   .MapMember(x => x.Birthday, x => x.Birthday)
                   .MapMember(x => x.FirstName, x => x.FirstName)
                   .MapMember(x => x.MiddleName, x => x.MiddleName)
                   .MapMember(x => x.LastName, x => x.LastName)
                   .MapMember(x => x.Email, x => x.Email)
                   .MapMember(x => x.PageId, x => x.PersonalPageId)
                   .ForMember(x => x.UserName, opt => opt.MapFrom(x => Regex.Replace(x.Email, "[^a-z0-9]", "")))
                   .ForAllOtherMembers(x => x.Ignore());

            profile.CreateMap<AppUser, UserEditorVM>()
                   .MapMember(x => x.PersonalPageId, x => x.PageId)
                   .ForMember(x => x.Role, opt => opt.Ignore())
                   .ForMember(x => x.CreatePersonalPage, opt => opt.Ignore());
        }
    }
}
