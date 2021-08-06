using System.Windows;
using System.Windows.Input;

namespace ZB.Calculator
{
    /// <summary>
    /// RegisterWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        public bool IsSuccess = false;
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var result = UserOperations.CreateUser(txtUserName.Text, pswPassword.Password, pswRePassword.Password, txtEMail.Text);

            if (string.IsNullOrEmpty(result))
            {
                IsSuccess = true;
                this.Close();
            }
            else
                MessageBox.Show(result);
        }

        private void lblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUserName.Text = "";
            IsSuccess = false;
           this.Close();
        }
    }
}
