using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        public ClientTokenDto CreateClientToken(Client userApp)
        {
            throw new NotImplementedException();
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            throw new NotImplementedException();
        }
    }
}
