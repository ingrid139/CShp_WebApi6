using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Loja.Api.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

namespace Loja.Services
{
    public class UserProfileService : IProfileService
    {
        private LojaContexto _context;

        // utilizar o mesmo banco atual
        public UserProfileService(LojaContexto context)
        {
            _context = context;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // cast token to objeto
            var request = context.ValidatedRequest as ValidatedTokenRequest;

            // verificar se o token é nulo
            if (request != null)
            {
                // buscar o usuário na base e add as respectivas claims
                var user = _context.Clientes.FirstOrDefault(x => x.Nome == request.UserName);
                if (user != null)
                    context.AddRequestedClaims(GetUserClaims(user));
            }

            return Task.CompletedTask;
        }

        //set contexto ativo
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        //add claims
        public static Claim[] GetUserClaims(Cliente user)
        {
            return new []
            {
                new Claim(ClaimTypes.Name, user.Nome ?? ""),
                new Claim(ClaimTypes.Email, user.Email.TrimEnd() ?? ""),
                new Claim(ClaimTypes.Role, "user")
            };
        }

    }
}