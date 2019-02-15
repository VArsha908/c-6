using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ
{
    class Program
    {
        List<Employee> employeeList;
        List<Salary> salaryList;


        public Program()
        {
            employeeList = new List<Employee>()
            {
                new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
                new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
                new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
                new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
                new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
                new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
                new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
            };
            salaryList = new List<Salary>()
            {
                new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
            };
        }

        public static void Main()
        {
            Program program = new Program();
            program.Task1();
            program.Task2();
            program.Task3();
        }

        public void Task1()
        {
            var query = from Employee in employeeList
                        join Salary in salaryList
                        on Employee.EmployeeID equals Salary.EmployeeID into sal
                        select new
                        {
                            FName = Employee.EmployeeFirstName,
                            Sum_Salary = sal.Sum(x => x.Amount)
                        };
            Console.WriteLine("The total Salary of the Employees are:");
            foreach (var item in query.OrderBy(y => y.Sum_Salary))
            {
                Console.WriteLine(item.FName + ":" + item.Sum_Salary);
            }
        }

        public void Task2()
        {
            var query1 = from Employee in employeeList
                         join Salary in salaryList
                         on Employee.EmployeeID equals Salary.EmployeeID
                         where Salary.Type == SalaryType.Monthly
                         orderby Employee.Age descending
                         select new
                         {
                             Employee.EmployeeFirstName,
                             Employee.EmployeeLastName,
                             Employee.EmployeeID,
                             Employee.Age,
                             Salary.Type,
                             Salary.Amount
                         };
            var query2 = from fname in query1
                         group fname by fname.EmployeeFirstName;
            foreach (var data in query2.Take(2).Skip(1))
            {
                foreach (var details in data)
                {
                    Console.WriteLine("The details of the second oldest employee are :");
                    Console.WriteLine("Name:" + details.EmployeeFirstName + " " + details.EmployeeLastName + " Id:" + details.EmployeeID + " Total Salary:" + details.Amount + " Age:" + details.Age + " Type of salary:" + details.Type);
                }
            }
        }
        public void Task3()
        {
            var query3 = from Employee in employeeList.Where
                         (a => a.Age > 30)
                         join Salary in salaryList
                         on Employee.EmployeeID equals Salary.EmployeeID into sal
                         orderby Employee.EmployeeID
                         select new
                         {
                             fname = Employee.EmployeeFirstName,
                             Meansalary = sal.Average(x => x.Amount)
                         };
            Console.WriteLine("Mean of Monthly, Performance and Bonus salary of employees whose age is greater than 30 are:");
            foreach (var item in query3)
            {
                Console.WriteLine(item.Meansalary);
            }
        }
        public enum SalaryType
        {
            Monthly,
            Performance,
            Bonus
        }
        public class Employee
        {
            public int EmployeeID { get; set; }
            public object EmployeeId { get; internal set; }
            public string EmployeeFirstName { get; set; }
            public string EmployeeLastName { get; set; }
            public int Age { get; set; }
        }
        public class Salary
        {
            public int EmployeeID { get; set; }
            public int Amount { get; set; }
            public SalaryType Type { get; set; }
        }
    }
}
