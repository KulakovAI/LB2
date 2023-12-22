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
using LB2.Classes;

namespace LB2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageNote.xaml
    /// </summary>
    public partial class PageNote : Page
    {
        public PageNote()
        {
            InitializeComponent();
            DtgNote.ItemsSource = LB2Entities.GetContext().NOTE.ToList();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TxbSearch.Text;
            var listSomeValues = LB2Entities.GetContext().NOTE.ToList();
            if (!string.IsNullOrWhiteSpace(search))
                DtgNote.ItemsSource = listSomeValues.
                Where(x => x.Birthday.Value.ToString("MMMM").ToLower().Contains(search.ToLower())).ToList();
            else
                DtgNote.ItemsSource = LB2Entities.GetContext().NOTE.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddNote(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DtgNote.SelectedItems.Cast<NOTE>().ToList();
            if (MessageBox.Show($"Удалить {usersForRemoving.Count()} запись?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    LB2Entities.GetContext().NOTE.RemoveRange(usersForRemoving);
                    LB2Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            DtgNote.ItemsSource = LB2Entities.GetContext().NOTE.ToList();
        }
    }
}
