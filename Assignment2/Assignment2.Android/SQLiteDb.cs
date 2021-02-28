using System.IO;

using Assignment2.Droid;
using Assignment2.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteDb))]
namespace Assignment2.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }

    }
}