using Application.Common;
using Application.DTOs.UsrDTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IOC.Interface
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<UserDto>> CreateAsync(CreateUserDto dto);
        Task<Result<UserDto>> UpdateAsync(int id, UpdateUserDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
