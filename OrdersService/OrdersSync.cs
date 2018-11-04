using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace OrdersService
{
    public partial class OrdersSync : ServiceBase
    {
        DateTime lastRunDate = DateTime.Today;
        private Timer Schedular;
        //public string connAzure = ConfigurationManager.AppSettings["ConnStringAzure"];
        //public string connPublic = ConfigurationManager.AppSettings["ConnString"];

        public OrdersSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Orders Service started {0}");
            ScheduleService();
        }


        protected override void OnStop()
        {
            WriteToFile("Orders Service stopped {0}");
            Schedular.Dispose();
        }


        private void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                WriteToFile("Orders Service Mode: " + mode + " {0}");

                DateTime scheduledTime = DateTime.MinValue;

                if (mode.ToUpper() == "INTERVAL")
                {
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                SyncOrders();

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                WriteToFile("Orders Service scheduled to run after: " + schedule + " {0}");

                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                Schedular.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                WriteToFile("Orders Service Error on: {0} " + ex.Message + ex.StackTrace);

                using (ServiceController serviceController = new ServiceController("OrdersService"))
                {
                    serviceController.Stop();
                }
            }
        }

        public void SyncOrders()
        {
            HiEffAPI hieffapi = new HiEffAPI();
            List<Order> orders = hieffapi.GetOrders();

        }

        private void SchedularCallback(object e)
        {
            WriteToFile("Fuels Service Log: {0}");
            ScheduleService();
        }

        private void WriteToFile(string text)
        {
            string path = ConfigurationManager.AppSettings["LogPath"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}
