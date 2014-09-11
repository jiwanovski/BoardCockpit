using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Helpers
{
    public abstract class GeneralChartInformation : IChart
    {
        //Highcharts chart;        
        List<string> categories;
        List<Series> dataSeries;
        List<YAxis> yAxisSeries;
        string chartName;
        string title;
        string subtitle;

        public abstract Highcharts Chart
        {
            get;
            set;
            //get { return chart; }
            //set { chart = value; }
        }        
        public List<string> Categories
        {
            get { return categories; }
            set { categories = value; }
        }
        public List<Series> DataSeries
        {
            get { return dataSeries; }
            set { dataSeries = value; }
        }
        public List<YAxis> YAxisSeries
        {
            get { return yAxisSeries; }
            set { yAxisSeries = value; }
        }
        public string ChartName 
        {
            get { return chartName; }
            set { chartName = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }
        
                      
    }
}