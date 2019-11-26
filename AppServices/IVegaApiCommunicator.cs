using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTApi.AppServices
{
    public interface IVegaApiCommunicator
    {
        public Task<AuthenticationResponseModel> AuthenticateAsync(AuthenticationRequestModel request);
    }
}