using helping_hand.Models;
using helping_hand.Models.Dto;
using System.Threading.Tasks;

namespace helping_hand.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenDto> Login(LoginDto userForLogin);
        Task Logout();
    }
}