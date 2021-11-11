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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace BAARS_4_Tester
{
    /* 
     * Main Window for the BAARS 4 Tester
     * */

    public partial class MainWindow : Window
    {

        // Our two lists for the table
        List<Tester> names = new List<Tester>();
        List<Tester> filteredNames = new List<Tester>();

        // Runs on start of window
        public MainWindow()
        {
            InitializeComponent();
            QuickScoreButton.Content = "Quickscore for \nParent/Teacher Forms";
            LoadDataIntoTable();


            firstNameCheckBox.Visibility = Visibility.Hidden;
            middleNameCheckBox.Visibility = Visibility.Hidden;
            lastNameCheckBox.Visibility = Visibility.Hidden;
            firstNameCheckBox.IsChecked = true;
            middleNameCheckBox.IsChecked = true;
            lastNameCheckBox.IsChecked = true;
        }

        // Button to get user to take test, opens user information input window
        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {
            new TesterInfoWindow().Show();
        }

        // Quickly calculate the score
        private void QuickScoreButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tester currentSelected = filteredNames[Table.SelectedIndex];

                MessageBox.Show(currentSelected.path);



            }
            catch
            {
                MessageBox.Show("Nothing Selected");
            }
        }

        //Button to refresh datagrid. Mainly for testing purposes. May toggle visibility before final release
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadTable();
        }

        //Button for deleting directories. Mainly for development purposes. May be made invisible for final release
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            //var cellInfos = Table.SelectedItem;
            int rowNumber = Table.SelectedIndex;

            if (Directory.Exists(names.ElementAt(rowNumber).path))
            {
                Directory.Delete(names.ElementAt(rowNumber).path, true);
            }

            names.RemoveAt(rowNumber);
            ReloadTable();
        }

        private void showResultsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tester currentSelected = filteredNames[Table.SelectedIndex];
                new TesterAnswers(currentSelected).Show();
            }
            catch
            {
                MessageBox.Show("Nothing Selected");
            }
        }

        //Button for toggling advanced search functionality
        //This should make things relatively more simple/straightforward based on what we already have
        private void ToggleAdvancedSearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!firstNameCheckBox.IsVisible)
            {
                firstNameCheckBox.Visibility = Visibility.Visible;
                middleNameCheckBox.Visibility = Visibility.Visible;
                lastNameCheckBox.Visibility = Visibility.Visible;
                ToggleAdvancedSearchButton.Content = "Basic Search";
            }
            else
            {
                firstNameCheckBox.Visibility = Visibility.Hidden;
                middleNameCheckBox.Visibility = Visibility.Hidden;
                lastNameCheckBox.Visibility = Visibility.Hidden;
                firstNameCheckBox.IsChecked = true;
                middleNameCheckBox.IsChecked = true;
                lastNameCheckBox.IsChecked = true;
                ToggleAdvancedSearchButton.Content = "Advanced Search Options";
            }
        }

        //This gives us a way to update the table any time new content is added
        private void ReloadTable()
        {
            Table.ItemsSource = null; //In theory, this is unneccessary. But we'll leave it in for now anyways.
            LoadDataIntoTable();
        }

        // Loads the names into the main window table
        private void LoadDataIntoTable()
        {
            Debug.WriteLine("here");
            names.Clear(); //clearing names here prevents duplicates from showing up on reload


            if (Directory.Exists("Tester_Profiles\\"))
            {
                //This should work for now, but could definitely use a bit of cleaning.
                //Might be worthwhile to have some sort of "on first entry" method that creates the Tester_Profiles
                // directory in mainwindow instead of creating it the first time somebody creates a new tester
                string[] files1 = Directory.GetDirectories("Tester_Profiles\\");

                int numOfPeople = files1.Length;

                for (int i = 0; i < numOfPeople; i++)
                {
                    names.Add(new Tester(files1[i]));
                    filteredNames.Add(new Tester(files1[i]));
                }

                Table.IsReadOnly = true;
                Table.ItemsSource = names;
            }



        }

        // Researches table everytime the text is changed
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadTable();
            SearchTable();
        }

        // Searches the table depending on first/middle/last name depending on user
        private void SearchTable()
        {
            filteredNames.Clear();

            if (SearchBox.Text.Equals(""))
            {
                filteredNames.AddRange(names);
            }
            else
            {
                if (firstNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.firstName.Contains(SearchBox.Text) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
                if (middleNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.middleName.Contains(SearchBox.Text) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
                if (lastNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.lastName.Contains(SearchBox.Text) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
            }

            Table.ItemsSource = filteredNames.ToList();
        }

        // Researches table through first name
        private void FirstName_Checked(object sender, RoutedEventArgs e)
        {
            firstNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Researches table through middle name
        private void MiddleName_Checked(object sender, RoutedEventArgs e)
        {
            middleNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Researches table through last name
        private void LastName_Checked(object sender, RoutedEventArgs e)
        {
            lastNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Opens up new windoww with the testers answers 
        private void Table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = sender as DataGridRow;
                new TesterAnswers(filteredNames[row.GetIndex()]).Show();
            }
            catch
            {

            }
        }
    }
}
