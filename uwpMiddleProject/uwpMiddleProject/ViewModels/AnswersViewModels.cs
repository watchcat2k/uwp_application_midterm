using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using Windows.UI.Xaml.Media.Imaging;

namespace uwpMiddleProject.ViewModels
{
    class AnswersViewModels
    {
        private static AnswersViewModels instance;

        private ObservableCollection<Models.AnswersModel> allRecords = new ObservableCollection<Models.AnswersModel>();
        public ObservableCollection<Models.AnswersModel> AllRecords { get { return this.allRecords; } }

        private Models.AnswersModel selectedRecord = null;
        public Models.AnswersModel SelectedRecord
       {
            get
            {
                return selectedRecord;
            }
            set
            {
                this.selectedRecord = value;
            }
        }

        private AnswersViewModels()
        {
            this.allRecords = Services.DbContext.getAllRecord();
            /*
            Models.AnswersModel initRecord = new Models.AnswersModel();
            initRecord.score = 80;
            initRecord.date = DateTimeOffset.Now;
            this.allRecords.Add(initRecord);
            */
        }

        public static AnswersViewModels GetInstance()
        {
            if (instance == null)
            {
                instance = new AnswersViewModels();
            }
            return instance;
        }

        public void AddRecord(Models.AnswersModel temp)
        {
            this.allRecords.Add(temp);
            Services.DbContext.InsertData(temp.id, temp.score, temp.answerTo1, temp.answerTo2, temp.answerTo3, temp.answerTo4, temp.answerTo5, temp.answerTo6, temp.answerTo7, temp.answerTo8, temp.answerTo9, temp.answerTo10,temp.date,temp.imauri);

        }
        /*
        public void AddRecord(string id, string answerTo1, string answerTo2, string answerTo3, string answerTo4, string answerTo5, string answerTo6, string answerTo7, string answerTo8, string answerTo9, string answerTo10, int score, DateTimeOffset date, BitmapImage coverImage)
        {
            Models.AnswersModel temp = new Models.AnswersModel(id, score, answerTo1, answerTo2, answerTo3, answerTo4, answerTo5, answerTo6, answerTo7, answerTo8, answerTo9, answerTo10, date, coverImage);
            this.allRecords.Add(temp);
        }*/

        public void RemoveRecord()
        {
            this.allRecords.Remove(this.selectedRecord);
            Services.DbContext.DeleteData(this.selectedRecord.id);
            this.selectedRecord = null;
        }

    }
}
