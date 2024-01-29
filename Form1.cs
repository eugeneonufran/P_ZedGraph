using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ZedGraphQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ScatterButton_Click(null, null);
            SaveFromConsoleApplication();
        }

        private void SaveFromConsoleApplication()
        {
            // simulate plotting from a console application
            var pane = new ZedGraph.GraphPane();
            var curve1 = pane.AddCurve(
                label: "demo",
                x: new double[] { 1, 2, 3, 4, 5 },
                y: new double[] { 1, 4, 9, 16, 25 },
                color:Color.Blue);
            curve1.Line.IsAntiAlias = true;
            pane.AxisChange();
            Bitmap bmp = pane.GetImage(400, 300, dpi: 100, isAntiAlias: true);
            bmp.Save("zedgraph-console-quickstart.png", ImageFormat.Png);
        }

        private Random rand = new Random(0);
        private double[] RandomWalk(int points, double start = 100, double mult = 50)
        {
            // return an array of difting random numbers
            double[] values = new double[points];
            values[0] = start;
            for (int i = 1; i < points; i++)
                values[i] = values[i - 1] + (rand.NextDouble() - .5) * mult;
            return values;
        }

        private double[] Consecutive(int points)
        {
            // return an array of ascending numbers starting at 1
            double[] values = new double[points];
            for (int i = 0; i < points; i++)
                values[i] = i + 1;
            return values;
        }

        private void BarButton_Click(object sender, EventArgs e)
        {
            /*
            // generate some random Y data
            int pointCount = 5;
            double[] xs = Consecutive(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // plot the data as bars
            zedGraphControl1.GraphPane.AddBar("Group A", xs, ys1, Color.Blue);
            zedGraphControl1.GraphPane.AddBar("Group B", xs, ys2, Color.FromArgb(2, Color.Red));

            // style the plot
            zedGraphControl1.GraphPane.Title.Text = $"Bar Plot ({pointCount:n0} points)";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Horizontal Axis Label";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Vertical Axis Label";

            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.Refresh();
            */
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {/*
            // generate some random Y data
            int pointCount = 100;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // plot the data as curves
            var curve1 = zedGraphControl1.GraphPane.AddCurve("Series A", xs1, ys1, Color.FromArgb(50, Color.Blue));
            curve1.Line.IsAntiAlias = true;

            var curve2 = zedGraphControl1.GraphPane.AddCurve("Series B", xs2, ys2, Color.FromArgb(50, Color.Red));
            curve2.Line.IsAntiAlias = true;

            // style the plot
            zedGraphControl1.GraphPane.Title.Text = $"Scatter Plot ({pointCount:n0} points)";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Horizontal Axis Label";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Vertical Axis Label";

            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.Refresh();
            */
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            /*
            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();


            // Example of adding a vertical line from y = 50 to y = 100 with semi-transparent green color
            SetRainbowBackground(zedGraphControl1.GraphPane);
            AddVerticalLine(zedGraphControl1.GraphPane, 0, Color.Blue, 255, 0, 1);
            AddVerticalLine(zedGraphControl1.GraphPane, 0, Color.Blue, 128, 1, 2);
            AddVerticalLine(zedGraphControl1.GraphPane, 0, Color.Blue, 40, 2, 4);

            zedGraphControl1.AxisChange();


            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            */

            CreateSimplifiedClimateGraph();
            zedGraphControl1.Refresh();
        }

        private void AddVerticalLine(ZedGraph.GraphPane pane, double xPosition, Color lineColor, int alpha, double yMin, double yMax)
        {
            // Create a semi-transparent color
            Color transparentColor = Color.FromArgb(alpha, lineColor);

            // Create a vertical line object
            var line = new ZedGraph.LineObj(transparentColor, xPosition, yMin, xPosition, yMax);

            // Customize line properties (if needed)
            line.Line.Width = 8f; // Example line width
            line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Line style
            line.Line.IsAntiAlias = true; // Enable anti-aliasing for smoother line

            // Add the line object to the pane
            pane.GraphObjList.Add(line);
        }

        private void CreateSimplifiedClimateGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Clear previous settings
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            // Set the graph title
            myPane.Title.Text = "Simplified Climate Data Graph";

            // Configure the primary X-axis (Bottom) - Time in years
            myPane.XAxis.Title.Text = "Year";
            myPane.XAxis.Title.FontSpec.FontColor = Color.Blue;
            myPane.XAxis.Color = Color.Blue;
            myPane.XAxis.Type = AxisType.Linear;
            myPane.XAxis.Scale.Min = 1990; // Starting year
            myPane.XAxis.Scale.Max = 2020; // Ending year

            // Configure the secondary X-axis (Top) - Climate epochs
            myPane.X2Axis.IsVisible = true;
            myPane.X2Axis.Title.Text = "Climate Epoch";
            myPane.X2Axis.Title.FontSpec.FontColor = Color.Red;
            myPane.X2Axis.Color = Color.Red;
            myPane.X2Axis.Scale.FontSpec.FontColor = Color.Red;
            myPane.X2Axis.Type = AxisType.Linear;
            myPane.X2Axis.Scale.Min = 0; // Align with the primary X-axis
            myPane.X2Axis.Scale.Max = 3; // Number of epochs
            myPane.X2Axis.Scale.TextLabels = new string[] { "", "Pre-Industrial", "Industrial Revolution", "Modern Era", "" };

            // Add a simple line for demonstration
            PointPairList points = new PointPairList();
            for (int year = 1990; year <= 2020; year++)
            {
                points.Add(year, Random.NextDouble()); // Random Y values
            }
            LineItem curve = myPane.AddCurve("Sample Data", points, Color.Green);

            // Update the graph
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private Random Random = new Random();



    }
}
