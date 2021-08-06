using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ZB.Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<string> _accaptableChars = new List<string>() { "1", "2", "3", "5", "6", "7", "8", "9", "0", "+", "-", "/", "*", "(", ")", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow();
            var historyLines = HistoryOperations.ReadFromHistory(lblUserName.Content.ToString());
            lstResultHistory.Items.Clear();

            foreach (var item in historyLines)
            {
                lstResultHistory.Items.Add(item);
            }
        }

        private void AcceptInput(object sender, RoutedEventArgs e)
        {
            //To make it clean, used only one method for buttons those just fills the input box
            txtInput.Text += ((Button)sender).Content;
        }

        private void btn_Equals_Click(object sender, RoutedEventArgs e)
        {
            //bastaki, sondaki, aradaki bosluklari sil
            var validInput = txtInput.Text.Trim().Replace(" ", "");
            var result = MathOperations.EvaluateInput(validInput);
            var historyLine = validInput + " = " + result;

            lstResultHistory.Items.Insert(0, historyLine);
            HistoryOperations.AddToHistory(lblUserName.Content.ToString(), historyLine);
            txtInput.Text = string.Empty;//input box temizledik
        }

        private void btn_C_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            OpenLoginWindow();
        }

        private void OpenLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();
            lblUserName.Content = loginWindow.txtLogin.Text;
        }

        private void lstResultHistory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstResultHistory.SelectedItem != null)
                txtInput.Text = lstResultHistory.SelectedItem.ToString().Split('=')[0].Trim();
        }

        private void TxtInput_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!_accaptableChars.Contains(e.Text))
                e.Handled = true;
        }

        private void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var addedCharCount = e.Changes.ElementAt(0).AddedLength;
            var addedText = txtInput.Text.Substring(txtInput.Text.Length - addedCharCount);
            for (int i = 0; i < addedCharCount; i++)
            {
                if (!_accaptableChars.Contains(addedText.ElementAt(i).ToString()))
                {
                    txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - addedCharCount);
                    break;
                }
            }
        }
    }
}
