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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Data.Entity;


namespace wpfPass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int LoggedUser { get; set; }
        public static string Secret { get; set; }
        public static PasskeeperModelContext context { get; set; }
        public static PasswordSet selectedSet { get; set; }
        public CollectionViewSource passwordSetViewSource { get; set; }

        
        public MainWindow()
        {
            if (new LoginWindow().ShowDialog() == true)
            {
                InitializeComponent();
            }
            else { Application.Current.Shutdown(); }
        }                
              
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataLoad();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddingWindow().ShowDialog();
        }
        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordDatagrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the password to edit!");
            }
            else
            {
                selectedSet = (PasswordSet)PasswordDatagrid.SelectedItem;
                new EditWindow().ShowDialog();
                dataLoad();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordDatagrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the password to delete!");
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete password", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedSet = (PasswordSet)PasswordDatagrid.SelectedItem;
                        selectedSet.Status = "deleted";
                        selectedSet.DateChanged = DateTime.Now;
                        context.SaveChanges();
                        dataLoad();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString());
                    }
                }
            }
        }
        public void dataLoad()
        {
            context = new PasskeeperModelContext();
            try
            {
                context.PasswordSets.Where(p => p.UserUserId == LoggedUser && p.Status == "active").Load();
                this.DataContext = context.PasswordSets.Local;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Hide();
                if (new LoginWindow().ShowDialog() == true)
                {
                    dataLoad();
                    this.Show();
                }
                else { Application.Current.Shutdown(); }
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow().ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Password Keeper, 2016.\nSource code is available at GitHub.\nhttps://github.com/lexera/passkeeper");
        } 
    }
}
