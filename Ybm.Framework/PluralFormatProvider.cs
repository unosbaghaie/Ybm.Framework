﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework
{
    public class PluralizeHelper
    {


        public  string Pluralize(string value, int count)
        {
            if (count == 1)
            {
                return value;
            }
            return PluralizationService
                .CreateService(new CultureInfo("en-US"))
                .Pluralize(value);
        }

    }
}
