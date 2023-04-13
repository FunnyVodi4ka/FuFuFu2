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

namespace test_demo_exam_2.LogRegPages
{
    /// <summary>
    /// Логика взаимодействия для WindowCaptcha.xaml
    /// </summary>
    public partial class WindowCaptcha : Window
    {
        private string rightCode = "";
        public WindowCaptcha()
        {
            InitializeComponent();

            ReloadCaptcha();
        }

        private void ReloadCaptcha()
        {
            rightCode = "";
            string str = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            Random rnd = new Random();

            for(int i = 0; i < 6; i++)
            {
                rightCode += str[rnd.Next(str.Length)];
            }

            tblCaptcha.Text = rightCode;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if(tbCaptcha.Text == rightCode)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный код", "Неправильный код", MessageBoxButton.OK, MessageBoxImage.Warning);
                ReloadCaptcha();
            }
        }
    }
}
