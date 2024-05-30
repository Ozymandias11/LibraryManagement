using Library.Model.Models;
using Library.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;

namespace Library.Service
{
    public class SmsService : ISmsService
    {

        private readonly VonageClient _vonageClient;
        private readonly VonageSettings _vonageSettings;
        public SmsService(IOptions<VonageSettings> vonageSettings)
        {
            _vonageSettings = vonageSettings.Value;
            var credentials = Credentials.FromApiKeyAndSecret(_vonageSettings.ApiKey, _vonageSettings.SecretKey);
            _vonageClient = new VonageClient(credentials);   
        }

        public async Task<bool> SendSms(string phoneNumber, int verificationCode)
        {


            var smsBody = $"your validation code is {verificationCode}";

            var response = await _vonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
            {
                To = phoneNumber,
                From = "Vonage APIs",
                Text = smsBody
            });


            return response.Messages[0].Status == "0";



        }
    }
}
