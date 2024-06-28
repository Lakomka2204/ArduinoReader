using ArduinoReaderLib;
using DataPoint = ArduinoReaderLib.DataPoint;

namespace ArduinoReaderDesktop
{
    public partial class Main : Form
    {
        private float xZoomFactor = 1f;
        private const float ZoomIncrement = 0.8f;
        private int numXGridLines = 10;
        private int numYGridLines = 10;
        private bool isPanning = false;
        private Point panStartPoint;
        private double panOffset = 0;
        private const float MinZoomFactor = 1f;
        private float gridXScale = 1.0f;
        private int maxMargin = 10;
        private object locker = new object();
        private string cursorInfo;
        private int cursorX = -1;
        public Main()
        {
            InitializeComponent();
            InitializeIcons();
            cursorInfo = string.Empty;
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            dataPoints.CollectionChanged += DataPoints_CollectionChanged;
#if DEBUG
            CheckForIllegalCrossThreadCalls = false;
#endif
        }

        private void DataPoints_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs? e)
        {
            checkedListBox1.Items.Clear();
            foreach (var k in dataPoints.Keys)
                checkedListBox1.Items.Add(k, true);
        }

        private double MinTime
        {
            get
            {
                lock (locker)
                {
                    return dataPoints != null && dataPoints.Any()
                        ? dataPoints.Min(line => line.Value.Min(dp => dp.Time)) - maxMargin
                        : 0;
                }
            }
        }
        private double MinValue
        {
            get
            {
                lock (locker)
                {
                    return dataPoints != null && dataPoints.Any()
                        ? dataPoints.Min(line => line.Value.Min(dp => dp.Value)) - maxMargin
                        : 0;
                }
            }
        }

        private double MaxTime
        {
            get
            {
                lock (locker)
                {
                    return dataPoints != null && dataPoints.Any()
                        ? dataPoints.Max(line => line.Value.Max(dp => dp.Time)) + maxMargin
                        : 0;
                }
            }
        }
        private double MaxValue
        {
            get
            {
                lock (locker)
                {
                    return dataPoints != null && dataPoints.Any()
                        ? dataPoints.Max(line => line.Value.Max(dp => dp.Value)) + maxMargin
                        : 0;
                }
            }
        }

        private float XScale
        {
            get
            {
                return pictureBox1.Width / (float)(MaxTime - MinTime) * xZoomFactor;
            }
        }
        private float YScale
        {
            get
            {
                return pictureBox1.Height / (float)(MaxTime - MinTime);
            }
        }

        ArduinoReader? reader;
        DataPointDictionary dataPoints = [];
        private void InitializeIcons()
        {
            fileToolStripMenuItem.Image = IconExtractor.Extract(0, false)?.ToBitmap();
            newGraphToolStripMenuItem.Image = IconExtractor.Extract(0, false)?.ToBitmap();
            openGraphToolStripMenuItem.Image = IconExtractor.Extract(3, false)?.ToBitmap();
            saveGraphToolStripMenuItem.Image = IconExtractor.Extract(122, false)?.ToBitmap();
            exitToolStripMenuItem.Image = IconExtractor.Extract(131, false)?.ToBitmap();
            startMeasuringToolStripMenuItem.Image = IconExtractor.Extract(297, false)?.ToBitmap();
            stopMeasuringToolStripMenuItem.Image = IconExtractor.Extract(109, false)?.ToBitmap();
        }
        private void newGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var selectDialog = new SelectArduino();
            string port = string.Empty;
            int baud = 0;
            string folder = Directory.GetCurrentDirectory();
            if (selectDialog.ShowDialog() != DialogResult.OK) return;
            port = selectDialog.SelectedPort;
            baud = selectDialog.SelectedBaud;
            using var selFolderDialog = new FolderBrowserDialog()
            {
                AddToRecent = true,
                AutoUpgradeEnabled = true,
                ShowNewFolderButton = true,
                ShowPinnedPlaces = true,
                ShowHiddenFiles = true,
            };
            if (selFolderDialog.ShowDialog() != DialogResult.OK) return;
            folder = selFolderDialog.SelectedPath;

            reader = new ArduinoReader(port, folder, baud);
        }
        private void openGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var fileOpenDialog = new OpenFileDialog()
            {
                Filter = "Graph Files (*.graph)|*.graph",
                Multiselect = false,
            };
            if (fileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                var dpr = new DataPointReader(fileOpenDialog.FileName);
                if (AsyncDialog.Run(dpr.Read) == DialogResult.Cancel) return;
                lock (locker)
                {
                    dataPoints = DataPointCollection.TransformToDictionary(dpr.DataPoints);
                    DataPoints_CollectionChanged(sender, null);
                }
                pictureBox1.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startMeasuringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reader is null)
            {
                MessageBox.Show("Please select arduino!");
                return;
            }
            dataPoints.Clear();
            reader.Start();
            reader.NewDataReceived += NewDataFromReader;
            pictureBox1.Invalidate();
        }
        private void NewDataFromReader(object? sender, DataPoint e)
        {
            lock (locker)
            {
                if (!dataPoints.TryGetValue(e.Index, out var list))
                    list = [];
                list.Add(e);
                dataPoints[e.Index] = list;
                DataPoints_CollectionChanged(sender, null);
            }
            pictureBox1.Invalidate();
        }

        private void stopMeasuringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reader is null)
            {
                MessageBox.Show("Please select arduino!");
                return;
            }
            reader.Stop();
            reader.NewDataReceived -= NewDataFromReader;

        }

        private void saveMeasuringToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reader is null)
            {
                MessageBox.Show("Please select arduino!");
                return;
            }
            AsyncDialog.Run(reader.SaveToFile);
        }
        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                xZoomFactor += ZoomIncrement;
            else if (e.Delta < 0)
                xZoomFactor = Math.Max(MinZoomFactor, xZoomFactor - ZoomIncrement);
            gridXScale = 1.0f / xZoomFactor;
            pictureBox1.Invalidate();
        }

        private Point DataPoint2Point(DataPoint dp, double minTime, double minValue, float xScale, float yScale)
        {
            int x = (int)((dp.Time - minTime) * xScale);
            int y = (int)(pictureBox1.Height - (dp.Value - minValue) * yScale);
            return new Point(x, y);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            lock (locker)
            {

                if (dataPoints.Count == 0)
                {
                    g.DrawString("Nothing to draw", SystemFonts.DefaultFont, Brushes.Black, 10, 10);
                    return;
                }
                var minValue = MinValue;
                var maxValue = MaxValue;
                var minTime = MinTime;
                var maxTime = MaxTime;
                var xScale = (pictureBox1.Width / (float)(maxTime - minTime)) * xZoomFactor;
                var yScale = pictureBox1.Height / (float)(maxValue - minValue);
                minTime += panOffset;
                maxTime += panOffset;

                DrawGrid(g, minTime, maxTime, minValue, maxValue, xScale, yScale);
                foreach (var line in dataPoints)
                {
                    if (!checkedListBox1.CheckedItems.Contains(line.Key))
                        continue;
                    var brush = new SolidBrush(line.Value.Color);
                    var pen = new Pen(brush);
                    var pointArray = line.Value.Select(x => DataPoint2Point(x, minTime, minValue, xScale, yScale)).ToArray();
                    g.DrawCurve(pen, pointArray, (float)numericUpDown1.Value);
                    // ? interpolation
                    for (int i = 0; i < line.Value.Count - 1; i++)
                    {
                        if (Math.Abs(cursorX - pointArray[i].X) < 5)
                        {
                            var value = line.Value[i].ToString();
                            var fontSize = g.MeasureString(value, SystemFonts.DefaultFont);
                            var fontX = pointArray[i].X - fontSize.Width / 2;
                            var fontY = pointArray[i].Y - fontSize.Height;
                            g.DrawString(value, SystemFonts.DefaultFont, brush, fontX, fontY);
                        }
                    }
                }
                if (cursorX >= 0)
                {
                    double cursorTime = cursorX / XScale + MinTime;
                    var closestTimePoint = dataPoints.SelectMany(line => line.Value)
                                                      .OrderBy(dp => Math.Abs(dp.Time - cursorTime))
                                                      .FirstOrDefault();
                    cursorX = (int)((closestTimePoint.Time - MinTime) * XScale);
                    string timesText = $"{closestTimePoint.Time:0.##}";
                    g.DrawLine(Pens.Red, cursorX, 0, cursorX, pictureBox1.Height);
                }

            }
        }

        private void DrawGrid(Graphics g, double minTime, double maxTime, double minValue, double maxValue, float xScale, float yScale)
        {
            var gridPen = new Pen(Color.LightGray);
            double timeRange = maxTime - minTime;
            double valueRange = maxValue - minValue;
            double xInterval = timeRange / numXGridLines;
            for (int i = 0; i <= numXGridLines; i++)
            {
                double x = minTime + i * xInterval * gridXScale;
                var screenX = (float)((x - minTime) * xScale);
                g.DrawLine(gridPen, screenX, 0, screenX, pictureBox1.Height);
                g.DrawString(x.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Gray, screenX, pictureBox1.Height - 20);
            }
            double yInterval = valueRange / numYGridLines;
            for (int i = 0; i <= numYGridLines; i++)
            {
                double y = minValue + i * yInterval;
                var screenY = (float)(pictureBox1.Height - (y - minValue) * yScale);
                g.DrawLine(gridPen, 0, screenY, pictureBox1.Width, screenY);
                g.DrawString(y.ToString("0.##"), SystemFonts.DefaultFont, Brushes.Gray, 0, screenY - 10);
            }
        }

        private double GetNiceInterval(double range)
        {
            if (range < 10)
                return 1;
            else if (range < 100)
                return 10;
            else if (range < 1000)
                return 100;
            else
                return 1000;
        }
        private void Main_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanning = true;
                panStartPoint = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPanning)
            {
                var deltaX = -(e.Location.X - panStartPoint.X);
                var minVisibleTime = panOffset / xZoomFactor;
                var maxVisibleTime = minVisibleTime + pictureBox1.Width / XScale;
                panOffset = Math.Max(-100, Math.Min((MaxTime + 100) - MinTime - maxVisibleTime + minVisibleTime, panOffset + deltaX));
                panStartPoint = e.Location;
                pictureBox1.Invalidate();
            }
            else
            {
                cursorX = e.X;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isPanning = false;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Test().Show();
        }
        private async Task SomeAsyncFunction(IProgress<double>? progress = null, CancellationToken token = default)
        {
            for (int i = 0; i < 100; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(50);
                progress?.Report((i + 1) / 100.0);
            }
        }
        private void testAsyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsyncDialog.Run(SomeAsyncFunction);
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
