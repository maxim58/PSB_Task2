using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSB_Task2
{
    internal class Person
    {
        public static int MinimumAge = int.MaxValue;
        public static int MaximumAge = int.MinValue;
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Initials { get; set; }
        public int YearOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int YearsToRetirement { get; set; }

        public Person(string fullName, int yearOfBirth)
        {
            FullName = fullName;
            YearOfBirth = yearOfBirth;

            Surname = FullName.Split(' ')[0];
            Name = FullName.Split(' ')[1];
            Patronymic = FullName.Split(' ')[2];
            Initials = $"{Name.ElementAt(0)}. {Patronymic.ElementAt(0)}.";
            Age = DateTime.Today.Year - yearOfBirth;

            if (Patronymic.ElementAt(Patronymic.Length - 1) == 'а')
            {
                Gender = "Female";
                if (Age < 60)
                {
                    YearsToRetirement = 60 - Age;
                }
                else YearsToRetirement = 0;
            }
            else
            {
                Gender = "Male";
                if (Age < 65)
                {
                    YearsToRetirement = 65 - Age;
                }
                else YearsToRetirement = 0;
            }

            if (Age < MinimumAge) MinimumAge = Age;
            if (Age > MaximumAge) MaximumAge = Age;
        }
    }
}
