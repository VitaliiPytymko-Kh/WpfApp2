using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool userIsDraggingSlider = false;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        { if((mediaElement1.Source !=null) && (mediaElement1.NaturalDuration.HasTimeSpan) && (! userIsDraggingSlider))
            {
             sliProgress.Maximum = 0;
                sliProgress.Maximum = mediaElement1.NaturalDuration.TimeSpan.Milliseconds;
                sliProgress.Value = mediaElement1.Position.TotalSeconds;
            }
            
        }

        private void BtnPlay(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void BtnPause(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
        }

        private void BtnStop(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = (double)volumeSlider.Value;
        }

        private void SliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider= true;
        }

        private void SliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaElement1.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text=TimeSpan.FromSeconds(sliProgress.Value).ToString(@"\hh\:mm\:ss");
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog()==true)
                mediaElement1.Source= new Uri(openFileDialog.FileName);
        }

    }
}