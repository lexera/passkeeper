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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (new ValidationClass().CheckIfNotEmpty(OldPasswordBox.Text, NewPasswordBox.Text, ConfirmNewPasswordBox.Text))
            {
                if (NewPasswordBox.Text == ConfirmNewPasswordBox.Text)
                {
                    PasskeeperModelContext db = new PasskeeperModelContext();
                    try
                    {
                        var users = from u in db.Users
                                    select u;
                        var findUser = db.Users.FirstOrDefault(u => u.UserId == MainWindow.LoggedUser);
                        if (PasswordProtect.ValidatePassword(OldPasswordBox.Text, findUser.MasterPassword))
                        {
                            findUser.MasterPassword = PasswordProtect.CreateHash(NewPasswordBox.Text);
                            db.SaveChanges();
                            DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Old password is incorrect!");
                        } 
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString());
                    }
                } 
                else 
                {
                    MessageBox.Show("New passwords do not match!");
                }
            }
        }
    }
}
