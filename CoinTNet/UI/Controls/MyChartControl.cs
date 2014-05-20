using CoinTNet.Common;
using CoinTNet.DO;
using CoinTNet.DO.Strategies;
using CoinTNet.UI.Common;
using CoinTNet.UI.Common.EventAggregator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Control displaying a chart
    /// </summary>
    partial class MyChartControl : UserControl
    {
        #region Private Members
        private RectangleAnnotation _priceAnnotation;
        private CalloutAnnotation _maxPriceAnnotation;
        private CalloutAnnotation _minPriceAnnotation;
        private ArrowAnnotation _currentPriceAnnotation;

        private Stack<RectangleF> _zoomPositions = new Stack<RectangleF>();

        private ChartArea _mainChartArea;
        private IList<OHLC> _candles;

        private IndicatorOptions _indicatorOptions;
        #endregion


        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public MyChartControl()
        {
            InitializeComponent();
            _candles = new List<OHLC>();
            _mainChartArea = chartCtrl.ChartAreas[Constants.CandleAreaName];

            chartCtrl.MouseWheel += chartCtrl_MouseWheel;

            chartCtrl.MouseEnter += (s, e) =>
            {
                if (!chartCtrl.Focused)
                    chartCtrl.Focus();
            };
            chartCtrl.MouseLeave += (s, e) =>
            {
                if (chartCtrl.Focused)
                {
                    chartCtrl.Parent.Focus();
                }
                _mainChartArea.CursorX.Position = _mainChartArea.CursorY.Position = -10;
                _priceAnnotation.X = _priceAnnotation.Y = -10;
                lblInfo.Text = string.Empty;
            };
            chartCtrl.MouseMove += chartCtrl_MouseMove;

            InitChart();
        }
        /// <summary>
        /// Notifies the control that a simulation finished
        /// </summary>
        /// <param name="result"></param>
        public void NotifySimulationResult(SimulationResult result)
        {
            DisplaySimulationResult(result);
        }

        /// <summary>
        /// Notifies the control that indicators changed
        /// </summary>
        /// <param name="options"></param>
        public void NotifyIndicatorsChanged(IndicatorOptions options)
        {
            _indicatorOptions = options;
            CalculateMSChartAverages(Constants.PriceSerieName, true);
        }

        /// <summary>
        /// Clears the chart
        /// </summary>
        public void Clear()
        {
            chartCtrl.Series.ForEachExt(s => s.Points.Clear());
            _maxPriceAnnotation.Visible = false;//Otherwise, only this one stays visible during refresh
            chartCtrl.Invalidate();
        }

        /// <summary>
        /// Initialises the chart
        /// </summary>
        private void InitChart()
        {
            _mainChartArea.AxisX.ScaleView.Zoomable = true;
            _mainChartArea.AxisY.ScaleView.Zoomable = true;
            _mainChartArea.CursorX.AutoScroll = true;
            _mainChartArea.CursorY.AutoScroll = true;
            //_mainChartArea.AxisY2.Enabled = AxisEnabled.True;

            _priceAnnotation = new RectangleAnnotation();
            _priceAnnotation.ForeColor = Color.Black;
            _priceAnnotation.Font = new Font("Arial", 9); ;
            _priceAnnotation.LineWidth = 1;
            _priceAnnotation.BackColor = Color.LemonChiffon;

            chartCtrl.Annotations.Add(_priceAnnotation);


            _maxPriceAnnotation = new CalloutAnnotation();
            _maxPriceAnnotation.ForeColor = Color.Black;
            _maxPriceAnnotation.Font = new Font("Arial", 9); ;
            _maxPriceAnnotation.LineWidth = 1;
            _maxPriceAnnotation.CalloutStyle = CalloutStyle.SimpleLine;
            _maxPriceAnnotation.AnchorAlignment = ContentAlignment.BottomRight;
            chartCtrl.Annotations.Add(_maxPriceAnnotation);


            _minPriceAnnotation = new CalloutAnnotation();
            _minPriceAnnotation.ForeColor = Color.Black;
            _minPriceAnnotation.Font = new Font("Arial", 9); ;
            _minPriceAnnotation.LineWidth = 1;
            _minPriceAnnotation.CalloutStyle = CalloutStyle.SimpleLine;
            _minPriceAnnotation.AnchorAlignment = ContentAlignment.MiddleRight;
            _minPriceAnnotation.AxisX = _mainChartArea.AxisX;
            _minPriceAnnotation.AxisY = _mainChartArea.AxisY;

            chartCtrl.Annotations.Add(_minPriceAnnotation);

            _currentPriceAnnotation = new ArrowAnnotation();
            _currentPriceAnnotation.ForeColor = Color.Black;
            _currentPriceAnnotation.Font = new Font("Arial", 9);
            _currentPriceAnnotation.LineWidth = 1;
            _currentPriceAnnotation.Width = 1;
            _currentPriceAnnotation.Height = 1;
            _currentPriceAnnotation.ArrowSize = 1;
            _currentPriceAnnotation.AxisX = _mainChartArea.AxisX;
            _currentPriceAnnotation.AxisY = _mainChartArea.AxisY;
            _currentPriceAnnotation.ArrowSize = 3;
            _currentPriceAnnotation.Height = 0;
            _currentPriceAnnotation.AnchorAlignment = ContentAlignment.MiddleLeft;
            //_currentPriceAnnotation.AnchorAlignment = ContentAlignment.MiddleRight;

            chartCtrl.Annotations.Add(_currentPriceAnnotation);

            // _mainChartArea.BackSecondaryColor = _mainChartArea.BackColor = Color.Black;
            chartCtrl.Series[Constants.PriceSerieName]["PriceUpColorUp"] = "Green";
        }

        /// <summary>
        /// Draws cursors and price indicator when the mouse moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (_candles.Count > 0 && chartCtrl.ClientRectangle.Contains(e.Location))
                {
                    var valX = _mainChartArea.AxisX.PixelPositionToValue(e.Location.X);
                    var valY = _mainChartArea.AxisY.PixelPositionToValue(e.Location.Y);

                    if (valX <= _mainChartArea.AxisX.Maximum && valX >= _mainChartArea.AxisX.Minimum)
                    {
                        _mainChartArea.CursorX.SetCursorPosition(valX);
                    }

                    if (valY <= _mainChartArea.AxisY.Maximum && valY >= _mainChartArea.AxisY.Minimum)
                    {
                        _mainChartArea.CursorY.SetCursorPosition(valY);
                    }

                    //priceAnnotation.AxisX = _mainChartArea.AxisX;
                    //priceAnnotation.AxisY = _mainChartArea.AxisY;
                    //priceAnnotation.X = valX;
                    //priceAnnotation.Y = valY;

                    _priceAnnotation.X = 90;
                    _priceAnnotation.Y = e.Location.Y / (float)chartCtrl.Height * 100;

                    //Find out how many decimals we want to display
                    int nbDecimals = 2;
                    var k = valY;
                    while (k < 10 && nbDecimals < 8)
                    {
                        nbDecimals++;
                        k = k * 10;
                    }
                    string format = "0." + new string('0', nbDecimals);


                    _priceAnnotation.Text = valY.ToString(format, CultureInfo.InvariantCulture);
                    int index = (int)(_mainChartArea.CursorX.Position - 1);
                    if (index >= 0 && index <= chartCtrl.Series[Constants.PriceSerieName].Points.Count - 1)
                    {
                        var candle = chartCtrl.Series[Constants.PriceSerieName].Points[index].Tag as OHLC;
                        lblInfo.Text = string.Format(CultureInfo.InvariantCulture, "Date: {0} - O: {1} - H: {2} - L: {3} - C: {4}",
                            candle.Date, candle.Open.ToString(format, CultureInfo.InvariantCulture),
                            candle.High.ToString(format, CultureInfo.InvariantCulture), candle.Low.ToString(format, CultureInfo.InvariantCulture), candle.Close.ToString(format, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        lblInfo.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                ErrorHelper.DisplayErrorMessage(ex.ToString());
            }

        }

        #region Chart MA/Forecasting

        /// <summary>
        /// Displays averages
        /// </summary>
        /// <param name="dataSerieName"></param>
        /// <param name="invalidate"></param>
        private void CalculateMSChartAverages(string dataSerieName, bool invalidate = false)
        {
            string[] series = new string[] { "SimpleH", "SimpleFastH", "ExponentialH", "ExponentialFastH", "TriangularH", "TriangularFastH", "WeightedH", "WeightedFastH" };
            string areaName = Constants.CandleAreaName;

            foreach (string serie in series)
            {
                if (chartCtrl.Series.FindByName(serie) != null)
                {
                    if (chartCtrl.Series[serie].ChartArea == areaName)
                    {
                        chartCtrl.Series[serie].Points.Clear();
                        chartCtrl.Series[serie].Enabled = false;
                        chartCtrl.Series[serie].IsVisibleInLegend = false;
                    }
                }
            }

            // Return if anything to calculate or not enabled
            if (chartCtrl.Series[dataSerieName].Points.Count == 0 || _indicatorOptions == null)
            {
                return;
            }

            // Start from first property is true by default.
            chartCtrl.DataManipulator.IsStartFromFirst = false;

            var slowSelection = _indicatorOptions.SlowMSAverage;
            var fastSelection = _indicatorOptions.FastMSAverage;

            // Calculate and draw slow average
            if (slowSelection.SerieName != string.Empty && _candles.Count >= _indicatorOptions.SlowMSPeriod)
            {
                chartCtrl.DataManipulator.FinancialFormula(slowSelection.Formula, _indicatorOptions.SlowMSPeriod.ToString(), dataSerieName, slowSelection.SerieName);
                chartCtrl.Series[slowSelection.SerieName].ChartArea = areaName;
                chartCtrl.Series[slowSelection.SerieName].Color = Color.Orange;
                SetSeriesAppearance(slowSelection.SerieName, SeriesChartType.Line);
            }

            // Calculate and draw fast average
            if (fastSelection.SerieName != string.Empty && _candles.Count >= _indicatorOptions.FastMSPeriod)
            {
                chartCtrl.DataManipulator.FinancialFormula(fastSelection.Formula, _indicatorOptions.FastMSPeriod.ToString(), dataSerieName, fastSelection.SerieName);
                //chartCtrl.DataManipulator.FinancialFormula(FinancialFormula.MovingAverageConvergenceDivergence, "12,26", dataSerieName, fastSelection.SerieName);
                chartCtrl.Series[fastSelection.SerieName].ChartArea = areaName;
                chartCtrl.Series[fastSelection.SerieName].Color = Color.SkyBlue;
                SetSeriesAppearance(fastSelection.SerieName, SeriesChartType.Line);
            }

            if (invalidate)
            {
                chartCtrl.AlignDataPointsByAxisLabel();
                chartCtrl.Invalidate();
            }
        }

        private void Forecast()
        {
            // typeRegression is a string represented by one of the following strings:
            // "Linear", "Exponential", "Logarithmic", or "Power".
            // Polynomial is represented by an integer value in the form of a string.
            string typeRegression;

            // Defining the typeRegression.
            // This Statement can also be represented by the statement typeRegression = "2";
            typeRegression = "Exponential";

            // The number of days for Forecasting.
            int forecasting = 10;

            // Show Error as a range chart.
            string error = "true";

            // Show Forecasting Error as a range chart.
            string forecastingError = "true";

            // Formula parameters
            string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;

            // Create Forecasting Series.
            chartCtrl.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, Constants.PriceSerieName + ":Y", "Forecasting:Y,Forecasting:Y2,Forecasting:Y3");
            SetSeriesAppearance("Forecasting", SeriesChartType.Line);

            // Copy Forecasting Series Data Points to Range Chart.
            if (error == "true" || forecastingError == "true")
            {
                //chartCtrl.DataManipulator.CopySeriesValues("Forecasting:Y2,Forecasting:Y3", "Range:Y,Range:Y");
                //SetSeriesAppearance("Range", SeriesChartType.Range, true);
            }
        }

        #endregion


        /// <summary>
        /// Updates the chart control
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="serieName"></param>
        public void DrawChart(IList<OHLC> candles, string serieName)
        {
            if(this.chartCtrl.IsDisposed)
            {
                return;
            }
            _candles = candles;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            try
            {
                // Set series chart type
                if (chartCtrl.Series.FindByName(serieName) != null)
                {
                    chartCtrl.Series[serieName].Points.Clear();
                }

                var xAxis = _mainChartArea.AxisX;
                var yAxis = _mainChartArea.AxisY;
                var xScaleView = _mainChartArea.AxisX.ScaleView;
                var yScaleView = _mainChartArea.AxisY.ScaleView;
                bool zoomed = xScaleView.IsZoomed || yScaleView.IsZoomed;
                // Adjust Y & X axis scale

                double width = xScaleView.ViewMaximum - xScaleView.Position;
                double height = yScaleView.ViewMaximum - yScaleView.ViewMinimum;
                double xstart = xScaleView.Position, xEnd = xScaleView.ViewMaximum;
                double ystart = yScaleView.ViewMinimum, yEnd = yScaleView.ViewMaximum;

                bool maxX = zoomed && xScaleView.ViewMaximum == xAxis.Maximum;
                bool maxY = zoomed && yScaleView.ViewMaximum == yAxis.Maximum;

                chartCtrl.ResetAutoValues();
                chartCtrl.Series[serieName].Color = Color.SteelBlue;

                // Set point width
                chartCtrl.Series[serieName]["PointWidth"] = "0.8";
                chartCtrl.Series[serieName].IsXValueIndexed = true;

                for (int i = 0; i < candles.Count; i++)
                {
                    var candle = candles[i];
                    // adding date and high, low, open, close
                    chartCtrl.Series[serieName].Points.AddXY(candle.Date, (double)candle.High);

                    DataPoint currentPoint = chartCtrl.Series[serieName].Points[i];
                    currentPoint.YValues[1] = (double)candle.Low;
                    currentPoint.YValues[2] = (double)candle.Open;
                    currentPoint.YValues[3] = (double)candle.Close;
                    currentPoint.Tag = candle;

                    currentPoint.Color = candle.Close >= candle.Open ? Color.Green : Color.Red;
                    currentPoint.BorderColor = currentPoint.Color;

                    string format = "dd/MM HH:mm";
                    currentPoint.AxisLabel = candle.Date.ToString(format);
                }//for candles

                CalculateMSChartAverages(serieName);


                chartCtrl.AlignDataPointsByAxisLabel();

                _maxPriceAnnotation.Visible = _minPriceAnnotation.Visible = _currentPriceAnnotation.Visible = candles.Count > 0;
                if (candles.Count == 0)
                {
                    return;
                }

                var minPrice = candles.Min(c => c.Low);
                var maxPrice = candles.Max(c => c.High);

                //Indicates highest/lowest price
                DataPoint maxValuePoint = chartCtrl.Series[serieName].Points.FindMaxByValue();
                DataPoint minValuePoint = chartCtrl.Series[serieName].Points.Where(prop => prop.YValues[1] == (double)minPrice).First();
                DataPoint currentPrice = chartCtrl.Series[serieName].Points.Last();



                //Forced to do all of that so that we don't anchor on the High value of the last candle
                _currentPriceAnnotation.AnchorX = currentPrice.XValue;
                _currentPriceAnnotation.AnchorY = currentPrice.YValues[3];

                _maxPriceAnnotation.AnchorDataPoint = maxValuePoint;
                _maxPriceAnnotation.Text = (maxValuePoint.Tag as OHLC).High.ToString("0.00######", CultureInfo.InvariantCulture);

                //Forced to do all of that so that we don't anchor on the High value of the min point
                _minPriceAnnotation.AnchorX = minValuePoint.XValue;
                _minPriceAnnotation.AnchorY = minValuePoint.YValues[1];
                _minPriceAnnotation.Text = (minValuePoint.Tag as OHLC).Low.ToString("0.00######", CultureInfo.InvariantCulture);

                //Sets axes min/max

                decimal range = maxPrice - minPrice;
                if (range == 0m)
                {
                    range = 0.0000001m;
                }
                int extra = 5;

                //Find out how many decimals we want to display
                int nbDecimals = 0;
                var k = range;
                double cursorInterval = 0.1;
                while (k < 10 && nbDecimals < 8)
                {
                    nbDecimals++;
                    k = k * 10;
                    cursorInterval /= 10;
                }

                yAxis.Maximum = (double)(maxPrice + range * extra / 100);
                yAxis.Minimum = (double)(minPrice - range * extra / 100);

                yAxis.LabelStyle.Format = nbDecimals > 0 ? ("{0:0." + new string('#', nbDecimals) + "}") : "{0}";

                _mainChartArea.CursorY.Interval = cursorInterval;

                if (zoomed)
                {
                    _mainChartArea.RecalculateAxesScale();
                    if (maxX)
                    {
                        xEnd = xAxis.Maximum;
                        xstart = xEnd - width;
                    }
                    if (maxY)
                    {
                        yEnd = yAxis.Maximum;
                        ystart = yEnd - height;
                    }
                    xScaleView.Zoom(xstart, xEnd);
                    yScaleView.Zoom(ystart, yEnd);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
                EventAggregator.Instance.Publish(new StatusUpdateMessage { Message = string.Format("Error in DrawChart: {0}", e.Message) });
            }
            finally
            {
                chartCtrl.Invalidate();
            }
        }

        /// <summary>
        /// Sets a series appearance
        /// </summary>
        /// <param name="seriesName"></param>
        /// <param name="chartType"></param>
        /// <param name="isTransparent"></param>
        private void SetSeriesAppearance(string seriesName, SeriesChartType chartType, bool isTransparent = false)
        {
            chartCtrl.Series[seriesName].ChartType = chartType;
            chartCtrl.Series[seriesName].BorderWidth = 1;
            chartCtrl.Series[seriesName].ShadowOffset = 1;
            chartCtrl.Series[seriesName].IsVisibleInLegend = false;
            chartCtrl.Series[seriesName].IsXValueIndexed = true;
            chartCtrl.Series[seriesName].Enabled = true;

            if (isTransparent)
            {
                Color color = chartCtrl.Series[seriesName].Color;
                chartCtrl.Series[seriesName].Color = Color.FromArgb(128, color.R, color.G, color.B);
            }
        }

        /// <summary>
        /// Displays simulation result
        /// </summary>
        /// <param name="result"></param>
        private void DisplaySimulationResult(SimulationResult result)
        {
            foreach (var point in chartCtrl.Series[Constants.PriceSerieName].Points)
            {
                MarketAction action = result.Actions.Where(x => x.Date == (point.Tag as OHLC).Date).FirstOrDefault();

                if (action != null)
                {
                    point.MarkerImage = action.ActionType == ActionType.Bid ? "MaxMarker.bmp" : "MinMarker.bmp";

                    point.MarkerImageTransparentColor = Color.White;
                }
                else if (!string.IsNullOrEmpty(point.MarkerImage))
                {
                    point.MarkerImage = string.Empty;
                }

            }
        }

        #region Zoom

        /// <summary>
        /// Zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!chartCtrl.ClientRectangle.Contains(e.Location))
            {
                return;
            }
            try
            {
                if (e.Delta < 0)
                {
                    if (_zoomPositions.Count > 0)
                    {
                        var pos = _zoomPositions.Pop();

                        _mainChartArea.AxisX.ScaleView.Zoom(pos.X, pos.X + pos.Width);
                        _mainChartArea.AxisY.ScaleView.Zoom(pos.Y, pos.Y + pos.Height);
                    }

                }

                if (e.Delta > 0)
                {

                    _zoomPositions.Push(new RectangleF
                    {

                        X = (float)_mainChartArea.AxisX.ScaleView.Position,
                        Width = (float)_mainChartArea.AxisX.ScaleView.Size,

                        Y = (float)_mainChartArea.AxisY.ScaleView.Position,
                        Height = (float)_mainChartArea.AxisY.ScaleView.Size
                    });

                    double xMin = _mainChartArea.AxisX.ScaleView.ViewMinimum;
                    double xMax = _mainChartArea.AxisX.ScaleView.ViewMaximum;
                    double yMin = _mainChartArea.AxisY.ScaleView.ViewMinimum;
                    double yMax = _mainChartArea.AxisY.ScaleView.ViewMaximum;

                    double posXStart = _mainChartArea.AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = _mainChartArea.AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    double posYStart = _mainChartArea.AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    double posYFinish = _mainChartArea.AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    _mainChartArea.AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    _mainChartArea.AxisY.ScaleView.Zoom(posYStart, posYFinish);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void chartCtrl_AxisViewChanged(object sender, ViewEventArgs e)
        {
            //RectangleF lastPos = _zoomPositions.Count > 0 ? _zoomPositions.Pop() : new RectangleF();
            //RectangleF obj = new RectangleF();

            //if (e.Axis == MainChartArea.AxisY)
            //{
            //    if (lastPos.X != 0)
            //    {
            //        obj = lastPos;
            //    }
            //    obj.Y = (float)e.NewPosition;
            //    obj.Height = (float)e.NewSize;
            //}

            //else//X axis
            //{
            //    if (lastPos.Y != 0)
            //    {
            //        obj = lastPos;
            //    }
            //    obj.X = (float)e.NewPosition;
            //    obj.Width = (float)e.NewSize;
            //}


            //_zoomPositions.Push(obj);

        }
        #endregion
    }
}
