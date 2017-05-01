using System;

namespace CinemaAdmin.Tables
{
    class Film : TableElement, ICloneable
    {
        public int Id;
        public string FilmName;
        public string Description;
        public int AgeRate;

        public override int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            Film res = new Film();
            res.Id = Id;
            res.FilmName = FilmName;
            res.Description = Description;
            res.AgeRate = AgeRate;
            return res;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("(id={0} / film_name={1} / description={2} / age_rate={3})",
                Id, FilmName, Description, AgeRate).Replace("\n", " ").Replace("\r", " "); 
        }
    }
}
