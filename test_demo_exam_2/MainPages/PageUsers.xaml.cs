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

namespace test_demo_exam_2.MainPages
{
    /// <summary>
    /// Логика взаимодействия для PageUsers.xaml
    /// </summary>
    public partial class PageUsers : Page
    {
        Verification verification = new Verification();

        public PageUsers()
        {
            InitializeComponent();

            SetFilter();
            SetSort();
            ShowHiddenButtons();

            listViewUsers.ItemsSource = SortFilterUsers();
        }

        private void ShowHiddenButtons()
        {
            if (SelectedUser.user != null)
            {
                btnAdd.Visibility = Visibility.Visible;
                btnDel.Visibility = Visibility.Visible;
            }
            else
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnDel.Visibility = Visibility.Hidden;
            }
        }

        Users[] SortFilterUsers()
        {
            var users = AppConnect.modelDB.Users.ToList();
            var allRows = users;

            if(tbSearch.Text != null)
            {
                users = users.Where(x => x.Login.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            if(cbFilter.SelectedIndex > 0)
            {
                users = users.Where(x => x.Roles.Name == cbFilter.SelectedItem.ToString()).ToList();
            }

            switch (cbFilter.SelectedIndex)
            {
                case 1:
                    users = users.OrderBy(x => x.Login).ToList();
                    break;
                case 2:
                    users = users.OrderByDescending(x => x.Login).ToList();
                    break;
            }

            if(users.Count > 0)
            {
                tblCounter.Text = "Показано: " + users.Count + " из " + allRows.Count;
            }
            else
            {
                tblCounter.Text = "Не найдено";
            }

            return users.ToArray();
        }

        private void SetFilter()
        {
            cbFilter.Items.Add("Без фильтрации");

            foreach(var item in AppConnect.modelDB.Roles)
            {
                cbFilter.Items.Add(item.Name);
            }

            cbFilter.SelectedIndex = 0;
        }

        private void SetSort()
        {
            cbSort.Items.Add("Без сортировки");
            cbSort.Items.Add("По возростанию");
            cbSort.Items.Add("По убыванию");

            cbSort.SelectedIndex = 0;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewUsers.ItemsSource = SortFilterUsers();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewUsers.ItemsSource = SortFilterUsers();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewUsers.ItemsSource = SortFilterUsers();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AppConnect.modelDB.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            listViewUsers.ItemsSource = SortFilterUsers();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (verification.CheckAccess())
            {
                if (listViewUsers.SelectedItems != null)
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var row = listViewUsers.SelectedItems.Cast<Users>().ToList().ElementAt(0);
                            if (SelectedUser.user.IdUser != row.IdUser)
                            {
                                AppConnect.modelDB.Users.Remove(row);
                                AppConnect.modelDB.SaveChanges();
                                listViewUsers.ItemsSource = SortFilterUsers();
                            }
                            else
                            {
                                MessageBox.Show("Вы не можете удалить себя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка удаления, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Для удаления записи её необходимо выбрать", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void listViewUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (verification.CheckAccess())
            {
                Users user = listViewUsers.SelectedItem as Users;
                AppFrame.frameMain.Navigate(new PageAddEditUser(user));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddEditUser((sender as Button).DataContext as Users));
        }
    }
}
