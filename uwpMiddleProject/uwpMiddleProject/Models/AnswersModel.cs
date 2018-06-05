using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace uwpMiddleProject.Models
{
    class AnswersModel
    {
        private string private_id;
        public string id { get { return this.private_id; } set { this.private_id = value; } }
        public string answerTo1;
        public string answerTo2;
        public string answerTo3;
        public string answerTo4;
        public string answerTo5;
        public string answerTo6;
        public string answerTo7;
        public string answerTo8;
        public string answerTo9;
        public string answerTo10;
        public int score;
        public DateTimeOffset date;
        public Uri imauri;
        public BitmapImage coverImage;

        public AnswersModel()
        {
            this.private_id = Guid.NewGuid().ToString();
            this.id = private_id;
            this.answerTo1 = "";
            this.answerTo2 = "";
            this.answerTo3 = "";
            this.answerTo4 = "";
            this.answerTo5 = "";
            this.answerTo6 = "";
            this.answerTo7 = "";
            this.answerTo8 = "";
            this.answerTo9 = "";
            this.answerTo10 = "";
            this.score = 0;
            this.date = DateTimeOffset.Now;
            this.imauri = new Uri("ms-appx:///Assets/cup.png");
            this.coverImage = new BitmapImage(new Uri("ms-appx:///Assets/cup.png"));
        }

        public AnswersModel(string id, int score, string answerTo1, string answerTo2, string answerTo3, string answerTo4, string answerTo5, string answerTo6, string answerTo7, string answerTo8, string answerTo9, string answerTo10, DateTimeOffset date, Uri imauri, BitmapImage coverImage)
        {
            this.private_id = id;
            this.id = private_id;
            this.answerTo1 = answerTo1;
            this.answerTo2 = answerTo2;
            this.answerTo3 = answerTo3;
            this.answerTo4 = answerTo4;
            this.answerTo5 = answerTo5;
            this.answerTo6 = answerTo6;
            this.answerTo7 = answerTo7;
            this.answerTo8 = answerTo8;
            this.answerTo9 = answerTo9;
            this.answerTo10 = answerTo10;
            this.score = score;
            this.date = date;
            this.imauri = imauri;
            this.coverImage = coverImage;
        }

        public static string dateTimeToString(DateTimeOffset date)
        {
            return date.ToString();
        }
        public static DateTimeOffset stringToDateTime(string datestring)
        {
            return DateTimeOffset.Parse(datestring);
        }

        public static Uri stringToUri(string uristr)
        {
            return new Uri(uristr);
        }

        public static string uriToString(Uri imageUri)
        {
            return imageUri.ToString();
        }
    }
}
