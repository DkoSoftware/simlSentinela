using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Timers;
using SIMLSentinela.Jobs;

namespace SIMLSentinela.Services
{
    partial class ClassificationClients : ServiceBase
    {
        public ClassificationClients()
        {
            InitializeComponent();
        }

        static Timer timer;
        private DateTime lasDayrun;

        protected override void OnStart(string[] args)
        {
            if (DateTime.Now.TimeOfDay.Hours == 5)
            {
                timer = new Timer();
                timer.Interval = 1000 * 60 * 60 * 24;//set interval of one day 
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                start_timer();
                Console.Read();
            }
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            new ClassificationClientsJob().ExecuteJob();

        }
        private static void start_timer()
        {
            timer.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
