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

        private DateTime? _birthdate;
        public DateOnly Birthdate
        {
            get => DateOnly.FromDateTime(_birthdate ?? DateTime.Now); 
            set => _birthdate = value.ToDateTime(TimeOnly.MinValue);  
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
        public ICommand BackCommand { get; set; }

        #endregion

        #region Ctor
        public SignupViewModel()
        {
            Random random = new Random();
            msg = random.Next(200, 999);
            NewUser = new User();
            RegisterCommand = new RelayCommand(RegisterCommandExecute, IsRegisterCommand);
            GetCode = new RelayCommand(SendVerificationCode, IsGetCodeCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            Database = new MirtalibDbContext();
        }
        #endregion

        #region Functions
        public bool IsGetCodeCommand(object? obj)
        {
            return !string.IsNullOrEmpty(NewUser?.Email);
        }

        public bool IsRegisterCommand(object? obj)
        {
            return NewUser != null &&
                   !string.IsNullOrEmpty(NewUser.Firstname) &&
                   !string.IsNullOrEmpty(NewUser.LatsName) &&
                   !string.IsNullOrEmpty(NewUser.Email) &&
                   Codemail == msg &&
                   !string.IsNullOrEmpty(Password);
        }

        public void RegisterCommandExecute(object obj)
        {
            if (NewUser != null && Password != null)
            {
                // Hash the password using the PasswordHasher class
                var hashedBytes = PasswordHasher.HashPassword(Password);

                NewUser.DateofBirth = Birthdate;
               
                NewUser.Password = hashedBytes;

                // Save the new user to the database
                Database.Users.Add(NewUser);
                Database.SaveChanges();
                MessageBox.Show("You are registered");
                NewUser = new User();

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
