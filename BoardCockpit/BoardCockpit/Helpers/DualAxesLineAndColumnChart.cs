using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BoardCockpit.Helpers
{
    public class DualAxesLineAndColumnChart : GeneralChartInformation
    {
        Highcharts chart; 
        //#region Locals
        //Highcharts chart;
        //List<string> categories;
        //List<Series> dataSeries;
        //List<YAxis> yAxisSeries;
        //string title;
        //string subtitle;
        //#endregion

        //#region accessors
        //public List<string> Categories
        //{
        //    get { return categories; }
        //    set { categories = value; }
        //}
        //public List<YAxis> YAxisSeries
        //{
        //    get { return yAxisSeries; }
        //    set { yAxisSeries = value; }
        //}

        //public List<Series> DataSeries
        //{
        //    get { return dataSeries; }
        //    set { dataSeries = value; }
        //}
        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }
        //}
        //public string Subtitle
        //{
        //    get { return subtitle; }
        //    set { subtitle = value; }
        //}
        //#endregion

        
        public override Highcharts Chart
        {
            get 
            {
                chart = new Highcharts(ChartName)
                    .InitChart(new Chart { ZoomType = ZoomTypes.Xy })
                    .SetTitle(new Title { Text = Title })
                    .SetSubtitle(new Subtitle { Text = Subtitle })
                    .SetXAxis(new XAxis { Categories = Categories.ToArray() })
                    .SetYAxis(YAxisSeries.ToArray())
                    .SetTooltip(new Tooltip
                    {
                        Formatter = "function() { return ''+ this.x +': '+ this.y + (this.series.name == 'Rainfall' ? ' mm' : '°C'); }"
                    })
                    //.SetLegend(new Legend
                    //{
                    //    Layout = Layouts.Vertical,
                    //    Align = HorizontalAligns.Left,
                    //    X = 120,
                    //    VerticalAlign = VerticalAligns.Top,
                    //    Y = 100,
                    //    Floating = true,
                    //    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF"))
                    //})
                    .SetSeries(DataSeries.ToArray());

                return chart; 
            }
            set
            {
                chart = value;
            }
        }
        //public Highcharts GetChart(string chartName)
        //{
        //    Highcharts Chart = new Highcharts(chartName)
        //        .InitChart(new Chart { ZoomType = ZoomTypes.Xy })
        //        .SetTitle(new Title { Text = Title })
        //        .SetSubtitle(new Subtitle { Text = Subtitle })
        //        .SetXAxis(new XAxis { Categories = Categories.ToArray() })
        //        .SetYAxis(YAxisSeries.ToArray()
        //        //{
        //        //    new YAxis
        //        //    {
        //        //        Labels = new YAxisLabels
        //        //        {
        //        //            Formatter = "function() { return this.value +'°C'; }",
        //        //            Style = "color: '#89A54E'"
        //        //        },
        //        //        Title = new YAxisTitle
        //        //        {
        //        //            Text = "Temperature",
        //        //            Style = "color: '#89A54E'"
        //        //        }
        //        //    },
        //        //    new YAxis
        //        //    {
        //        //        Labels = new YAxisLabels
        //        //        {
        //        //            Formatter = "function() { return this.value +' mm'; }",
        //        //            Style = "color: '#4572A7'"
        //        //        },
        //        //        Title = new YAxisTitle
        //        //        {
        //        //            Text = "Rainfall",
        //        //            Style = "color: '#4572A7'"
        //        //        },
        //        //        Opposite = true
        //        //    }
        //        //})
        //        )
        //        .SetTooltip(new Tooltip
        //        {
        //            Formatter = "function() { return ''+ this.x +': '+ this.y + (this.series.name == 'Rainfall' ? ' mm' : '°C'); }"
        //        })
        //        .SetLegend(new Legend
        //        {
        //            Layout = Layouts.Vertical,
        //            Align = HorizontalAligns.Left,
        //            X = 120,
        //            VerticalAlign = VerticalAligns.Top,
        //            Y = 100,
        //            Floating = true,
        //            BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF"))
        //        })
        //        .SetSeries( DataSeries.ToArray()
        //        //new[]
        //        //{
        //        //    new Series
        //        //    {
        //        //        Name = "Test",//"Rainfall",
        //        //        Color = ColorTranslator.FromHtml("#4572A7"),
        //        //        Type = ChartTypes.Column,
        //        //        YAxis = "1",
        //        //        Data = new Data(new object[] { 49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
        //        //    },
        //        //    new Series
        //        //    {
        //        //        Name = "bs.ass",//"Temperature",
        //        //        Color = ColorTranslator.FromHtml("#89A54E"),
        //        //        Type = ChartTypes.Spline,
        //        //        Data = new Data(new object[] { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 })
        //        //    }
        //        //});
        //        );

        //    return Chart;
        //}
        //#endregion
    }
}