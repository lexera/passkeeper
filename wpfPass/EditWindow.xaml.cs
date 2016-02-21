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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            LoginBox.Text = MainWindow.selectedSet.Login;
            LocationBox.Text = MainWindow.selectedSet.Location;
            PasswordBox.Text = PasswordProtect.DecryptStringAES(MainWindow.selectedSet.Password, MainWindow.Secret);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (new ValidationClass().CheckIfNotEmpty(LoginBox.Text, LocationBox.Text, PasswordBox.Text))
            {
                MainWindow.selectedSet.Login = LoginBox.Text;
                MainWindow.selectedSet.Location = LocationBox.Text;
                MainWindow.selectedSet.Password = PasswordProtect.EncryptStringAES(PasswordBox.Text, MainWindow.Secret);
                MainWindow.selectedSet.DateChanged = DateTime.Now;
                try
                {
                    MainWindow.context.SaveChanges();
                    DialogResult = true;
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
