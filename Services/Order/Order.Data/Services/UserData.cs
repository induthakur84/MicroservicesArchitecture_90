using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Data.Context;
using Order.Data.Interfaces;
using Order.Domain.DTO.Request;
using Order.Domain.DTO.Response;
using Order.Domain.Entities;

namespace Order.Data.Services
{
    public class UserData : IUserData
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly IMapper _mapper;


        public UserData(StoreDbContext storeDbContext, IMapper mapper)
        {
            _storeDbContext = storeDbContext;
            _mapper = mapper;
        }

        public async Task<UserResponse> Add(UserRequest userRequest)
        {
            if (userRequest == null)
            {
                return null;
            }

            var user = _mapper.Map<User>(userRequest);


            _storeDbContext.Users.Add(user);

            await _storeDbContext.SaveChangesAsync();


            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _storeDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _storeDbContext.Users.Remove(user);

            await _storeDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            var users = await _storeDbContext.Users.AsNoTracking().OrderByDescending(u => u.Id).ToListAsync();

            var response = users.Select(u =>

            _mapper.Map<UserResponse>(u)).ToList();

            return response;
        }

        public  async Task<UserResponse> GetById(int id)
        {
            var user = await _storeDbContext.Users.AsNoTracking()
                 .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                return null;
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Update(int id, UserRequest userRequest)
        {
            var existingUser = await _storeDbContext.Users.FindAsync(id);

            if(existingUser==null)
            {
                return null;
            }
            _mapper.Map(userRequest, existingUser);

            await _storeDbContext.SaveChangesAsync();


            return _mapper.Map<UserResponse>(existingUser);
        }
    }
}


////AsSplitQuery why we use in ef core?
//// var customer= await
//     // _storedbcontext.Customer
//           .Include(c => c.order)
//            .include(c => c.Payment)
//            .AsSplitQuery()
//            .ToListAsync();