using System;

namespace CinemaAdmin.Tables
{
    class CinemaRoom : TableElement,ICloneable
    {
        public int Id=-1;
        public string RoomName="";
        public string RoomScheme="";

        public override int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            CinemaRoom res = new CinemaRoom();
            res.Id = Id;
            res.RoomName = RoomName;
            res.RoomScheme = RoomScheme;
            return res;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("(id={0} / room_name={1})", Id, RoomName).Replace("\n", " ").Replace("\r", " ");
        }
    }
}
