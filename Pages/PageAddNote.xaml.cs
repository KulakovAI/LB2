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
    /// Логика взаимодействия для PageAddNote.xaml
    /// </summary>
    public partial class PageAddNote : Page
    {
        private NOTE _currentNote = new NOTE();
        public PageAddNote(NOTE selectedNote)
        {
            InitializeComponent();
            DataContext = _currentNote;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentNote.FirstName)) error.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(_currentNote.LastName)) error.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(_currentNote.PhoneNumber)) error.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentNote.Birthday))) error.AppendLine("Укажите дату рождения");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentNote.idNote == 0)
            {
                LB2Entities.GetContext().NOTE.Add(_currentNote);
                try
                {
                    LB2Entities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageNote());
                    MessageBox.Show("Новый человек успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    LB2Entities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageNote());
                    MessageBox.Show("Человек успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageNote());
        }
    }
}
