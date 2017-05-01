using System;

namespace CinemaAdmin.Tables
{
    class Seance : TableElement, ICloneable 
    {
        public int Id;
        public Film Film;
        public CinemaRoom Room;
        public DateTime SeanceDate;

        public override int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            Seance res = new Seance();
            res.Id = Id;
            res.Film = Film;
            res.Room = Room;
            res.SeanceDate = SeanceDate;
            return res;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("(id={0} / film={1} / seance_date={2})", Id, Film, SeanceDate).Replace("\n", " ").Replace("\r", " ");
        }
    }
}
