using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace ScheduleXamarin
{
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// collecions for meetings.
        /// </summary>
        private ObservableCollection<Meeting> meetings;
        public DateTime dateTime { get; set; }
        public SchedulerViewModel()
        {
            jsonDataCollection  = new List<JSONData>();
            GetInformation();
            dateTime = new DateTime(2017, 06, 01);
        }
        private async void GetInformation()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://js.syncfusion.com/demos/ejservices/api/Schedule/LoadData");
            jsonDataCollection = JsonConvert.DeserializeObject<List<JSONData>>(response);
            this.Meetings = new ObservableCollection<Meeting>();
            foreach (var data in jsonDataCollection)
            {
                Meetings.Add(new Meeting()
                {
                    EventName = data.Subject,
                    From = Convert.ToDateTime(data.StartTime),
                    To = Convert.ToDateTime(data.EndTime),
                    Color = Color.Red
                });
            }
        }
        private List<JSONData> jsonDataCollection;
        /// <summary>
        /// Gets or sets meetings.
        /// </summary>
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return this.meetings;
            }
            set
            {
                this.meetings = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Invoke method when property changed.
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
