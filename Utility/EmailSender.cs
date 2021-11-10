using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("aedf49a94b2b94ed1d099d4383e1cf1f", "257d4b020deaf4d5836e38b7b735ea56")
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "0322644@student.rocvantwente.nl")
            .Property(Send.FromName, "CamperPlanner")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage)
            .Property(Send.Recipients, new JArray {
                new JObject{
                    {
                        "Email", email }
                    }
            });
            MailjetResponse response = await client.PostAsync(request);
            }
        }
    }
