namespace ArduinoReaderDesktop
{
    public partial class Test : Form
    {
        private ToolTip toolTip;
        public Test()
        {
            InitializeComponent();
            LoadIcons();
            toolTip = new ToolTip();
            InitializeToolTip();
        }
        private async void LoadIcons()
        {
            await Task.Run(() =>
            {
                const int IconSize = 32;
                int IconsInRow = (int)Math.Floor((double)(Width / IconSize));
                int i = 0;
                while (true)
                {
                    var icon = IconExtractor.Extract(i, true);
                    if (icon == null)
                        break;
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = icon.ToBitmap(),
                        Size = new Size(IconSize, IconSize),
                        Location = new Point((i % IconsInRow) * IconSize, (i / IconsInRow) * IconSize),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Tag = i
                    };
                    pictureBox.MouseHover += PictureBox_MouseHover;
                    BeginInvoke(() => Controls.Add(pictureBox));
                    i++;
                }
            });
        }
        private void InitializeToolTip()
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1;
            toolTip.ReshowDelay = 1;
            toolTip.ShowAlways = true;
            toolTip.IsBalloon = true;
        }
        private void PictureBox_MouseHover(object? sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox && pictureBox.Tag != null)
            {
                int iconIndex = (int)pictureBox.Tag;
                toolTip.SetToolTip(pictureBox, "Index: " + iconIndex);
            }
        }
    }
}