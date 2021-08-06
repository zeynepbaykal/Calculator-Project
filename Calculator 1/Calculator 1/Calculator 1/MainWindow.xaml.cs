using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Calculator_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            File.AppendAllText("users.txt","");
            LoginWindow.Focus();
            DictionaryUserVePassword();

        }
        public char separator = ':';
        //Methodlar
        public void Loginsurec()
        {
            DictionaryUserVePassword();
            tabCalculator.IsEnabled = true;
            tabCalculator.Focus();
            LoginWindow.IsEnabled = false;
            RegisterWindow.IsEnabled = false;
        }
        public void Logoutsurec()
        {
            DictionaryUserVePassword();
            tabCalculator.IsEnabled = false;
            LoginWindow.IsEnabled = true;
            LoginWindow.Focus();
            RegisterWindow.IsEnabled = true;
        }
        Dictionary<string, string> allUsers;
        private void DictionaryUserVePassword()
        {
            
            allUsers = new Dictionary<string, string>();
            foreach (var item in File.ReadLines("users.txt"))
            {
                if (!allUsers.ContainsKey(item.Split(':').First()))
                {    //first one username second one password
                    allUsers.Add(item.Split(':')[0], item.Split(':')[1]);//first one key , second value
                }
            }
        }





        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.ToLower().Trim();

            if (userName.Length > 20 || userName.Length < 3)
            {
                MessageBox.Show("User name must be between 3 and 20 characters.");
                return;
            }
            if (char.IsDigit(userName[0]))
            {
                MessageBox.Show("başta numrar olmaz");
                return;
            }
            else if (txtEMail.Text.Length > 50 || txtEMail.Text.Length < 6)
            {
                MessageBox.Show("E mail must be between 6 and 50 characters.");
                return;

            }
            else if (txtEMail.Text.Contains("@") == false)
            {
                MessageBox.Show("Your e-mail address must contain the '@' character.");
                return;
            }
            else if (pswPassword.Password.Length > 20 || pswPassword.Password.Length < 8)
            {
                MessageBox.Show("Password must be between 8 and 20 characters.");
                return;
            }
            else if (!pswPassword.Password.Any(char.IsUpper))
            {
                MessageBox.Show("Password must contain at least one capital case letter.");
                return;
            }
            else if (!pswPassword.Password.Any(char.IsLower))
            {
                MessageBox.Show("Password must contain at  least one lower case letter.");
                return;
            }
            else if (!pswPassword.Password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must contain at least one diğit.");
                return;
            }
            else if (checkforspecialcaracter(pswPassword.Password) == false)
            {
                MessageBox.Show("özel karakter eklemyi unuttubz");
                return;
            }
            else if (pswPassword.Password != pswRePassword.Password)
            {

                MessageBox.Show("passworler eşit değil");
                return;
            }
            if (allUsers.ContainsKey(userName))
            {
                MessageBox.Show("bu kullanıcı kayıtlı");
                return;
            }
            else
            {
                //
                string encriptedPassword = encrtpy.ComputeSha256Hash(pswPassword.Password);
                System.IO.File.AppendAllText("users.txt", userName + separator + encriptedPassword  +  System.Environment.NewLine);
                File.AppendAllText("user.email.txt", userName + separator + txtEMail.Text);
                IsSuccess = true;
                Loginsurec();
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (allUsers != null)
            {
                if (allUsers.ContainsKey(txtLogin.Text.ToLower().Trim()))
                {
                    if (allUsers[txtLogin.Text.ToLower().Trim()] == encrtpy.ComputeSha256Hash(pswLogin.Password))
                    {
                        Loginsurec();

                    }
                    else
                    {
                        MessageBox.Show("şifre hatalı.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("böyle bir kullanıcı adı yok.");
                    return; 
                }
            }
        }

        //private void lblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtUserName.Text = "";
        //    IsSuccess = false;
        //    this.Close();
        //}
        private void BtnRegister_degisme_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow.Focus();
        }



        private void btn_0_Click(object sender, RoutedEventArgs e)
        {
            if (txtSonuc.Text == "0")
            {
                txtSonuc.Text += "";
            }
            else
            {
                txtSonuc.Text += "0";
            }
        }


        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "2";
        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "1";
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "3";
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "4";
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "5";
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "6";
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "7";
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "8";
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "9";
        }

        private void btn_Comma_Click(object sender, RoutedEventArgs e)
        {
            if ((txtSonuc.Text == "") | (txtSonuc.Text.EndsWith(",")))
            {
                txtSonuc.Text += "";
            }
            else
            {
                txtSonuc.Text += "";
            }
        }
        private void btn_Point_Click(object sender, RoutedEventArgs e)
        {
            if ((txtSonuc.Text == "") | (txtSonuc.Text.EndsWith(",")))
            {
                txtSonuc.Text += "";
            }
            else
            {
                txtSonuc.Text += "";
            }
        }
        private void btn_BracketsRight_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "(";
        }

        private void btn_BracketsLeft_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += ")";
        }

        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "+";
        }
        private void btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "-";
        }
        private void btn_Multiply_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "x";
        }

        private void btn_Divided_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "/";
        }

        private void btn_Equals_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text += "=";

        }

        private void btn_C_Click(object sender, RoutedEventArgs e)
        {
            txtSonuc.Text = "";

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Logoutsurec();
        }


        public bool IsSuccess = false;




        private bool checkforspecialcaracter(string input)
        {
            char[] lstNotAllowedChars = new char[] { ',', '.', '%', '-', '&', '/', '!', '?', '=', '$', '\\' };
            int result = input.IndexOfAny(lstNotAllowedChars, 0);
            return result > 0 == true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}


