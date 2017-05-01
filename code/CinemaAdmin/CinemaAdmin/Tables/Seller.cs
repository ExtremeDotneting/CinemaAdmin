using System;

namespace CinemaAdmin.Tables
{
    class Seller : TableElement, ICloneable
    {
        public int Id;
        public string Login;
        public string Pass;
        public string Name;
        public int RulesLevel;

        public override int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            Seller res = new Seller();
            res.Id = Id;
            res.Login = Login;
            res.Pass = Pass;
            res.Name = Name;
            res.RulesLevel = RulesLevel;
            return res;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("(id={0} / login={1} / pass={2} / name={3} / rules_lvl={4})", 
                Id, Login, Pass, Name, RulesLevel).Replace("\n", " ").Replace("\r", " "); 
        }
    }
}
