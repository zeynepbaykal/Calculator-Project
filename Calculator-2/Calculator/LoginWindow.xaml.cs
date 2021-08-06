using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ZB.Calculator
{
    /// <summary>
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void lblRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Owner = this;
            registerWindow.ShowDialog();
            if (registerWindow.IsSuccess==true)
            {
                txtLogin.Text = registerWindow.txtUserName.Text;
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = UserOperations.CheckUser(txtLogin.Text.ToLower().Trim().Replace(" ", ""),
                Md5Operations.CreateMD5(pswLogin.Password));

            if (string.IsNullOrEmpty(result))
                this.Close();
            else
                MessageBox.Show(result);
        }
    }
}
