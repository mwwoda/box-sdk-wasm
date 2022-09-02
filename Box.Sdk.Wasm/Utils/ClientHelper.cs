using Box.V2;
using Box.V2.Auth;
using Box.V2.CCGAuth;
using Box.V2.Config;
using static Box.Sdk.Wasm.Utils.Enums;

namespace Box.Sdk.Wasm.Utils
{
    public static class ClientHelper
    {
        public static async Task<IBoxClient> CreateBoxClient(Auth auth, string clientId, string clientSecret, string userId, string token)
        {
            return auth == Auth.CCG ? 
                await CreateClientFromCCG(clientId, clientSecret, userId) : 
                await Task.FromResult(CreateClientFromDevToken(token));
        }

        private static BoxClient CreateClientFromDevToken(string token)
        {
            var config = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET").Build();
            var session = new OAuthSession(token, "N/A", 3600, "bearer");
            return new BoxClient(config, session);
        }

        private static async Task<IBoxClient> CreateClientFromCCG(string clientId, string clientSecret, string userId)
        {
            var config = new BoxConfigBuilder(clientId, clientSecret).Build();
            var boxCCG = new BoxCCGAuth(config);
            var userToken = await boxCCG.UserTokenAsync(userId);
            return boxCCG.UserClient(userToken, userId);
        }
    }
}
