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
using test_demo_exam_2.AppConnection;
using test_demo_exam_2.MainPages;

namespace test_demo_exam_2.LogRegPages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageRegister());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = AppConnect.modelDB.Users.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);
            if (user != null)
            {
                SelectedUser.user = user;
                AppFrame.frameMain.Navigate(new PageUsers());
            }
            else
            {
                WindowCaptcha captcha = new WindowCaptcha();
                captcha.ShowDialog();
            }
        }
    }
}
