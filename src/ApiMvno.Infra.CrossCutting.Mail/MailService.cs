using System.Globalization;
using System.Text.RegularExpressions;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Mail;

public class MailService : IMailService
{
    private readonly IOptions<MailApiOptions> _options;
    public MailApiOptions Config => _options.Value;

    public IFlurlRequest Url => new Url(Config.Url)
        .WithHeader("token", Config.Token);

    public MailService(IOptions<MailApiOptions> options)
    {
        _options = options;
    }

    public async Task<bool> SendAsync(SendMailResquest sendMailResquest)
    {
        if (Config == null || !Config.Ok())
            throw new ArgumentNullException(MailApiOptions.sectionKey);
        if (string.IsNullOrEmpty(sendMailResquest.EmailDestination) || sendMailResquest.EmailDestination.Split(';')
                .ToList().Any(email => !IsValidEmail(email)))
            throw new ArgumentException("Invalid Email.", nameof(sendMailResquest.EmailDestination));
        if (string.IsNullOrEmpty(sendMailResquest.Subject))
            throw new ArgumentNullException(nameof(sendMailResquest.Subject));
        if (string.IsNullOrEmpty(sendMailResquest.Body))
            throw new ArgumentNullException(nameof(sendMailResquest.Body));

        var result = await Url
            .AppendPathSegment("/api/email")
            .PostJsonAsync(sendMailResquest);

        return result.ResponseMessage.IsSuccessStatusCode;
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        if (email.Contains(";"))
        {
            var results = new List<bool>();
            foreach (var e in email.Split(";"))
            {
                results.Add(IsValidEmail(e));
            }

            return results.All(result => result);
        }

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}