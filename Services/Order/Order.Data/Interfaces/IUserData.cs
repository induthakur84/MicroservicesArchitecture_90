using Order.Domain.DTO.Request;
using Order.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Data.Interfaces
{

    //[RegisterScoped]
    public interface IUserData
    {

        Task<UserResponse> GetById(int id);

        Task<IEnumerable<UserResponse>> GetAll();


        Task<UserResponse> Add(UserRequest userRequest);

        Task<UserResponse> Update(int id, UserRequest userRequest);

        Task<bool> Delete(int id);
    }
}
