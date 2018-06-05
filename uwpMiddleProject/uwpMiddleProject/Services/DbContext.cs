using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace uwpMiddleProject.Services
{
    class DbContext
    {
        private static String DB_NAME = "SQLiteRecord.db";
        private static String TABLE_NAME = "RecordTable";
        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Key STRING PRIMARY KEY NOT NULL,Score INT NOT NULL,Answer1 TEXT,Answer2 TEXT,Answer3 TEXT,Answer4 TEXT,Answer5 TEXT,Answer6 TEXT,Answer7 TEXT,Answer8 TEXT,Answer9 TEXT,Answer10 TEXT,Date VARCHAR(140),Image VARCHAR(350));";
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + "(Key, Score, Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Date, Image) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        private static string SQL_AllITEMS = "SELECT Key, Score, Answer1, Answer2, Answer3, Answer4, Answer5, Answer6, Answer7, Answer8, Answer9, Answer10, Date, Image FROM " + TABLE_NAME;
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";

        public DbContext()
        {
            var conn = new SQLiteConnection(DB_NAME);
            using (var statement = conn.Prepare(SQL_CREATE_TABLE))
            {
                statement.Step();
            }
        }

        public static ObservableCollection<Models.AnswersModel> getAllRecord()
        {
            ObservableCollection<Models.AnswersModel> todoItemList = new ObservableCollection<Models.AnswersModel>();
            var con = new SQLiteConnection(DB_NAME);
            var statement = con.Prepare(SQL_AllITEMS);
            while (statement.Step() == SQLiteResult.ROW)
            {
                todoItemList.Add(new Models.AnswersModel((string)statement[0], (int)Convert.ToInt32(statement[1]), (string)statement[2], (string)statement[3], (string)statement[4], (string)statement[5], (string)statement[6], (string)statement[7], (string)statement[8], (string)statement[9], (string)statement[10], (string)statement[11], Models.AnswersModel.stringToDateTime((string)statement[12]),
                                                      Models.AnswersModel.stringToUri((string)statement[13]),new BitmapImage(new Uri((string)statement[13])) ));
            }
            return todoItemList;
        }

        public static bool InsertData(string key, int score, string answer1, string answer2, string answer3, string answer4, string answer5, string answer6, string answer7, string answer8, string answer9, string answer10, DateTimeOffset date, Uri imauri)
        {
            var conn = new SQLiteConnection(DB_NAME);
            try
            {
                using (var todo = conn.Prepare(SQL_INSERT))
                {
                    todo.Bind(1, key);
                    todo.Bind(2, score);
                    todo.Bind(3, answer1);
                    todo.Bind(4, answer2);
                    todo.Bind(5, answer3);
                    todo.Bind(6, answer4);
                    todo.Bind(7, answer5);
                    todo.Bind(8, answer6);
                    todo.Bind(9, answer7);
                    todo.Bind(10, answer8);
                    todo.Bind(11, answer9);
                    todo.Bind(12, answer10);
                    todo.Bind(13, Models.AnswersModel.dateTimeToString(date));
                    todo.Bind(14, Models.AnswersModel.uriToString(imauri));
                    todo.Step();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static void DeleteData(string key)
        {
            var connection = new SQLiteConnection(DB_NAME);
            using (var todo = connection.Prepare(SQL_DELETE))
            {
                todo.Bind(1, key);
                todo.Step();
            }
        }


    }
}
