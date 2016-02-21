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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (new ValidationClass().CheckIfNotEmpty(LoginBox.Text, PasswordBox.Text, RepeatPassBox.Text, EmailBox.Text))
            {
                if (RepeatPassBox.Text == PasswordBox.Text)
                {
                    PasskeeperModelContext context = new PasskeeperModelContext();
                    var user = new User();
                    user.Username = LoginBox.Text;
                    user.MasterPassword = PasswordProtect.CreateHash(PasswordBox.Text);
                    user.Email = EmailBox.Text;
                    user.DateCreated = DateTime.Now;
                    user.Status = "active";
                                        
                    try
                    {
                        var users = from u in context.Users
                                    select u;
                        var duplicate = context.Users.FirstOrDefault(u => u.Username == user.Username);
                        
                        if (duplicate != null)
                        {
                            MessageBox.Show("Such username already exists! Please make another username!");
                        }
                        else
                        {
                            context.Users.Add(user);
                            context.SaveChanges();
                            MessageBox.Show("User was added successfully!");
                            DialogResult = true;
                        }

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString());
                    }                   
                }
                else { MessageBox.Show("Passwords do not match!"); }
            }
            else { MessageBox.Show("Please populate all fields!"); }
        }
    }
}
