using CinemaAdmin.Tables;
using System;
using System.Windows;
using System.Windows.Documents;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_FilmRedactor.xaml
    /// </summary>
    public partial class Window_FilmRedactor : Window
    {
        Film filmObj;
        bool okPress = false;
            

        Window_FilmRedactor()
        {
            InitializeComponent();
        }

        internal static Film ShowDialog_Redactor(Film film)
        {
            var wd = new Window_FilmRedactor();
            wd.InitValues(film);
            wd.ShowDialog();

            return wd.okPress ? wd.filmObj.Clone() as Film : null;
        }

        internal static void ShowDialog_View(Film film)
        {
            var wd = new Window_FilmRedactor();
            wd.InitValues(film);
            wd.richTextBoxComment.IsReadOnly = true;
            wd.textBoxName.IsReadOnly = true;
            wd.textBoxAgeRate.IsReadOnly = true;
            wd.ShowDialog();
        }

        void InitValues(Film film)
        {
            filmObj = film.Clone() as Film;
            textBoxName.Text = filmObj.FilmName;
            textBoxAgeRate.Text = filmObj.AgeRate.ToString();
            richTextBoxComment.Document.Blocks.Clear();
            richTextBoxComment.AppendText(filmObj.Description);

        }

        /// <summary>
        /// Return true if valid values.
        /// </summary>
        bool GetValues()
        {
            try
            {
                filmObj.AgeRate = Convert.ToByte(textBoxAgeRate.Text.Trim());
            }
            catch
            {
      
                System.Windows.MessageBox.Show("Возврастной рейтинг - целое число!");
                return false;
            }
            filmObj.FilmName = textBoxName.Text.Trim();
            filmObj.Description = new TextRange(richTextBoxComment.Document.ContentStart, richTextBoxComment.Document.ContentEnd).Text.Trim();
            return true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (GetValues())
            {
                okPress = true;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            filmObj = null;
            Close();
        }
    }
}
