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

namespace wpfPass
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new RegisterWindow().ShowDialog();
            ShowDialog();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (new ValidationClass().CheckIfNotEmpty(LoginBox.Text, PasswordBox.Password))
            {
                PasskeeperModelContext db = new PasskeeperModelContext();
                try
                {
                    var findUser = db.Users.FirstOrDefault(u => u.Username == LoginBox.Text);
                    if (findUser != null && PasswordProtect.ValidatePassword(PasswordBox.Password, findUser.MasterPassword))
                    {
                        MainWindow.LoggedUser = findUser.UserId;
                        MainWindow.Secret = findUser.Username;
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Wrong login or password!");
                        PasswordBox.Password = "";
                        LoginBox.Focus();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }        
            }
            else { MessageBox.Show("Please populate all fields!"); }
            
        }

        private void RemindPassButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new RemindWindow().ShowDialog();
            ShowDialog();
        }
    }
}
