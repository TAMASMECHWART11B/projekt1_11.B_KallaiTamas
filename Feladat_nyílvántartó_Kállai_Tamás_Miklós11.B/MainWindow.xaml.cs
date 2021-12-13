using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Feladat_nyílvántartó_Kállai_Tamás_Miklós11.B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CheckBox> feladatok = new List<CheckBox>();
        List<CheckBox> toroltek = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            letezoFeladatok.ItemsSource = feladatok;
            toroltFeladatok.ItemsSource = toroltek;
        }


        private void FeladatHozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (feladatNev.Text != "")
            {
                CheckBox uj = new CheckBox();
                uj.Content = feladatNev.Text;
                uj.Checked += new RoutedEventHandler(CheckBox_Checked);
                uj.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                feladatok.Add(uj);
                letezoFeladatok.ItemsSource = feladatok;
                letezoFeladatok.Items.Refresh();
                feladatNev.Text = "";
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Italic;
            aktualis.Foreground = Brushes.Gray;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Normal;
            aktualis.Foreground = Brushes.Black;
        }

        private void FeladatTorol_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)letezoFeladatok.SelectedItem;
            toroltek.Add(kijelolt);
            feladatok.Remove(kijelolt);
            letezoFeladatok.Items.Refresh();
            toroltFeladatok.Items.Refresh();
        }

        private void feladatVissza_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)toroltFeladatok.SelectedItem;
            feladatok.Add(kijelolt);
            toroltek.Remove(kijelolt);
            letezoFeladatok.Items.Refresh();
            toroltFeladatok.Items.Refresh();
        }

        private void feladatVeglegtorol_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)toroltFeladatok.SelectedItem;
            toroltek.Remove(kijelolt);
            toroltFeladatok.Items.Refresh();
        }


        private void feladatMod_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)letezoFeladatok.SelectedItem;
            kijelolt.Content = feladatNev.Text;

        }
        private void letezoFeladatok_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)letezoFeladatok.SelectedItem;
            feladatNev.Text = kijelolt.Content.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            List<string> checkboxok = new List<string>();
            foreach (CheckBox x in letezoFeladatok.Items)
            {
                int allapot = 0;
                if (x.IsChecked == true)
                    allapot = 1;
                string cbJellemzoje = x.Content.ToString() + ";" + allapot;
                checkboxok.Add(cbJellemzoje);
            }
            File.WriteAllLines("feladatok.txt", checkboxok.ToArray());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var be = File.ReadAllLines("feladatok.txt");
            foreach (var x in be)
            {
                CheckBox uj = new CheckBox();
                uj.Content = x.Split(';')[0];
                uj.Checked += new RoutedEventHandler(CheckBox_Checked);
                uj.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                uj.IsChecked = x.Split(';')[1] == "1" ? true : false;
                feladatok.Add(uj);
            }
            letezoFeladatok.Items.Refresh();
            
        }
    }
}
