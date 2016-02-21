using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace wpfPass
{
    /// <summary>
    /// Interaction logic for RemindWindow.xaml
    /// </summary>
    public partial class RemindWindow : Window
    {
        
        public RemindWindow()
        {
            InitializeComponent();
        }
        
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if(new ValidationClass().CheckIfNotEmpty(EmailBox.Text))
            {
                PasskeeperModelContext db = new PasskeeperModelContext();
                try
                {
                    var users = from u in db.Users
                                select u;
                    var findUser = db.Users.FirstOrDefault(u => u.Email == EmailBox.Text);
                    if (findUser != null)
                    {
                        byte[] ba = new byte[8];
                        new RNGCryptoServiceProvider().GetBytes(ba);
                        var hexString = BitConverter.ToString(ba); 
                        string newPassword = hexString.Replace("-", "");
                        findUser.MasterPassword = PasswordProtect.CreateHash(newPassword);
                        db.SaveChanges();
                        new EmailingClass().SendEmail(EmailBox.Text, "Your password for PassKeeper is reset", newPassword);
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("This e-mail is not registered!");
                        EmailBox.Focus();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }        
            }
            else { MessageBox.Show("Please populate all fields!"); }
        }
    }
}
