using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class PasswordValidationService : IPasswordValidationService
    {
        public string Encrypt(string password)
        {
            return Encrypt(password, null);
        }

        public bool IsMatch(string encrypted, string entered)
        {
            var bytes = Convert.FromBase64String(encrypted);
            var salt = bytes.TakeLast(16);
            return encrypted == Encrypt(entered, salt.ToArray());
        }

        private string Encrypt(string password, byte[] salt)
        {
            if (salt == null)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var key = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(key.Concat(salt).ToArray());
        }
    }
}
