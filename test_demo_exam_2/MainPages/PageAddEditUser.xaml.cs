using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace test_demo_exam_2.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditUser.xaml
    /// </summary>
    public partial class PageAddEditUser : Page
    {
        ValidationClass validation = new ValidationClass();
        Users currentUser = new Users();

        private string newFileName = null, saveFileName = null;

        public PageAddEditUser(Users user)
        {
            InitializeComponent();

            SetRoles();

            if(user != null)
            {
                currentUser = user;
                DataContext = currentUser;

                FindRole();
            }
        }

        private void FindRole()
        {
            cbRole.SelectedItem = currentUser.Roles.Name;
        }

        private void SetRoles()
        {
            cbRole.Items.Add("Выберите роль");

            foreach(var role in AppConnect.modelDB.Roles)
            {
                cbRole.Items.Add(role.Name);
            }

            cbRole.SelectedIndex = 0;
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void tbAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }

        private bool CheckAllData()
        {
            tbLogin.BorderBrush = Brushes.Black;
            pbPassword.BorderBrush = Brushes.Black;
            tbEmail.BorderBrush = Brushes.Black;
            tbPhone.BorderBrush = Brushes.Black;
            tbAge.BorderBrush = Brushes.Black;
            dpDateOfBirth.BorderBrush = Brushes.Black;
            cbRole.BorderBrush = Brushes.Black;

            if (!validation.CheckStringData(tbLogin.Text, 4, 50))
            {
                tbLogin.BorderBrush = Brushes.Red;
                MessageBox.Show("Логин должен быть от 4 до 50 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (currentUser.Login == tbLogin.Text) { }
            else
            {
                if (!validation.CheckUniqueLogin(tbLogin.Text))
                {
                    tbLogin.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пользователь с таким логином уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (currentUser.IdUser != 0 && pbPassword.Password.Length <= 0) { }
            else
            {
                if (!validation.CheckPassword(pbPassword.Password))
                {
                    pbPassword.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пароль должен быть от 6 до 50 символов (содерджать латинские строчные и прописные буквы, специальные символы и цифры)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (!validation.CheckEmail(tbEmail.Text))
            {
                tbEmail.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорректный формат email (от 5 до 250 символов)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (currentUser.Login == tbLogin.Text) { }
            else
            {
                if (!validation.CheckUniqueEmail(tbEmail.Text))
                {
                    tbEmail.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пользователь с таким email уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (!validation.CheckPhone(tbPhone.Text))
            {
                tbPhone.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорректный формат телефона (начинается с 8 и содердить 11 цифр)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (currentUser.Login == tbLogin.Text) { }
            else
            {
                if (!validation.CheckUniquePhone(tbPhone.Text))
                {
                    tbPhone.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пользователь с таким телефоном уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (!validation.CheckIntData(tbAge.Text, 1))
            {
                tbAge.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорретный возраст", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!CheckRole())
            {
                cbRole.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорретная роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!validation.CheckDateOfBirth(dpDateOfBirth.Text))
            {
                dpDateOfBirth.BorderBrush = Brushes.Red;
                MessageBox.Show("Некорретная дата рождения (необходимо быть от 14 до 110 лет)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckRole()
        { 
            var role = AppConnect.modelDB.Roles.FirstOrDefault(x => x.Name == cbRole.SelectedItem.ToString());
            if(role != null)
            {
                return true;
            }
            return false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAllData())
            {
                try
                {
                    currentUser.Login = tbLogin.Text;
                    currentUser.Password = pbPassword.Password;
                    currentUser.Email = tbEmail.Text;
                    currentUser.Phone = tbPhone.Text;
                    var role = AppConnect.modelDB.Roles.FirstOrDefault(x => x.Name == cbRole.SelectedItem.ToString());
                    currentUser.IdRole = role.IdRole;
                    currentUser.pCost = Int32.Parse(tbAge.Text);
                    currentUser.DeteOfBirth = DateTime.Parse(dpDateOfBirth.Text);
                    if (newFileName != null)
                    {
                        LoadImage();
                        currentUser.pImage = newFileName;
                    }

                    if (currentUser.IdUser == 0)
                    {
                        AppConnect.modelDB.Users.Add(currentUser);
                    }
                    AppConnect.modelDB.SaveChanges();

                    MessageBox.Show("Данные успешно сохранены", "Регистрация успешна", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frameMain.Navigate(new PageUsers());
                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения данных, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnLoadimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            Random random = new Random();

            saveFileName = dialog.FileName;
            if(saveFileName != null)
                newFileName = random.Next(10000,100000).ToString()+ random.Next(10000, 100000).ToString()+".png";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageUsers());
        }

        private void LoadImage()
        {
            try
            {
                File.Copy(saveFileName, System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\UserImages\\" + newFileName);
            }
            catch 
            {
                MessageBox.Show("Ошибка загрузки фото, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
