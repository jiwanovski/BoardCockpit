using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCockpit.Helpers
{
    interface IChart
    {
        Highcharts Chart
        {
            get;
            set;
        }
        List<string> Categories
        {
            get;
            set;
        }
        List<Series> DataSeries
        {
            get;
            set;
        }
        List<YAxis> YAxisSeries
        {
            get;
            set;
        }
        string ChartName
        {
            get;
            set;
        }
        string Title
        {
            get;
            set;
        }
        string Subtitle
        {
            get;
            set;
        }
    }
}
