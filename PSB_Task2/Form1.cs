using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSB_Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string staticPath;
        List<Person> people = new List<Person>();

        private void OpenFile_button_Click(object sender, EventArgs e)
        {
            people.Clear();
            Person.MaximumAge = int.MinValue;
            Person.MinimumAge = int.MaxValue;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "|*.csv";
            fileDialog.Title = "Выберите CSV-файл для обработки данных";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                staticPath = fileDialog.FileName;
                OpenAndReadFile(staticPath, people);

                if (people.Count != 0)
                {
                    label2.Text = $"Наименьший возраст: {Person.MinimumAge}";
                    label3.Text = $"Наибольший возраст: {Person.MaximumAge}";
                    label4.Text = "Средний возраст: " + new int[] { Person.MinimumAge, Person.MaximumAge }.Average().ToString();
                    ResultButton.Enabled = true;
                }
                else
                {
                    label2.Text = $"Наименьший возраст: ";
                    label3.Text = $"Наибольший возраст: ";
                    label4.Text = "Средний возраст: ";
                    ResultButton.Enabled = false;
                }
            }

        }

        private void OpenAndReadFile(string path, List<Person> people)
        {
            int counter = 0;
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                {
                    string currentLine;
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        counter++;
                        string name = currentLine.Split(';')[0];
                        int year = Convert.ToInt32(currentLine.Split(';')[1]);
                        people.Add(new Person(name, year));
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Строка {counter} \n" + ex.Message, "Обнаружена ошибка!");
                this.people.Clear();
            }
        }

        private void ResultButton_Click(object sender, EventArgs e)
        {

            List<Person> sortedPeople = new List<Person>();
            sortedPeople = people.OrderBy(x => x.YearOfBirth).ToList();


            WriteToCSV(staticPath, sortedPeople);
        }

        private void WriteToCSV(string path, List<Person> outputData)
        {
            int index = path.IndexOf(".csv");
            string newPath = path.Insert(index, "_result");
           
            using (TextWriter writer = new StreamWriter(newPath, false, Encoding.UTF8))
            {
                writer.WriteLine($"ФИО; Лет до пенсии");
                foreach (var person in outputData)
                {
                    writer.WriteLine($"{person.Surname} {person.Initials};{person.YearsToRetirement}");
                }
            }
            OpenCSV(newPath);
        }

        private void OpenCSV(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}
