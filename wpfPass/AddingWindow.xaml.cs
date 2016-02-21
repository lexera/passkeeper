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
    /// Interaction logic for AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {
        public AddingWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(new ValidationClass().CheckIfNotEmpty(LoginBox.Text, LocationBox.Text, PasswordBox.Text))
            {
                
                var passwordSet = new PasswordSet();
                passwordSet.Location = LocationBox.Text;
                passwordSet.Login = LoginBox.Text;
                
                passwordSet.Password = PasswordProtect.EncryptStringAES(PasswordBox.Text, MainWindow.Secret);
                
                passwordSet.UserUserId = MainWindow.LoggedUser;
                passwordSet.Status = "active";
                passwordSet.DateCreated = DateTime.Now;
                passwordSet.DateChanged = DateTime.Now;
                try
                {
                    MainWindow.context.PasswordSets.Add(passwordSet);
                    MainWindow.context.SaveChanges();
                    DialogResult = true;
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
            else { MessageBox.Show("Please populate all fields!"); }
        }
    }
}
