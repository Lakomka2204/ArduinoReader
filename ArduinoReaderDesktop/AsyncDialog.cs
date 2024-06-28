using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArduinoReaderDesktop
{
    public partial class AsyncDialog : Form
    {
        private readonly Func<IProgress<double>?, CancellationToken, Task> asyncFunction;
        private CancellationTokenSource cancellationTokenSource;

        public AsyncDialog(Func<IProgress<double>?, CancellationToken, Task> asyncFunction)
        {
            InitializeComponent();
            this.asyncFunction = asyncFunction;
            cancellationTokenSource = new CancellationTokenSource();
        }

        private async void AsyncDialog_Load(object sender, EventArgs e)
        {
            try
            {
                var progress = new Progress<double>();
                progress.ProgressChanged += Progress_ProgressChanged;
                await asyncFunction.Invoke(progress, cancellationTokenSource.Token);
                DialogResult = DialogResult.OK;
            }
            catch (OperationCanceledException)
            {
                DialogResult = DialogResult.Cancel;
            }
            finally
            {
                Close();
            }
        }

        private void Progress_ProgressChanged(object? sender, double e)
        {
            progressBar1.Value = (int)(e * progressBar1.Maximum);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel(true);
        }

        public static DialogResult Run(Func<IProgress<double>?, CancellationToken, Task> asyncFunction)
        {
            using var dialog = new AsyncDialog(asyncFunction);
            return dialog.ShowDialog();
        }
    }
}
