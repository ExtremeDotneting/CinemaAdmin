using CinemaAdmin.Tables;
using System.Windows;
using System.Windows.Documents;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_CinemaRoomRedactor.xaml
    /// </summary>
    public partial class Window_CinemaRoomRedactor : Window
    {
        bool okPress = false;
        CinemaRoom roomObject;

        internal static CinemaRoom ShowDialog_Redactor(CinemaRoom room)
        {
            Window_CinemaRoomRedactor wstr = new Window_CinemaRoomRedactor();
            wstr.InitValues(room);
            wstr.ShowDialog();
            return wstr.okPress ? wstr.roomObject.Clone() as CinemaRoom : null;
        }

        internal static void ShowDialog_View(CinemaRoom room)
        {
            Window_CinemaRoomRedactor wstr = new Window_CinemaRoomRedactor();
            wstr.InitValues(room);
            wstr.richTextBoxScheme.IsReadOnly = true;
            wstr.textBoxName.IsReadOnly = true;
            wstr.ShowDialog();
        }

        Window_CinemaRoomRedactor()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GetValues();
            okPress = true;
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            roomObject = null;
            Close();
        }

        void InitValues(CinemaRoom room)
        {
            roomObject = room.Clone() as CinemaRoom;
            textBoxName.Text = roomObject.RoomName;
            richTextBoxScheme.Document.Blocks.Clear();
            richTextBoxScheme.AppendText(roomObject.RoomScheme);

        }

        void GetValues()
        {
            roomObject.RoomName = textBoxName.Text.Trim();
            roomObject.RoomScheme = new TextRange(richTextBoxScheme.Document.ContentStart, richTextBoxScheme.Document.ContentEnd).Text.Trim();
        }

       
    }
}
