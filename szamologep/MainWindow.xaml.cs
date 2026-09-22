using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace szamologep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GombokElhelyezese();
        }

        private void GombokElhelyezese()
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonGrid.RowDefinitions.Add(new RowDefinition());
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            string[,] feliratok =
                {
                {"7", "8", "9", "/" },
                {"4", "5", "6", "*" },
                {"1", "2", "3", "-" },
                {"C", "0", "=", "+" }
            };

            for (int i = 0; i < 4; i++) //sor
            {
                for (int j = 0; j < 4; j++)  //oszlop
                {
                    string label = feliratok[i, j];
                    Button btn = new Button
                    {
                        Content = label,
                        FontSize = 20,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(3)
                    };
                    if (char.IsDigit(label[0]))
                    {
                        btn.Background = Brushes.WhiteSmoke;
                    }
                    else if (label == "C")
                    {
                        btn.Background = Brushes.IndianRed;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        btn.Background = Brushes.DodgerBlue;
                        btn.Foreground = Brushes.White;
                    }

                    btn.Click += Button_Click;

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    ButtonGrid.Children.Add(btn);
                }

            }

        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string felirat = button.Content.ToString();
            if (felirat == "=")
            {
                Kiszamol();
            }
            else if (felirat == "C")
            {
                tb_kijelzo.Text = " ";
            }
            else
            {
                tb_kijelzo.Text += felirat;
            }

        }

        private void Kiszamol()
        {
            string muvelet = tb_kijelzo.Text;
            string help = "";
            int utolsomuveletjel = 0;

            for (int i = 0; i < muvelet.Length; i++)
            {
                if (muvelet[i] == '+' || muvelet[i] == '-' || muvelet[i] == '*' || muvelet[i] == '/')
                {
                    if (muvelet[i] == '*' || muvelet[i] == '/')
                    {
                        int j = i - utolsomuveletjel;
                        bool mehet = true;
                        string szam = ""; 
                        while (mehet)
                        {
                            if ((muvelet[i] == '+' || muvelet[i] == '-' || muvelet[i] == '*' || muvelet[i] == '/')&&j>i &&j<muvelet.Length)
                            {
                                mehet = false;
                            }
                            else 
                            {
                                szam += muvelet[i];
                            }
                            j++;
                        }
                        string szamolt = Megold(szam);
                        help.Remove(help.Length - utolsomuveletjel);
                        i += j - i+1;
                        help += szamolt;
                       
                    }
                    else
                    {
                        help += muvelet[i];
                    }
                    utolsomuveletjel = 0;
                    
                }
                else 
                {
                    help += muvelet[i];
                    utolsomuveletjel += 1;
                }
            }
        }

        private string Megold(string szam)
        {
            string szam1 = "";
            string szam2 = "";
            char jel = default;
            bool elotte = true;
            for (int i = 0; i < szam.Length; i++)
            {
                if (szam[i] == '+' || szam[i] == '-' || szam[i] == '*' || szam[i] == '/')
                {
                    jel = szam[i];
                    elotte = false;
                }
                else if (elotte)
                {
                    szam1 += szam[i];
                }
                else
                {
                    szam2 += szam[i];
                }
            }

            int x = int.Parse(szam1);
            int y = int.Parse(szam2);

            switch (jel)
            {
                case '+':
                    
                    return (x + y) + "";
                case '-':

                    return (x - y) + "";
                case '*':

                    return (x * y) + "";
                case '/':

                    return Math.Round(x / y+0.0,0) + "";
                default:
                    throw new Exception("Блять!!!");
            }
            
        }
    }
}