using CinemaAdmin.Tables;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_ChoseElementWindow.xaml
    /// </summary>
    public partial class Window_ChoseElementWindow : Window
    {
        bool okPress = false;
        TableElement[] allItems;
        TableElement dialogRes;
        Type tableType;
        DataBaseWorker dataBaseWorker;

        internal Window_ChoseElementWindow(Type tableType, DataBaseWorker dbWorker, bool chooseOnly = false)
        {
            InitializeComponent();
            this.tableType = tableType;
            dataBaseWorker = dbWorker;
            allItems = dataBaseWorker.GetAllRecordsByType(tableType);
            if (chooseOnly)
            {
                buttonAdd.Visibility = Visibility.Hidden;
                buttonDelete.Visibility = Visibility.Hidden;
                buttonEdit.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Return choosed element.
        /// </summary>
        internal TableElement ShowDialogWithResult(int selectedIndex = -1)
        {
            InitItems(selectedIndex);
            ShowDialog();
            return dialogRes;
        }

        /// <summary>
        /// Return choosed element.
        /// </summary>
        internal TableElement ShowDialogWithResult(TableElement selectedElement)
        {
            int selectedIndex = -1;
            if (selectedElement != null)
            {
                for (int i = 0; i < allItems.Length; i++)
                {
                    if (allItems[i].GetId() == selectedElement.GetId())
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }
            InitItems(selectedIndex);
            ShowDialog();
            return okPress? dialogRes : null;
        }


        void InitItems(int selectedIndex)
        {
            TableElement[] items = allItems;;
            listBox.Items.Clear();
            for(int i = 0; i < items.Length; i++)
            {
                var ltbItem = new ListBoxItem();
                ltbItem.Content = items[i];
                listBox.Items.Add(Convert.ToString(items[i]));
            }
            if (selectedIndex < items.Length)
                listBox.SelectedIndex= selectedIndex;
        }

        void AddRecord()
        {
            TableElement element=TableElement.CreateInRedactor(tableType);
            if (element == null)
                return;
            dataBaseWorker.AddRecord(element);
            allItems=dataBaseWorker.GetAllRecordsByType(tableType);
            InitItems(allItems.Length-1);
        }

        void UpdateRecord()
        {
            if (listBox.SelectedIndex<0)
                return;
            TableElement element = allItems[listBox.SelectedIndex];
            element=element.EditInRedactor();
            if (element == null)
                return;
            dataBaseWorker.UpdateRecord(element);
            allItems = dataBaseWorker.GetAllRecordsByType(tableType);
            InitItems(listBox.SelectedIndex);
        }

        void ViewRecord()
        {
            if (listBox.SelectedIndex < 0)
                return;
            TableElement element = allItems[listBox.SelectedIndex];
            element.ViewInRedactor();
        }

        void DeleteRecord()
        {
            if (listBox.SelectedIndex < 0)
                return;
            TableElement element = allItems[listBox.SelectedIndex];
            dataBaseWorker.RemoveRecord(element);
            allItems = dataBaseWorker.GetAllRecordsByType(tableType);
            InitItems(-1);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dialogRes = allItems[listBox.SelectedIndex];
            }
            catch
            {
                dialogRes = null;
            }
            okPress = true;
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            dialogRes = null;
            Close();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            ViewRecord();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            UpdateRecord();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRecord();
        }
    }
}
