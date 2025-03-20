using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppointmentSystem.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IApplicationUser?> FindByUsernameAsync(string username)
        {
            Console.WriteLine($"🔍 Searching for user: {username}");

            // Normalize the username and email
            var normalizedUsername = _userManager.NormalizeName(username);
            var normalizedEmail = _userManager.NormalizeEmail(username);

            Console.WriteLine($"🔹 Normalized UserName: {normalizedUsername}");
            Console.WriteLine($"🔹 Normalized Email: {normalizedEmail}");

            // Try finding the user by UserName
            var user = await _userManager.FindByNameAsync(normalizedUsername);
            if (user != null)
            {
                Console.WriteLine($"✅ User found by UserName: {user.UserName}");
                return user;
            }

            Console.WriteLine("❌ User not found by UserName, trying Email...");

            // Try finding the user by Email
            user = await _userManager.FindByEmailAsync(normalizedEmail);
            if (user != null)
            {
                Console.WriteLine($"✅ User found by Email: {user.Email}");
                return user;
            }

            Console.WriteLine("❌ User not found by either UserName or Email.");
            return null;
        }


        public async Task<bool> VerifyPasswordAsync(string username, string password)
        {
            var user = await FindByUsernameAsync(username);
            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync((ApplicationUser)user, password);
        }

        public async Task<RegistrationResult> RegisterAsync(string username, string email, string password, string role)
        {
            // Create the user
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            // Create the user in the database
            var identityResult = await _userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                var rolesToAssign = new List<string> { role };

                // If role is "Admin", assign additional roles
                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    rolesToAssign.AddRange(["Patient", "Doctor"]);
                }

                // Assign multiple roles
                var roleResult = await _userManager.AddToRolesAsync(user, rolesToAssign);

                if (roleResult.Succeeded)
                {
                    return RegistrationResult.Success();
                }
                else
                {
                    // If role assignment fails, delete the user and return the error
                    await _userManager.DeleteAsync(user);
                    return RegistrationResult.Failure(roleResult.Errors.Select(e => e.Description).ToList());
                }
            }

            // If user creation fails, return the error
            return RegistrationResult.Failure(identityResult.Errors.Select(e => e.Description).ToList());
        }


        public async Task<LoginResult> LoginAsync(string usernameOrEmail, string password)
        {
            // Find the user by username or email
            var user = await _userManager.FindByNameAsync(usernameOrEmail) ??
                       await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                return LoginResult.Failure("User not found.");

            // Verify the password
            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                return LoginResult.Failure("Invalid password.");

            // Generate a token with user claims
            var token = await GenerateTokenWithClaimsAsync(user);

            return LoginResult.Success(token);
        }

        public async Task<string> GenerateTokenWithClaimsAsync(IApplicationUser user)
        {
            // Cast the user to ApplicationUser
            var appUser = user as ApplicationUser;
            if (appUser == null)
                throw new ArgumentException("User must be of type ApplicationUser");

            // Get the user's roles
            var roles = await _userManager.GetRolesAsync(appUser);

            // Create a list of claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id), // User ID
                new Claim(ClaimTypes.Name, appUser.UserName),     // Username
                new Claim(ClaimTypes.Email, appUser.Email),       // Email
                new Claim("aud", "http://localhost:5047")       // ✅ Add Audience claim
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Get JWT settings from configuration
            var issuer = "http://localhost:5047"; // Change as needed
            var audience = "http://localhost:5047"; // Change as needed
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHere_1234567890"));

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                Issuer = issuer,   // ✅ Add Issuer
                Audience = audience, // ✅ Add Audience
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }

    }
}
