using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JSONtestWPF.Models;

namespace JSONtestWPF.Data
{
    public class DAL
    {
        private const string FilePath = "C:\\Users\\Rachel\\source\\repos\\Anruina\\JSONtestWPF\\JSONtestWPF\\data.json";

        public List<Person> ReadDataFromFile()
        {
            List<Person> people = new List<Person>();

            if (File.Exists(FilePath))
            {
                string jsonString = File.ReadAllText(FilePath);
                people = JsonSerializer.Deserialize<List<Person>>(jsonString);
            }

            return people;
        }

        public void SaveDataToFile(List<Person> people)
        {
            string jsonString = JsonSerializer.Serialize(people);
            File.WriteAllText(FilePath, jsonString);
        }

        public void CreateOrUpdatePerson(Person person)
        {
            List<Person> people = ReadDataFromFile();

            var existingPerson = people.FirstOrDefault(p => p.Name == person.Name);
            if (existingPerson != null)
            {
                existingPerson.Age = person.Age;
            }
            else
            {
                people.Add(person);
            }

            SaveDataToFile(people);

        }

        public void DeletePerson(string name)
        {

            List<Person> people = ReadDataFromFile();

            var personToRemove = people.FirstOrDefault(p => p.Name == name);
            if (personToRemove != null)
            {
                people.Remove(personToRemove);
                SaveDataToFile(people);

            }
            else
            {
                Console.WriteLine($"Person with name '{name}' not found.");
            }


        }


    }
}
