using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories.Services;
using AuthServer.Core.Reposityories;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
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

        public Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
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
