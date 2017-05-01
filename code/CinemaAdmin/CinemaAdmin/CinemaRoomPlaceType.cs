namespace CinemaAdmin
{
    public enum CinemaRoomPlaceType
    {
        Default,
        Middle,
        Vip,
        NotExists
    }

    public class CinemaRoomPlaceTypeAndInfo
    {
        const char CharDefault = '1';
        const char CharMiddle = '2';
        const char CharVip = '3';
        const char CharNotExists = '-';

        public CinemaRoomPlaceType PlaceType= CinemaRoomPlaceType.NotExists;
        public bool IsUsed=false;

        public static CinemaRoomPlaceTypeAndInfo[,] SchemeFromString(string scheme)
        {
            string[] schemeArr = scheme.Trim().Split('\n');
            int w = 0, h = schemeArr.Length;
            for (int i = 0; i < schemeArr.Length; i++)
            {
                if (schemeArr[i].Length > w)
                    w = schemeArr[i].Length;
            }
            CinemaRoomPlaceTypeAndInfo[,] res = new CinemaRoomPlaceTypeAndInfo[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    res[i, j] = new CinemaRoomPlaceTypeAndInfo();
                }
            }

            for (int i = 0; i < schemeArr.Length; i++)
            {
                for (int j = 0; j < schemeArr[i].Length; j++)
                {
                    res[j, i].PlaceType = FromChar(schemeArr[i][j]);
                }
            }
            return res;
        }
        public static string SchemeToString(CinemaRoomPlaceTypeAndInfo[,] scheme)
        {
            string res = "";
            for (int i = 0; i < scheme.GetLength(0); i++)
            {
                for (int j = 0; j < scheme.GetLength(1); j++)
                {
                    res+=scheme[i, j].ToChar().ToString();
                }
            }

            return res;
        }
        public static CinemaRoomPlaceType FromChar(char c)
        {
            CinemaRoomPlaceType? res = null;
            switch (c)
            {
                case CharDefault:
                    res = CinemaRoomPlaceType.Default;
                    break;
                case CharMiddle:
                    res = CinemaRoomPlaceType.Middle;
                    break;
                case CharVip:
                    res = CinemaRoomPlaceType.Vip;
                    break;
                case CharNotExists:
                    res = CinemaRoomPlaceType.NotExists;
                    break;
            }
            if (res == null)
                //throw new Exception("Unknown symbol!");
                res = CinemaRoomPlaceType.NotExists;
            return (CinemaRoomPlaceType)res;
        }

        public char ToChar()
        {
            char res = CharNotExists;
            switch (this.PlaceType)
            {
                case CinemaRoomPlaceType.Default:
                    res = CharDefault ;
                    break;
                case CinemaRoomPlaceType.Middle:
                    res = CharMiddle;
                    break;
                case CinemaRoomPlaceType.Vip:
                    res = CharVip;
                    break;
                case CinemaRoomPlaceType.NotExists :
                    res = CharNotExists;
                    break;
            }
            return res;
        }
    }
}
