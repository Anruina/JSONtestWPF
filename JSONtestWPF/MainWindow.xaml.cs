using System.Collections.Generic;
using System.Windows;
using JSONtestWPF.Models;
using System.Xml.Linq;
using JSONtestWPF.Data;

namespace JSONtestWPF
{
    public partial class MainWindow : Window
    {
        private readonly DAL dal;
        private readonly List<Person> people;

        public MainWindow()
        {
            InitializeComponent();
            dal = new DAL();
            people = dal.ReadDataFromFile();
            dataGrid.ItemsSource = people;
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            // Read existing data from JSON file
            List<Person> people = dal.ReadDataFromFile();

            // Add new person
            Person person = new Person
            {
                Name = txtName.Text,
                Age = int.Parse(txtAge.Text)
            };

            // Save updated data to JSON file
            dal.CreateOrUpdatePerson(person);

            MessageBox.Show("Data added successfully.");
        }

/*        private void ModifyData_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = dataGrid.SelectedItem as Person;
            if (selectedPerson != null)
            {
                CreateOrUpdate modifyWindow = new CreateOrUpdate(selectedPerson);
                modifyWindow.ShowDialog();
            
                if (modifyWindow.PersonModified)
                {

                    people.Clear();
                    people.AddRange(dal.ReadDataFromFile());
                    dataGrid.Items.Refresh();

                }
                
            }
            else
            {

                MessageBox.Show("Please select a person to modify.");

            }
        }*/

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {

            Person selectedPerson = dataGrid.SelectedItem as Person;
            if(selectedPerson != null)
            {

                dal.DeletePerson(selectedPerson.Name);

                people.Clear();
                people.AddRange(dal.ReadDataFromFile());
                dataGrid.Items.Refresh();

                MessageBox.Show("Data Deleted Succesfully.");

            }
            else
            {
                MessageBox.Show("Please select a person to delete.");
            }

        }
    }
}
