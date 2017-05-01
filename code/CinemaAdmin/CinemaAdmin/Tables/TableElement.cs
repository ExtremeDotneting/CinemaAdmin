using System;

namespace CinemaAdmin.Tables
{
    abstract class TableElement:IFormattable
    {
        public abstract int GetId();

        /// <summary>
        /// Create new object.
        /// </summary>
        public static TableElement CreateInRedactor(Type typeOfTableElement)
        {
            return OpenInRedactor(null, true, typeOfTableElement);
        }

        public void ViewInRedactor()
        {

            OpenInRedactor(this, false);
        }

        public TableElement EditInRedactor()
        {
            return OpenInRedactor(this, true);
        }

        static TableElement OpenInRedactor(TableElement tableEl, bool canEdit, Type typeOfTableElement = null)
        {
            if (tableEl!=null)
                typeOfTableElement = tableEl.GetType();

            if (typeOfTableElement == typeof(Film))
            {
                if (tableEl == null)
                    tableEl = new Film();
                if (canEdit)
                    return Window_FilmRedactor.ShowDialog_Redactor(tableEl as Film);
                Window_FilmRedactor.ShowDialog_View(tableEl as Film);
            }
            else if (typeOfTableElement == typeof(Seance))
            {
                if (tableEl == null)
                    tableEl = new Seance();
                if (canEdit)
                    return Window_SeanceRedactor.ShowDialog_Redactor(tableEl as Seance);
                Window_SeanceRedactor.ShowDialog_View(tableEl as Seance);
            }
            else if (typeOfTableElement == typeof(Seller))
            {
                if (tableEl == null)
                    tableEl = new Seller();
            }
            else if (typeOfTableElement == typeof(CinemaRoom))
            {
                if (tableEl == null)
                    tableEl = new CinemaRoom();
                if (canEdit)
                    return Window_CinemaRoomRedactor.ShowDialog_Redactor(tableEl as CinemaRoom);
                Window_CinemaRoomRedactor.ShowDialog_View(tableEl as CinemaRoom);
            }
            else if (typeOfTableElement == typeof(SoldTicket))
            {
                if (tableEl == null)
                    tableEl = new SoldTicket();
            }

            return null;
        }

        public abstract string ToString(string format, IFormatProvider formatProvider);
    }
}
