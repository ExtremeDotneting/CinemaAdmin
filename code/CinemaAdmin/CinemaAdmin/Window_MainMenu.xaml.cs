using CinemaAdmin.Tables;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_MainMenu.xaml
    /// </summary>
    public partial class Window_MainMenu : Window
    {
        public Window_MainMenu()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
            string connectionSetting = File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "mysql_connection.txt"));
            DataBaseWorker.Instance.InitConnection(connectionSetting);
            image.Source = LoadImage(System.IO.Path.Combine(Environment.CurrentDirectory,"logo.png"));
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            MessageBox.Show("Неизвестная ошибка!\n\n" + ex.Message);
            Environment.Exit(0);
        }

        public static BitmapImage LoadImage(string path)
        {
            Uri imageAbsolutePath = new Uri(path);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = imageAbsolutePath;
            image.EndInit();
            return image;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FilmsClick();
        }

        void FilmsClick()
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(Film), DataBaseWorker.Instance);
            wd.ShowDialogWithResult(-1);
        }

        void CinemaRoomsClick()
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(CinemaRoom), DataBaseWorker.Instance);
            wd.ShowDialogWithResult(-1);
        }

        void SoldClick()
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(Seance), DataBaseWorker.Instance, true);
            Seance dialogRes =wd.ShowDialogWithResult(-1) as Seance;
            if (dialogRes == null)
                return;
            Window_CinemaRoomViewer crv = new Window_CinemaRoomViewer(DataBaseWorker.Instance, dialogRes);
            crv.ShowDialog_Redactor();
        }

        void SeancesClick()
        {
            Window_ChoseElementWindow wd = new Window_ChoseElementWindow(typeof(Seance), DataBaseWorker.Instance);
            wd.ShowDialogWithResult(-1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SoldClick();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SeancesClick();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            CinemaRoomsClick();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Поддержка многопользовательского режима будет в следующей версии!" +
                "\nО авторе" +
                "\n    Telegram - @yura_mysko" +
                "\n    Vk - https://vk.com/yura_mysko" +
                "\n    Github - https://github.com/KogerCoder";
            MessageBox.Show(msg);
        }
    }
}
