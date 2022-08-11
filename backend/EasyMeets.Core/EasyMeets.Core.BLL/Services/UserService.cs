﻿using AutoMapper;
using EasyMeets.Core.BLL.Interfaces;
using EasyMeets.Core.Common.DTO.User;
using EasyMeets.Core.DAL.Context;
using EasyMeets.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMeets.Core.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(EasyMeetsCoreContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<UserDto?> GetUserPreferences(long userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(_ => _.Id == userId);
            if (user is null)
            {
                return default;
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserPreferences(UserDto userDto)
        {
            var userEntity = await GetUserById(userDto.Id);
            if (userEntity is null)
            {
                throw new KeyNotFoundException("User doesn't exist");
            }

            userEntity.Name = userDto.UserName;
            userEntity.PhoneNumber = userDto.Phone;
            userEntity.Country = userDto.Country;
            userEntity.Language = userDto.Language;
            userEntity.DateFormat = userDto.DateFormat;
            userEntity.TimeFormat = userDto.TimeFormat;
            userEntity.TimeZone = userDto.TimeZone;

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        private async Task<User?> GetUserById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User doesn't exist");
        }
    }
}
