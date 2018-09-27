using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP
{
    public interface IFacebookService
    {
        Task<dynamic> GetPostAsync(string accessToken, string id, string param);
    }
    public class FacebookServices : IFacebookService
    {
        public readonly IFacebookClient _facebookClient;
        public FacebookServices(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }
        public async Task<dynamic> GetPostAsync(string accessToken, string id, string param)
        {
            var result = await _facebookClient.GetAsync<dynamic>(accessToken, id, param);
            //if(result == null)
            //{
            //    return null;
            //}
            return result;
        }
    }
}
