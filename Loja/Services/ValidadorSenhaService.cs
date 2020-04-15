using IdentityServer4.Models;
using IdentityServer4.Validation;
using Loja.Api.Models;
using System.Linq;
using System.Threading.Tasks;
 
namespace Loja.Services
{
    public class ValidadorSenhaService : IResourceOwnerPasswordValidator
    {
        private LojaContexto _context;

        // utilizar o mesmo banco atual

        public ValidadorSenhaService(LojaContexto context)
        {
            _context = context;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // acessar cliente na base
            var user = _context.Clientes.FirstOrDefault(x => x.Nome == context.UserName);

            // verificar a senha
            if (user != null && user.Password.TrimEnd() == context.Password)
            {
                // retornar objeto tipo GrantValidationResult com sub, auth e claims
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom", 
                    claims: UserProfileService.GetUserClaims(user)
                );
                return Task.CompletedTask;
            } 
            else 
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "Usu�rio ou senha inv�lidos");

                return Task.FromResult(context.Result);
            }
        }
     
    }
}