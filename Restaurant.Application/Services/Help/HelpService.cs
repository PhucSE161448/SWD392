using AutoMapper;
using MimeKit;
using Restaurant.Application.Interfaces;
using Restaurant.Application.ViewModels.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Restaurant.Application.Interfaces.Help;
using Restaurant.Application.Commons;

namespace Restaurant.Application.Services.Help
{
    public class HelpService : IHelpService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppConfiguration _emailConfig;

        public HelpService(IUnitOfWork unitOfWork, IMapper mapper, AppConfiguration emailConfig)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailConfig = emailConfig;
        }

        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.EmailConfiguration.From, _emailConfig.EmailConfiguration.From));
            emailMessage.To.Add(new MailboxAddress(_emailConfig.EmailConfiguration.From, _emailConfig.EmailConfiguration.From));
            //emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.EmailConfiguration.SmtpServer, _emailConfig.EmailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.EmailConfiguration.UserName, _emailConfig.EmailConfiguration.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
