using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;
using System.Windows.Input;
using AppUserPanel.Commands;
using AppLibrary.Models;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text;
using AppLibrary.Data;
using System.Windows;

namespace AppUserPanel.Viewmodels
{
    public class SignupViewModel : BaseViewModel
    {
        #region Properties

        private string? errorMassage;
        public string? ErrorMassage { get => errorMassage; set { errorMassage = value; OnPropertyChanged(); } }
        private string? password;
        public string? Password { get => password; set { password = value; OnPropertyChanged(); } }
        //burda nese temavar
        private DateTime _birthdate;
        public DateTime Birthdate
        {
            get => _birthdate;
            set {_birthdate = value;OnPropertyChanged();
        }
        }



        private MirtalibDbContext Database;
        private int? codemail;
        public int? Codemail
        {
            get { return codemail; }
            set { codemail = value; OnPropertyChanged(); }
        }

        private User? user;
        public User? NewUser
        {
            get { return user; }
            set { user = value; OnPropertyChanged(); }
        }



        private int? msg;

        #endregion

        #region Commands
        public ICommand RegisterCommand { get; set; }
        public ICommand GetCode { get; set; }

        #endregion

        #region Ctor
        public SignupViewModel()
        {
            Random random = new Random();
            msg = random.Next(1000, 9000);
            NewUser = new User();
            RegisterCommand = new RelayCommand(RegisterCommandExecute, IsRegisterCommand);
            GetCode = new RelayCommand(SendVerificationCode, IsGetCodeCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            Database = new MirtalibDbContext();
            Birthdate = DateTime.Now;
        }
        #endregion

        #region Functions
        public bool IsGetCodeCommand(object? obj)
        {
            return !string.IsNullOrEmpty(NewUser?.Email);
        }
        public bool IsRegisterCommand(object? obj)
        {
             var userExists = Database.Users.Any(u => u.UserName == NewUser.UserName);

            return NewUser != null &&
                   !string.IsNullOrEmpty(NewUser.Firstname) &&
                   !string.IsNullOrEmpty(NewUser.LatsName) &&
                   !string.IsNullOrEmpty(NewUser.Email) &&
                   Codemail == msg &&
                   !string.IsNullOrEmpty(Password)&& !userExists; 
        }

        public void RegisterCommandExecute(object obj)
        {
            if (NewUser != null && Password != null)
            {
                var userExists = Database.Users.Any(u => u.UserName == NewUser.UserName);

                if (userExists)
                {
                    MessageBox.Show("The username already exists. Please choose a different username.");
                    return;
                }

                // Hashing
                var hashedBytes = PasswordHasher.HashPassword(Password);

                NewUser.DateofBirth = Birthdate;
               
                NewUser.Password = hashedBytes;

                // Save
                Database.Users.Add(NewUser);
                Database.SaveChanges();
                MessageBox.Show("You are registered");
             
                // Clear data
                NewUser = new User();
                Codemail = null; // Clear Codemail
                Password = string.Empty; // Clear Password
                Random random = new Random();
                msg = random.Next(1000, 9000);
            }
        }




        private void SendVerificationCode(object? obj)
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
        }



        #endregion
    }
}
