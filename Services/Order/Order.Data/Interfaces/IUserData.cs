using Order.Domain.DTO.Request;
using Order.Domain.DTO.Response;
using RegisterDependency;

namespace Order.Data.Interfaces
{

    //[RegisterScoped]

    [RegisterScoped]
    public interface IUserData
    {

        Task<UserResponse> GetById(int id);

        Task<IEnumerable<UserResponse>> GetAll();


        Task<UserResponse> Add(UserRequest userRequest);

        Task<UserResponse> Update(int id, UserRequest userRequest);

        Task<bool> Delete(int id);
    }
}
