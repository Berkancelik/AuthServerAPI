using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories.Services;
using AuthServer.Core.Reposityories;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService 
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshToken;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshToken = userRefreshToken;
        }

        public Task<Response<TokenDto>> CreateByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {

            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong",400,true);
            // iç içe olmayan if else için savunmacı kod yazılır.yaptığımız işlem savunmacı kod'a örnek olarak verilmekltedir.
            if(await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshToken.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken==null)
            {
                await _userRefreshToken.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token, 200);

        }

        public Task<Response<ClientTokenDto>> CreateTokenByClient(ClientTokenDto clientTokenDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoDataDto>> RevokeRefrehToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
