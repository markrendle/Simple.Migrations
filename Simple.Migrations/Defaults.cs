namespace Simple.Migrations
{
    public class Defaults
    {
        private static int _stringLength = 50;
        public static int StringLength
        {
            get { return _stringLength; }
            set { _stringLength = value; }
        }
    }
}