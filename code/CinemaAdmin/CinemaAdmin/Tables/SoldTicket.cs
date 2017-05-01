using System;

namespace CinemaAdmin.Tables
{
    class SoldTicket : TableElement, ICloneable
    {
        public int Id;
        public int SeatRow;
        public int SeatColumn;
        public DateTime SaleDate;
        public int Price;
        public Seance Seance;
        public Seller Seller;

        public object Clone()
        {
            SoldTicket res = new SoldTicket();
            res.Id = Id;
            res.SeatRow = SeatRow;
            res.SeatColumn = SeatColumn;
            res.SaleDate = SaleDate;
            res.Price = Price;
            res.Seance = Seance;
            res.Seller = Seller;
            return res;
        }

        public override int GetId()
        {
            return Id;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("(id={0} / seat_row={1} / seat_column={2} / sale_date={3} / price={4})",
                Id, SeatRow, SeatColumn, SaleDate, Price).Replace("\n", " ").Replace("\r", " ");
        }
    }
}
