using Application.Common;
using Application.DTOs.UsrDTOs;
using Application.IOC.Interface;
using Domain.Model;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.IOC.Service
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var dtos = users.Select(MapToDto);
            return Result<IEnumerable<UserDto>>.Success(dtos);
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == id);
            if (user is null)
                return Result<UserDto>.Failure("User with id" + id + "not found.");
            var res = MapToDto(user);
            return Result<UserDto>.Success(res);
        }

        public async Task<Result<UserDto>> CreateAsync(CreateUserDto dto)
        {
            var existing = await _unitOfWork.Users.GetAsync(u => u.NationalCode == dto.NationalCode);

            if (existing is not null)
                return Result<UserDto>.Failure("A user with this national code already exists.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode,
                BirthDate = dto.BirthDate
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            var res = MapToDto(user);
            return Result<UserDto>.Success(res);
        }

        public async Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == id);
            if (user is null)
                return Result<UserDto>.Failure("User with id" + id + "not found.");

            var existing = await _unitOfWork.Users.GetAsync(u => u.NationalCode == dto.NationalCode && u.Id != id);

            if (existing is not null)
                return Result<UserDto>.Failure("Another user with this national code already exists.");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.NationalCode = dto.NationalCode;
            user.BirthDate = dto.BirthDate;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
            var res = MapToDto(user);
            return Result<UserDto>.Success(res);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == id);
            if (user is null)
                return Result<bool>.Failure("User with id " + id + "not found.");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveAsync();

            return Result<bool>.Success(true);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalCod = user.NationalCode,
                BirthDate = user.BirthDate
            };
        }
    }
}
