using Microsoft.Maui.Controls;
using System;

namespace MyMauiApp
{
    public partial class MainPage : ContentPage
    {
        private double _currentThreshold = 52.00;
        private int _tokens = 100;

        public MainPage()
        {
            InitializeComponent();
            UpdateThresholdLabel();
            UpdateTokensLabel();
        }

        // Update the displayed threshold value when the slider moves
        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            _currentThreshold = Math.Round(e.NewValue, 2); // Round to two decimal places
            UpdateThresholdLabel();
        }

        // Update threshold label display
        private void UpdateThresholdLabel()
        {
            ThresholdLabel.Text = $"Win if roll is above: {_currentThreshold:F2}";
        }

        // Update tokens display
        private void UpdateTokensLabel()
        {
            TokensLabel.Text = $"Tokens: {_tokens}";
        }

        // Logic for rolling and determining if the user wins
        private void OnBetButtonClicked(object sender, EventArgs e)
        {
            if (_tokens <= 0)
            {
                ResultLabel.Text = "No tokens left to bet!";
                return;
            }

            Random random = new Random();
            double roll = Math.Round(random.NextDouble() * 99.99, 2); // Random roll between 0.00 and 99.99

            // Determine if it's a win or loss
            if (roll >= _currentThreshold)
            {
                // Calculate winnings based on threshold
                double multiplier = Math.Max(1.01, _currentThreshold / 100);
                int winnings = (int)Math.Round(_tokens * multiplier);
                _tokens += winnings;
                ResultLabel.Text = $"You rolled {roll:F2} - Win! You earned {winnings} tokens!";
                ResultLabel.TextColor = Colors.Green;
            }
            else
            {
                // Losing the bet, tokens are reduced
                _tokens = Math.Max(0, _tokens - 10); // Losing 10 tokens per loss
                ResultLabel.Text = $"You rolled {roll:F2} - Loss. Better luck next time!";
                ResultLabel.TextColor = Colors.Red;
            }

            UpdateTokensLabel(); // Refresh token display
        }
    }
}
