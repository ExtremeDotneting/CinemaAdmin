using CinemaAdmin.Tables;
using System;
using System.Windows;
using System.Windows.Input;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_SeanceRedactor.xaml
    /// </summary>
    public partial class Window_SeanceRedactor : Window
    {
        bool okPress = false;
        public Window_SeanceRedactor()
        {
            InitializeComponent();
        }

        Seance objectToWork;

        internal static Seance ShowDialog_Redactor(Seance seance)
        {
            Window_SeanceRedactor wd = new Window_SeanceRedactor();
            wd.InitValues(seance);
            wd.ShowDialog();
            return wd.okPress ? wd.objectToWork.Clone() as Seance : null;
        }

        internal static void ShowDialog_View(Seance seance)
        {
            Window_SeanceRedactor wd = new Window_SeanceRedactor();
            wd.InitValues(seance);
            wd.labelEditCinemaRoom.IsEnabled = false;
            wd.labelEditFilm.IsEnabled = false;
            wd.dateTimePicker.IsEnabled = false;
            wd.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GetValues();
            if (objectToWork.Room == null || objectToWork.Film == null)
                MessageBox.Show("Не все значения указаны!");
            else
            {
                okPress = true;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            objectToWork = null;
            Close();
        }

        void InitValues(Seance seance)
        {
            objectToWork = seance.Clone() as Seance;
            dateTimePicker.Value = objectToWork.SeanceDate;
        }

        void GetValues()
        {
            objectToWork.SeanceDate = Convert.ToDateTime(dateTimePicker.Text);
        }

        private void labelEditFilm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(Film), DataBaseWorker.Instance);
            objectToWork.Film = wd.ShowDialogWithResult(objectToWork.Film) as Film;
        }

        private void labelEditCinemaRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(CinemaRoom), DataBaseWorker.Instance);
            objectToWork.Room = wd.ShowDialogWithResult(objectToWork.Room) as CinemaRoom;
        }
    }
}
