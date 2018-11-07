using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModernStore.Api.Security;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ModernStore.Api.Controllers
{
    public class AccountController : BaseController
    {
        private Customer _customer;
        private readonly ICustomerRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public AccountController(IOptions<TokenOptions>jwtOptions, IUow Uow, ICustomerRepository repository) : base(Uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);
            //_jsonSerializerSettings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("v1/authenticate")]           //FromForm = Form URL Encoded para senha
        public async Task<IActionResult> Post([FromForm] AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválido") });

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválido") });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.Username),
                new Claim(JwtRegisteredClaimNames.NameId, command.Username),
                new Claim(JwtRegisteredClaimNames.Email, command.Username),
                new Claim(JwtRegisteredClaimNames.Sub, command.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("ModernStore")
            };

            //Gerar Json Web Token
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            //Codificar o Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //Resposta composta junto com o token (A gosto do fregues)
            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _customer.Id,
                    name = _customer.Name.ToString(),
                    email = _customer.Email.ToString(),
                    username = _customer.User.Username
                }
            };

            //Converter para JSON
            var jsonToken = JsonConvert.SerializeObject(response, _jsonSerializerSettings);
            return new OkObjectResult(jsonToken);

        }



        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero) throw new ArgumentException("O Período deve ser maior que zero",nameof(options));

            if (options.SigningCredentials == null) throw new ArgumentNullException(nameof(options.SigningCredentials));

            if (options.JtiGenerator == null) throw new ArgumentNullException(nameof(options.JtiGenerator));

        }

        //Metodo asinc
        //Se o Usuário for Autenticado Retorna os Clains
        private Task<ClaimsIdentity>GetClaims(AuthenticateUserCommand command)
        {
            //Fazer um select no banco para obter as permissões
            var customer = _repository.GetByUsername(command.Username);

            if (customer == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if(!customer.User.Authenticate(command.Username, command.Password))
                return Task.FromResult<ClaimsIdentity>(null);

            _customer = customer;

            return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(customer.User.Username, "Token"),
                new[] {
                    new Claim("ModernStore", "User")//Claim deve ser igual ao police do start up
                }));
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}