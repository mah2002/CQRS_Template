using Grpc.Net.Client;
using Newtonsoft.Json;

namespace NecoTemplate.gRPC;

public class nAuthClient : IAuthService
{
    private readonly authSrv.authSrvClient _client;

    public nAuthClient(string address)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        httpHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;


        var channel = GrpcChannel.ForAddress("https://localhost:5003", new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        //var channel = GrpcChannel.ForAddress("https://localhost:7068");
        _client = new authSrv.authSrvClient(channel);
    }

    public async Task<bool?> GetAuthAsync(Guid id)
    {
        try
        {
            var request = new ExampleAuthRequest
            {
                Id = id.ToString(),
                Name = "GetExample"
            };
            var response = await _client.GetAuthAsync(request);
            return response.IsAuthenticated;
        }
        catch (Exception ex)
        {
            var all = ex.ToString();
            var all2 = JsonConvert.SerializeObject(ex);
            var all3 = ex.Message;

            return null;
        }
    }


}
