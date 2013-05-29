namespace Simple.Migrations
{
    public static class Table
    {
        public static Table<T> For<T>()
        {
            return new Table<T>();
        }
    }
}