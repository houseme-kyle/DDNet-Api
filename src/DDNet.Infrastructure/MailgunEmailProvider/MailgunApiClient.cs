using DDNet.Application.Interfaces;
using FluentEmail.Core;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace DDNet.Infrastructure.MailgunEmailProvider
{
    public class MailgunApiClient : IAuthDispatcher
    {
        private readonly MailGunConfig mailGunConfig;

        public MailgunApiClient(IOptions<MailGunConfig> mailGunConfig)
        {
            this.mailGunConfig = mailGunConfig.Value;
        }

        public async Task DispatchPin(string emailAddress, Guid pin)
        {
            RestClient client = new RestClient("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", mailGunConfig.ApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", mailGunConfig.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"Mailgun Sandbox <mailgun@{mailGunConfig.Domain}>");
            request.AddParameter("to", emailAddress);
            request.AddParameter("subject", "Your security code!");
            request.AddParameter("text", $"Code: {pin}");
            request.Method = Method.POST;
            var response = client.Execute(request);
            /*
             * To Do: change to FluentEmail
            fluentEmail
                .To(emailAddress)
                .Subject("Your Pin Code")
                .Body($"{pin}");

            var response = await fluentEmail.SendAsync();*/
        }
    }
}
