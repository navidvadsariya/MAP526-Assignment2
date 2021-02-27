using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Assignment2.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
