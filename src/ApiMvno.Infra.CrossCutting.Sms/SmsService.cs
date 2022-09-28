using System.Text.RegularExpressions;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Sms;

public class SmsService : ISmsService
{
    private readonly SmsApiOptions _smsApiOptions;
    public SmsApiAuthToken SmsApiAuthToken { get; private set; }

    public SmsService(IOptions<SmsApiOptions> options)
    {
        _smsApiOptions = options.Value;
    }

    public async Task<bool> Authenticate()
    {
        if (SmsApiAuthToken == null || SmsApiAuthToken.Expiration <= DateTime.Now.AddMinutes(-5))
        {
            var result = await new Url(_smsApiOptions.Url).Clone()
                .AppendPathSegment("/user/auth")
                .PostJsonAsync(new SmsApiAuthRequest(_smsApiOptions.Email, _smsApiOptions.Password));

            var response = await result.GetJsonAsync<SmsApiAuthResponse>();

            if (!result.ResponseMessage.IsSuccessStatusCode
                || !response.Success
                || string.IsNullOrEmpty(response.Data?.Token))
            {
                throw new ArgumentNullException("Token");
            }

            SmsApiAuthToken = response.Data;

            return response.Success;
        }

        return SmsApiAuthToken != null && SmsApiAuthToken.Expiration > DateTime.Now.AddMinutes(-5);
    }

    public async Task<bool> SendAsync(SendSmsRequest request)
    {
        if (_smsApiOptions == null || !_smsApiOptions.Ok())
            throw new ArgumentNullException(SmsApiOptions.sectionKey);

        if (_smsApiOptions.Enabled)
        {
            if (!Regex.IsMatch(request.Destination, "\\d{12,13}"))
                throw new ArgumentNullException(nameof(request.Destination));
            if (string.IsNullOrEmpty(request.Message))
                throw new ArgumentNullException(nameof(request.Message));
            if (!await Authenticate())
                throw new Exception("Could not authenticate at SMS API");

            var response = await new Url(_smsApiOptions.Url).Clone()
                .AppendPathSegment("/sms/send")
                .WithOAuthBearerToken(SmsApiAuthToken.Token)
                .PostJsonAsync(request);

            return response.ResponseMessage.IsSuccessStatusCode;
        }

        return _smsApiOptions.Enabled;
    }
}