using AppLibrary.Data;
using AppUserPanel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.Text.RegularExpressions;
using System.Windows;
using AppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace AppUserPanel.ViewModels;

public class ChangeMailViewModel : BaseViewModel
{


    public ICommand ChangeCommand { get; set; }
    public ICommand GetCode { get; set; }
    private int? msg;

    private MirtalibDbContext Database;
    private int? codemail;
    public int? Codemail
    {
        get { return codemail; }
        set { codemail = value; OnPropertyChanged(); }
    }
    private AppLibrary.Models.User? user;
    public AppLibrary.Models.User? NewUser
    {
        get { return user; }
        set { user = value; OnPropertyChanged(); }
    }

    public ChangeMailViewModel()
    {
        Database = new MirtalibDbContext();

        NewUser = Database.Users
                              .Include(u => u.Photo)
                              .FirstOrDefault(u => u.Id == PasswordHasher.UserId) ?? new User();
        BackCommand = new RelayCommand(BackCommandExecute);
        ChangeCommand = new RelayCommand(ChangeMail);
        GetCode = new RelayCommand(SendVerificationCode, IsGetCodeCommand);
        Random random = new Random();
        msg = random.Next(1000, 9000);
    }

    private void SendVerificationCode(object? obj)
    {

        Thread thread = new Thread(() =>
        {
            string senderEmail = "mirtalibemirli498@gmail.com";
            string senderPassword = "eiwk gysv kmwd tttx";
            string recipientEmail = NewUser?.Email;

            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
                {
                    mailMessage.Subject = "Verification Code";
                    mailMessage.Body = $"Your verification code is {msg}";

                    smtpClient.Send(mailMessage);

                }
            }
        });

        thread.Start();

    }

    private bool IsGetCodeCommand(object? obj)
    {

        return true;
    }

        private void ChangeMail(object? obj)
        {
        if (codemail==msg)
        {

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(NewUser.Email))
            {

                Database.Users.Update(NewUser);
                Database.SaveChanges();
            MessageBox.Show("Mail Changed");
                if (obj is Page page)
                {
                    page.NavigationService.GoBack();

                }
            }
        }


    }
}
