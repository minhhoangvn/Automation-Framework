﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFrameWork.Reporter.Screenshots
{
    public class Screenshot
    {
        public Screenshot ()
        {
            var now = DateTime.Now;
            Name = ScreenshotTaker.GetScreenName(now);
            Date = now;
        }

        public Screenshot (DateTime date)
        {
            Name = ScreenshotTaker.GetScreenName(date);
            Date = date;
        }

        public string Name
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }
    }
}

