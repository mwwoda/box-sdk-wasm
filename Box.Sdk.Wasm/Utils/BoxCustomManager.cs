using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Managers;
using Box.V2.Services;
using System.Text.Json;

namespace Box.Sdk.Wasm.Utils
{
    public class BoxCustomManager : BoxResourceManager
    {
        public BoxCustomManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, 
            string asUser = null, bool? suppressNotifications = null) 
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        public async Task<IBoxResponse<object>> CustomApiCall(string path, RequestMethod method, string payload)
        {
            BoxRequest request = new BoxRequest(_config.BoxApiUri, path)
                .Method(method);

            if (!string.IsNullOrEmpty(payload))
            {
                request.Payload(payload);
            }

            return await ToResponseAsync<object>(request).ConfigureAwait(false);
        }
    }
}
