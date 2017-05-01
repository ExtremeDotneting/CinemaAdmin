using CinemaAdmin.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace CinemaAdmin
{
    class DataBaseWorker
    {
        const string defConnectionString = "Database=cinema_admin;Data Source=localhost;User Id=root;Password=1111";
        MySqlConnection dbConnection;
        static DataBaseWorker instance;

        public static DataBaseWorker Instance {
            get
            {
                if (instance == null)
                    instance = new DataBaseWorker();
                return instance;
            }
        }

        public void InitConnection()
        {
            InitConnection(defConnectionString);
        }

        public void InitConnection(string connectionStr)
        {
            dbConnection = new MySqlConnection(connectionStr);
            dbConnection.Open();
        }

        public TableElement[] GetAllRecordsByType(Type typeOfTableElement)
        {
            TableElement[] listOfRecords = null;

            if (typeOfTableElement == typeof(Film))
            {
                listOfRecords = GetAllFilms();
            }
            else if (typeOfTableElement == typeof(Seance))
            {
                listOfRecords = GetAllSeances();
            }
            else if (typeOfTableElement == typeof(Seller))
            {
                listOfRecords = GetAllSellers();
            }
            else if (typeOfTableElement == typeof(CinemaRoom))
            {
                listOfRecords = GetAllCinemaRooms();
            }
            else if (typeOfTableElement == typeof(SoldTicket))
            {
                listOfRecords = GetAllTickets();
            }

            return listOfRecords;
        }

        public void AddRecord(TableElement newElement)
        {
            Type typeOfTableElement = newElement.GetType();
            if (typeOfTableElement == typeof(Film))
            {
                AddFilm(newElement as Film);
            }
            else if (typeOfTableElement == typeof(Seance))
            {
                AddSeance(newElement as Seance);
            }
            else if (typeOfTableElement == typeof(Seller))
            {
                AddSeller(newElement as Seller);
            }
            else if (typeOfTableElement == typeof(CinemaRoom))
            {
                AddCinemaRoom(newElement as CinemaRoom);
            }
            else if (typeOfTableElement == typeof(SoldTicket))
            {
                AddTicket(newElement as SoldTicket);
            }
        }

        public void UpdateRecord(TableElement newElement)
        {
            Type typeOfTableElement = newElement.GetType();
            if (typeOfTableElement == typeof(Film))
            {
                UpdateFilm(newElement as Film);
            }
            else if (typeOfTableElement == typeof(Seance))
            {
                UpdateSeance(newElement as Seance);
            }
            else if (typeOfTableElement == typeof(Seller))
            {
                UpdateSeller(newElement as Seller);
            }
            else if (typeOfTableElement == typeof(CinemaRoom))
            {
                UpdateCinemaRoom(newElement as CinemaRoom);
            }
            else if (typeOfTableElement == typeof(SoldTicket))
            {
                UpdateTicket(newElement as SoldTicket);
            }
        }

        public void RemoveRecord(TableElement newElement)
        {
            Type typeOfTableElement = newElement.GetType();
            if (typeOfTableElement == typeof(Film))
            {
                RemoveFilm(newElement as Film);
            }
            else if (typeOfTableElement == typeof(Seance))
            {
                RemoveSeance(newElement as Seance);
            }
            else if (typeOfTableElement == typeof(Seller))
            {
                RemoveSeller(newElement as Seller);
            }
            else if (typeOfTableElement == typeof(CinemaRoom))
            {
                RemoveCinemaRoom(newElement as CinemaRoom);
            }
            else if (typeOfTableElement == typeof(SoldTicket))
            {
                RemoveTicket(newElement as SoldTicket);
            }
        }


        public SoldTicket[] GetAllTicketsOnSeance(Seance seance)
        {
            string sql = string.Format("SELECT * FROM sold_tickets WHERE seance_id=\"{0}\" LIMIT 200;", seance.Id);
            var res = new List<SoldTicket>();
            foreach (var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToTicket(dict));
            }
            return res.ToArray();
        }

        public CinemaRoom[] GetAllCinemaRooms()
        {
            string sql = "SELECT * FROM cinema_rooms WHERE 1 LIMIT 200;";
            var res = new List<CinemaRoom>();
            foreach (var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToCinemaRoom(dict));
            }
            return res.ToArray();
        }

        public Film[] GetAllFilms()
        {
            string sql = "SELECT * FROM films WHERE 1 LIMIT 200;";
            var res = new List<Film>();
            foreach (var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToFilm(dict));
            }
            return res.ToArray();
        }

        public Seance[] GetAllSeances()
        {
            string sql = "SELECT * FROM seance_schedule WHERE 1 LIMIT 200;";
            var res = new List<Seance>();
            foreach (var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToSeance(dict));
            }
            return res.ToArray();
        }

        public Seller[] GetAllSellers()
        {
            string sql = "SELECT * FROM sellers WHERE 1 LIMIT 200;";
            var res = new List<Seller>();
            foreach (var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToSeller(dict));
            }
            return res.ToArray();
        }

        public SoldTicket[] GetAllTickets()
        {
            string sql = "SELECT * FROM sold_tickets WHERE 1 LIMIT 200;";
            var res = new List<SoldTicket>();
            foreach(var dict in ExSql_ResArray(sql))
            {
                res.Add(DictionaryToTicket(dict));
            }
            return res.ToArray();
        }

        public void AddTicket(SoldTicket val)
        {
            string sql = string.Format(
                "INSERT  INTO cinema_admin.sold_tickets(seance_id, seat_row, seat_column, sale_date, price, seller_id) " +
                "VALUES (\"{0}\", \"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");",
                val.Seance.Id,
                val.SeatRow,
                val.SeatColumn,
                val.SaleDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern),
                val.Price,
                val.Seller.Id
                );
             val.Id=ExSql_Insert(sql);
        }

        public void AddFilm(Film val)
        {
            string sql = string.Format(
                "INSERT  INTO cinema_admin.films(film_name,description,age_rate) VALUES (\"{0}\", \"{1}\",\"{2}\");",
                val.FilmName,
                val.Description,
                val.AgeRate
                );
            val.Id = ExSql_Insert(sql);
        }

        public void AddCinemaRoom(CinemaRoom val)
        {
            string sql = string.Format(
                "INSERT  INTO cinema_admin.cinema_rooms(room_name,room_scheme_object) VALUES (\"{0}\", \"{1}\");", 
                val.RoomName,
                val.RoomScheme
                );
            val.Id = ExSql_Insert(sql);
        }

        public void AddSeance(Seance val)
        {
            string sql = string.Format(
                "INSERT  INTO cinema_admin.seance_schedule(film_id,room_id,seance_date) VALUES (\"{0}\", \"{1}\",\"{2}\");",
                val.Film.Id,
                val.Room.Id,
                val.SeanceDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern)
                );
            val.Id = ExSql_Insert(sql);
        }

        public void AddSeller(Seller val)
        {
            string sql = string.Format(
                "INSERT  INTO cinema_admin.sellers(login,pass,name,rules_level) VALUES (\"{0}\", \"{1}\",\"{2}\",\"{3}\");",
                val.Login,
                val.Pass,
                val.Name,
                val.RulesLevel
                );
            val.Id = ExSql_Insert(sql);
        }

        public void UpdateTicket(SoldTicket val)
        {
            string sql = string.Format(
                "UPDATE cinema_admin.sold_tickets SET seance_id=\"{0}\", seat_row=\"{1}\", seat_column=\"{2}\", sale_date=\"{3}\", price=\"{4}\", " +
                "seller_id=\"{5}\" WHERE id=\"{6}\";",
                val.Seance.Id,
                val.SeatRow,
                val.SeatColumn,
                val.SaleDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern),
                val.Price,
                val.Seller.Id,
                val.GetId()
                );
        }
        
        public void UpdateFilm(Film val)
        {
            string sql = string.Format(
                "UPDATE cinema_admin.films SET film_name=\"{0}\",description=\"{1}\",age_rate=\"{2}\" WHERE id=\"{3}\";",
                val.FilmName,
                val.Description,
                val.AgeRate,
                val.GetId()
                );
            ExSql(sql);
        }

        public void UpdateCinemaRoom(CinemaRoom val)
        {
            string sql = string.Format(
                "UPDATE cinema_admin.cinema_rooms SET room_name=\"{0}\",room_scheme_object=\"{1}\" WHERE id=\"{2}\";",
                val.RoomName,
                val.RoomScheme,
                val.GetId()
                );
            ExSql(sql);
        }

        public void UpdateSeance(Seance val)
        {
            string sql = string.Format(
                "UPDATE cinema_admin.seance_schedule SET film_id=\"{0}\",room_id=\"{1}\",seance_date=\"{2}\" WHERE id=\"{3}\";",
                val.Film.Id,
                val.Room.Id,
                val.SeanceDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern),
                val.GetId()
                );
            ExSql(sql);
        }

        public void UpdateSeller(Seller val)
        {
            string sql = string.Format(
                "UPDATE cinema_admin.sellers SET login=\"{0}\",pass=\"{1}\",name=\"{2}\",rules_level=\"{3}\" WHERE id=\"{4}\";",
                val.Login,
                val.Pass,
                val.Name,
                val.RulesLevel,
                val.GetId()
                );
            ExSql(sql);
        }

        public void RemoveTicket(SoldTicket val)
        {
            string sql = string.Format("DELETE FROM sold_tickets WHERE id = \"{0}\" LIMIT 3;", val.GetId());
            ExSql(sql);
        }

        public void RemoveFilm(Film val)
        {
            string sql = string.Format("DELETE FROM films WHERE id = \"{0}\" LIMIT 3;", val.GetId());
            ExSql(sql);
        }

        public void RemoveCinemaRoom(CinemaRoom val)
        {
            string sql = string.Format("DELETE FROM cinema_rooms WHERE id = \"{0}\" LIMIT 3;", val.GetId());
            ExSql(sql);
        }

        public void RemoveSeance(Seance val)
        {
            string sql = string.Format("DELETE FROM seance_schedule WHERE id = \"{0}\" LIMIT 3;", val.GetId());
            ExSql(sql);
        }

        public void RemoveSeller(Seller val)
        {
            string sql = string.Format("DELETE FROM sellers WHERE id = \"{0}\" LIMIT 3;", val.GetId());
            ExSql(sql);
        }

        public void RemoveTicket(int row,int column)
        {
            string sql = string.Format("DELETE FROM sold_tickets WHERE seat_row=\"{0}\" AND seat_column=\"{1}\" LIMIT 3;", row,column);
            ExSql(sql);
        }

        public SoldTicket GetByIdTicket(int id)
        {
            string sql = string.Format("SELECT * FROM sold_tickets WHERE id=\"{0}\" LIMIT 3;", id);
            var dict = ExSql_ResRecord(sql);
            return DictionaryToTicket(dict);
        }

        public Film GetByIdFilm(int id)
        {
            string sql = string.Format("SELECT * FROM films WHERE id=\"{0}\" LIMIT 3;", id);
            var dict = ExSql_ResRecord(sql);
            return DictionaryToFilm(dict);
        }

        public CinemaRoom GetByIdCinemaRoom(int id)
        {
            string sql = string.Format("SELECT * FROM cinema_rooms WHERE id=\"{0}\" LIMIT 3;", id);
            var dict = ExSql_ResRecord(sql);
            return DictionaryToCinemaRoom(dict);
        }

        public Seance GetByIdSeance(int id)
        {
            string sql = string.Format("SELECT * FROM seance_schedule WHERE id=\"{0}\" LIMIT 3;", id);
            var dict = ExSql_ResRecord(sql);
            return DictionaryToSeance(dict);
        }

        public Seller GetByIdSeller(int id)
        {
            Seller seller = new Seller();
            seller.Id = id;
            seller.Login = "login";
            seller.Pass = "pass";
            seller.RulesLevel = 0;
            seller.Name = "Admin Seller";
            return seller;
            //string sql = "";
            //var dict = ExSql_ResRecord(sql);
            //return DictionaryToSeller(dict);
        }

        SoldTicket DictionaryToTicket(Dictionary<string, object> dict)
        {
            if (dict == null)
                return null;
            var res = new SoldTicket();
            res.Id = Convert.ToInt32(dict["id"]);
            res.Price = Convert.ToInt32(dict["price"]);
            res.SeatColumn = Convert.ToInt32(dict["seat_column"]);
            res.SeatRow = Convert.ToInt32(dict["seat_row"]);
            res.SaleDate = Convert.ToDateTime(dict["sale_date"]);
            res.Seance=GetByIdSeance(Convert.ToInt32(dict["seance_id"]));
            res.Seller = GetByIdSeller(Convert.ToInt32(dict["seller_id"]));
            return res;
        }

        Film DictionaryToFilm(Dictionary<string, object> dict)
        {
            if (dict == null)
                return null;
            var res = new Film();
            res.Id = Convert.ToInt32(dict["id"]);
            res.FilmName = Convert.ToString(dict["film_name"]);
            res.Description = Convert.ToString(dict["description"]);
            res.AgeRate = Convert.ToInt32(dict["age_rate"]);
            return res;
        }

        CinemaRoom DictionaryToCinemaRoom(Dictionary<string, object> dict)
        {
            if (dict == null)
                return null;
            var res = new CinemaRoom();
            res.Id = Convert.ToInt32(dict["id"]);
            res.RoomName = Convert.ToString(dict["room_name"]);
            res.RoomScheme = Convert.ToString(dict["room_scheme_object"]);
            return res;
        }

        Seance DictionaryToSeance(Dictionary<string, object> dict)
        {
            if (dict == null)
                return null;
            var res = new Seance();
            res.Id = Convert.ToInt32(dict["id"]);
            res.Room=GetByIdCinemaRoom(Convert.ToInt32(dict["room_id"]));
            res.Film = GetByIdFilm(Convert.ToInt32(dict["film_id"]));
            res.SeanceDate = Convert.ToDateTime(dict["seance_date"]);
            return res;
        }

        Seller DictionaryToSeller(Dictionary<string, object> dict)
        {
            return null;
        }

        int ExSql_Insert(string sql)
        {
            using (var cmd = new MySqlCommand(sql, dbConnection))
            {
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }

        void ExSql(string sql)
        {
            using (var cmd = new MySqlCommand(sql, dbConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        Dictionary<string, object> ExSql_ResRecord(string sql)
        {
            Dictionary<string, object> res ;
            using (var cmd = new MySqlCommand(sql, dbConnection))
            {
                var reader = cmd.ExecuteReader();
                reader.Read();
                res=Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                reader.Close();
            }
            return res;
        }

        List<Dictionary<string, object>> ExSql_ResArray(string sql)
        {
            List<Dictionary<string, object>>  res= new List<Dictionary<string, object>>();
            using (var cmd = new MySqlCommand(sql, dbConnection))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res.Add( Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                }
                reader.Close();
            }
            return res;
        }
    }
}
