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
using test_demo_exam_2.LogRegPages;
using test_demo_exam_2.MainPages;

namespace test_demo_exam_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppConnect.modelDB = new DemoExamEntities();
            AppFrame.frameMain = frmMain;

            AppFrame.frameMain.Navigate(new PageLogin());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            switch (btnExit.Content)
            {
                case "Войти как гость":
                    SelectedUser.user = null;
                    AppFrame.frameMain.Navigate(new PageUsers());
                    btnExit.Content = "Выйти";
                    break;
                case "Выйти":
                    SelectedUser.user = null;
                    AppFrame.frameMain.Navigate(new PageLogin());
                    btnExit.Content = "Войти как гость";
                    break;
            }
        }

        private void frmMain_Navigated(object sender, NavigationEventArgs e)
        {
            if(SelectedUser.user != null)
            {
                btnExit.Content = "Выйти";
            }
        }
    }
}
