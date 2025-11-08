using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheBluePill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double AnimationDurationSeconds = 0.35;
        private const double SegmentWidth = 120;


        public MainWindow()
        {
            InitializeComponent();
            // Optional: Log the initialized state to console
            Console.WriteLine("Segmented Control Initialized.");
        }

        private void SegmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button clickedButton)
                {
                    // 1. Determine the target index from the button's Tag property
                    if (int.TryParse(clickedButton.Tag?.ToString(), out int targetIndex))
                    {
                        // 2. Calculate the required horizontal offset
                        double targetOffset = targetIndex * SegmentWidth;

                        // Check if the pill is already in the target position to prevent unnecessary animation
                        if (PillTranslate.X == targetOffset)
                        {
                            return;
                        }

                        // 3. Setup the animation
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            To = targetOffset,
                            Duration = TimeSpan.FromSeconds(AnimationDurationSeconds),
                            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } // Provides the fluid, smooth look
                        };

                        // 4. Start the animation on the TranslateTransform's X property
                        PillTranslate.BeginAnimation(TranslateTransform.XProperty, animation);

                        // 5. Update the text colors to reflect the new selection
                        UpdateTextColors(clickedButton);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions during the process
                Console.WriteLine($"An error occurred during segment click: {ex.Message}");
            }
        }

        private void UpdateTextColors(Button selectedButton)
        {
            // Reset all buttons to the default gray color
            MusicButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 153, 153, 153)); // #999999
            MovieButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 153, 153, 153));
            AppsButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 153, 153, 153));

            // Set the selected button's text to white
            selectedButton.Foreground = Brushes.White;
        }


    }
}
