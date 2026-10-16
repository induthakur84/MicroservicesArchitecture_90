
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

        // Constructor injection:
        // StoreDbContext is injected to communicate with the database.
        public UserData(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        // CREATE: Add a new user to the database.
        public async Task<UserResponse> Add(UserRequest userRequest)
        {
            // Check whether the request object is null.
            if (userRequest == null)
            {
                return null;
            }

            // Manual mapping:
            // Copy values from UserRequest DTO to the User entity.
            var user = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email
            };

            // Add the user entity to the database context.
            _storeDbContext.Users.Add(user);

            // Save changes permanently to the database.
            await _storeDbContext.SaveChangesAsync();

            // Manual mapping:
            // Convert the saved User entity to UserResponse DTO.
            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return response;
        }

        // DELETE: Delete a user using their ID.
        public async Task<bool> Delete(int id)
        {
            // FindAsync searches for a user by primary key.
            var user = await _storeDbContext.Users.FindAsync(id);

            // If the user does not exist, return false.
            if (user == null)
            {
                return false;
            }

            // Mark the user entity for deletion.
            _storeDbContext.Users.Remove(user);

            // Apply the deletion to the database.
            await _storeDbContext.SaveChangesAsync();

            return true;
        }

        // READ: Retrieve all users from the database.
        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            // AsNoTracking improves read performance when
            // we do not need to modify the retrieved entities.
            // OrderByDescending returns the newest IDs first.
            var users = await _storeDbContext.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            // Manually convert every User entity into a DTO.
            var response = users.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();

            return response;
        }

        // READ: Retrieve one user using their ID.
        public async Task<UserResponse> GetById(int id)
        {
            // Find the matching user without tracking it.
            var user = await _storeDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            // Return null if the user does not exist.
            if (user == null)
            {
                return null;
            }

            // Manually map the entity to the response DTO.
            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return response;
        }

        // UPDATE: Update an existing user.
        public async Task<UserResponse> Update(
            int id,
            UserRequest userRequest)
        {
            // Find the existing user by primary key.
            var existingUser = await _storeDbContext.Users
                .FindAsync(id);

            // If the user does not exist, return null.
            if (existingUser == null)
            {
                return null;
            }

            // Check that the request is not null.
            if (userRequest == null)
            {
                return null;
            }

            // Manual mapping:
            // Copy the request values into the existing entity.
            existingUser.Name = userRequest.Name;
            existingUser.Email = userRequest.Email;

            // EF Core tracks existingUser, so it detects these
            // property changes and saves them to the database.
            await _storeDbContext.SaveChangesAsync();

            // Return the updated user as a response DTO.
            var response = new UserResponse
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email
            };

            return response;
        }
    }
}
