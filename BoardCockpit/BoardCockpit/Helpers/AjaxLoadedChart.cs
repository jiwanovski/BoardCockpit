using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Helpers
{
    public class AjaxLoadedChart
    {
        #region Locals
        Highcharts chart;
        List<string> categories;
        List<Series> dataSeries;        
        string title;
        string subtitle;        
        #endregion

        #region accessors 
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
        #endregion

        #region global functions
        public Highcharts GetChart(string chartName)
        {
            chart = new Highcharts(chartName)
                // OK
                .SetTitle(new Title { Text = title })
                // OK
                .SetSubtitle(new Subtitle { Text = subtitle })
                // OK
                .SetXAxis(new XAxis
                {
                    Categories = categories.ToArray()
                    //Type = AxisTypes.Datetime,
                    //TickInterval = 7 * 24 * 3600 * 1000, // one week
                    //TickWidth = 0,
                    //GridLineWidth = 1,
                    //Labels = new XAxisLabels
                    //{
                    //    Align = HorizontalAligns.Left,
                    //    X = 3,
                    //    Y = -3
                    //}
                })
                // OK
                .SetYAxis(new[]
                {
                    new YAxis
                    {
                        Title = new YAxisTitle { Text = "" },
                        Labels = new YAxisLabels
                        {
                            Align = HorizontalAligns.Left,
                            X = 3,
                            Y = 16,
                            Formatter = "function() { return Highcharts.numberFormat(this.value, 0); }",
                        },
                        ShowFirstLabel = false
                    },
                    new YAxis
                    {
                        LinkedTo = 0,
                        GridLineWidth = 0,
                        Opposite = true,
                        Title = new YAxisTitle { Text = "" },
                        Labels = new YAxisLabels
                        {
                            Align = HorizontalAligns.Right,
                            X = -3,
                            Y = 16,
                            Formatter = "function() { return Highcharts.numberFormat(this.value, 0); }"
                        },
                        ShowFirstLabel = false
                    }
                })
                // OK
                .SetLegend(new Legend
                {
                    Align = HorizontalAligns.Left,
                    VerticalAlign = VerticalAligns.Top,
                    Y = 20,
                    Floating = true,
                    BorderWidth = 0
                })
                // OK
                .SetTooltip(new Tooltip
                {
                    Shared = true,
                    Crosshairs = new Crosshairs(true)
                })
                // OK
                .SetPlotOptions(new PlotOptions
                {
                    Series = new PlotOptionsSeries
                    {
                        Cursor = Cursors.Pointer,
                        Point = new PlotOptionsSeriesPoint
                        {
                            Events = new PlotOptionsSeriesPointEvents
                            {
                                Click = @"function() {
                                    alert(Highcharts.dateFormat('%A, %b %e, %Y', this.x) +': '+ this.y +' visits');
                                }"
                            }
                        },
				        Marker = new PlotOptionsSeriesMarker { LineWidth = 1 }
                    }
                })
                // TODO Series einbauen
                .SetSeries(dataSeries.ToArray()
                    //new[]
                    //{                    
                    //    new Series { Name = "All visits" },
                    //    new Series { Name = "New visitors" }
                    //}
                );

            return chart;
        }
        #endregion
    }
}