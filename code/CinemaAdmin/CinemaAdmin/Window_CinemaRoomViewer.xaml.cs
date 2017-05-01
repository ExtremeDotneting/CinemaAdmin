using CinemaAdmin.Tables;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaAdmin
{
    /// <summary>
    /// Логика взаимодействия для Window_CinemaRoomViewer.xaml
    /// </summary>
    public partial class Window_CinemaRoomViewer : Window
    {
        DataBaseWorker dataBaseWorker;
        Seance currentSeance;

        CinemaRoomPlaceTypeAndInfo[,] cinemaRoomScheme;
        Button[,] placeButtonsArr;
        public int RoomWidth { get; private set; } = 0;
        public int RoomHeight { get; private set; } = 0;
        double squareWidth, squareHeight;
        double indentFromForm = 50, identBetwenPercent=0.2;

        internal void ShowDialog_Redactor()
        {
            ShowDialog();
        }
        
        void OnPlaceClick(Button sender, CinemaRoomPlaceTypeAndInfo place, int x, int y)
        {
            if (place.IsUsed)
                UnsoldNewTicket(x, y);
            else
                SoldNewTicket(x, y);
        }

        internal Window_CinemaRoomViewer(DataBaseWorker dbWorker,Seance seance)
        {
            dataBaseWorker = dbWorker;
            currentSeance = seance;
            Title += " - " + seance.Room.ToString();

            cinemaRoomScheme =CinemaRoomPlaceTypeAndInfo.SchemeFromString(currentSeance.Room.RoomScheme);
            InitializeComponent();
            BuildSchemeOnForm(cinemaRoomScheme);
            SoldTicket[] st = dataBaseWorker.GetAllTicketsOnSeance(currentSeance);
            HandleSoldTickets(st);
            UpdateRoom();
        }

        void HandleSoldTickets(SoldTicket[] soldTickets)
        {
            if (soldTickets == null)
                return;
            for(int i =0; i < soldTickets.Length; i++)
            {
                int row=soldTickets[i].SeatRow;
                int column = soldTickets[i].SeatColumn;
                try
                {
                    cinemaRoomScheme[row, column].IsUsed = true;
                }
                catch { }
            }
        }

        void SoldNewTicket(int x, int y)
        {

            cinemaRoomScheme[x, y].IsUsed = true;
            SoldTicket st = new SoldTicket();
            st.Price = 10;
            st.SaleDate = DateTime.Now;
            st.SeatRow = x;
            st.SeatColumn = y;
            st.Seller = dataBaseWorker.GetByIdSeller(1);
            st.Seance = currentSeance;
            dataBaseWorker.AddTicket(st);
            UpdateRoom();
        }

        void UnsoldNewTicket(int x, int y)
        {
            dataBaseWorker.RemoveTicket(x,y);
            cinemaRoomScheme[x, y].IsUsed = false;
            UpdateRoom();
        }

        void BuildSchemeOnForm(CinemaRoomPlaceTypeAndInfo[,] cinemaRoomScheme)
        {
            RoomWidth = cinemaRoomScheme.GetLength(0);
            RoomHeight = cinemaRoomScheme.GetLength(1);
            placeButtonsArr = new Button[RoomWidth, RoomHeight];
            AddButtons();

            this.StateChanged += delegate
            {
                UpdateRoom();
            };
            grid.SizeChanged += delegate
            {
                UpdateRoom();
            };
        }

        void UpdateRoom()
        {
            try
            {
                CalcSquare();
                for (int i = 0; i < cinemaRoomScheme.GetLength(0); i++)
                    for (int j = 0; j < cinemaRoomScheme.GetLength(1); j++)
                        UpdateButton(i, j);
            }
            catch { }
        }

        void CalcSquare()
        {
            squareWidth = ((ActualWidth - indentFromForm * 2) / RoomWidth)*(1- identBetwenPercent);
            squareHeight =( (ActualHeight - indentFromForm * 2) / RoomHeight)*(1 - identBetwenPercent);
        }

        void AddButtons()
        {
            for (int i = 0; i < cinemaRoomScheme.GetLength(0); i++)
            {
                for (int j = 0; j < cinemaRoomScheme.GetLength(1); j++)
                {
                    AddButtonToForm(cinemaRoomScheme[i, j] ,i,j);
                }
            }
        }

        void UpdateButton(int x, int y)
        {
            Button res = placeButtonsArr[x, y];
            if (res == null)
                return;
            switch (cinemaRoomScheme[x,y].PlaceType)
            {
                case CinemaRoomPlaceType.Default:
                    res.Background = Brushes.LightBlue;
                    break;
                case CinemaRoomPlaceType.Middle:
                    res.Background = Brushes.LightGreen;
                    break;
                case CinemaRoomPlaceType.Vip:
                    res.Background = Brushes.Red;
                    break;
            }
            res.Content = cinemaRoomScheme[x, y].IsUsed ? "$" : "";
            res.MaxWidth = squareWidth;
            res.MaxHeight = squareHeight;
            res.Width = squareWidth;
            res.Height = squareHeight;
            res.VerticalAlignment = VerticalAlignment.Top;
            res.HorizontalAlignment = HorizontalAlignment.Left;

            double posX = indentFromForm + (identBetwenPercent * squareWidth + squareWidth) * x;
            double posY = indentFromForm + (identBetwenPercent * squareHeight + squareHeight) * y;
            res.Margin = new Thickness(posX, posY, 0, 0);
            
        }

        void AddButtonToForm(CinemaRoomPlaceTypeAndInfo place, int x, int y)
        {
            if (place.PlaceType == CinemaRoomPlaceType.NotExists)
                return;
            Button res = new Button();
            placeButtonsArr[x, y] = res;
            grid.Children.Add(res);
            res.Click += delegate
            {
                OnPlaceClick(res, place, x, y);
            };
        }

        
    }
}
