using MyApp.Models;
using MyApp.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Services
{
    public interface IUserProfileService
    {
        Task<UserProfile> GetUserProfileAsync(int id);
        Task CreateUserProfileAsync(UserProfile userProfile);
        Task<bool> UpdateUserProfileAsync(UserProfile userProfile);
        Task<bool> DeleteUserProfileAsync(int id);
    }

    public class UserProfileService : IUserProfileService
    {
        private readonly UserProfileDbContext _context;

        public UserProfileService(UserProfileDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetUserProfileAsync(int id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        public async Task CreateUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfile userProfile)
        {
            _context.Entry(userProfile).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserProfileExists(userProfile.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteUserProfileAsync(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return false;
            }
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> UserProfileExists(int id)
        {
            return await _context.UserProfiles.AnyAsync(e => e.Id == id);
        }
    }
}