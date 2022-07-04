using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private IEnumerable<Claim> GetClaim(UserApp userApp, List<String> audiences)
        {
            var userList = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userApp.Id),
                    new Claim(JwtRegisteredClaimNames.Email, userApp.Email),
                    new Claim(ClaimTypes.Name, userApp.UserName),
                    // jti jason'u kimliklendirir yani bir identity verir
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
            // aşağaıdaki isimlendirme ile birlikte karşılaştırılma yapılmatkadır. Rasgele isimlendirme yapılmamaktadır.
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;

        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Auidences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.ClientId.ToString());

            return claims;


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
