﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers.Interfaces
{
    public interface ICannedDataParser
    {
        List<Period> ParseData(string data);
    }
}
