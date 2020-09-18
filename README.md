# How to bind JSON online data to Xamarin.Forms Schedule (SfSchedule) 

You can bind data from [JSON](http://www.json.org/) to the Xamarin forms [SfSchedule](https://help.syncfusion.com/xamarin/scheduler/getting-started) appointments.

You can also refer the following article.

https://www.syncfusion.com/kb/11931/how-to-bind-json-online-data-to-xamarin-forms-schedule-sfschedule

**STEP 1:** Create a JSON data model

``` C#
public class JSONData
{
    public string Subject { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}
```
**STEP 2:** Get the online JSON data with the help of [GetStringAsync](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=netcore-3.1#System_Net_Http_HttpClient_GetStringAsync_System_Uri_) method and [Deserialize](https://www.newtonsoft.com/json/help/html/DeserializeObject.htm) the JSON data as list of JSON data mode and then Add the JSON data list into the appointment collection ( **Meetings** ).

``` C#
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
```
**STEP 3:** Bind appointments that collected through **JSON** online it to a schedule using the [SfSchedule.DataSource](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_DataSource) property.

``` xml
<schedule:SfSchedule x:Name="Schedule"
                    DataSource="{Binding Meetings}"
                    ScheduleView="MonthView" MoveToDate="{Binding dateTime}" ShowAppointmentsInline="True"
                    >
    <schedule:SfSchedule.AppointmentMapping>
        <schedule:ScheduleAppointmentMapping
        EndTimeMapping="To"
        StartTimeMapping="From"
        SubjectMapping="EventName"
        ColorMapping="Color"
        />
    </schedule:SfSchedule.AppointmentMapping>
    <schedule:SfSchedule.BindingContext>
        <local:SchedulerViewModel/>
    </schedule:SfSchedule.BindingContext>
</schedule:SfSchedule>
```

**Output**

![JSONSchedule](https://github.com/SyncfusionExamples/appointment-json-online-schedule-xamarin/blob/master/ScreenShot/JSONSchedule.gif)
