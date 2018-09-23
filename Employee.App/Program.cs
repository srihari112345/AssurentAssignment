using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Employee.App
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }
        string Name { get; set; }
        string BaseLocation { get; set; }

        // Employee[] Employees { get; set; } 

    }
    public interface IEmployeeList
    {
        Employee[] Employees { get; set; }
    }
    public class EmployeeList: IEmployeeList
    {
        public Employee[] Employees { get; set; } 
    }

    public class Employee : IEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string BaseLocation { get; set; }

        public Employee(int employeeId, string name, string baseLocation) {
            this.EmployeeId = employeeId;
            this.Name = name;
            this.BaseLocation = baseLocation;
        }

        public Employee() { }
    }

    
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            
           // Console.WriteLine("Hello World!");
            RunAsync().GetAwaiter().GetResult() ;
        }

        static async Task RunAsync()
        {

            List<Employee> employees = new List<Employee>();

            try
            {
                Console.ReadKey();
                client.BaseAddress = new Uri("http://localhost:44362/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "https://localhost:44362/api/employee/single/project";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<List<Employee>>();
                    Console.WriteLine(employees[0].Name);
                    Console.ReadKey();
                }

                var contentsToWriteToFile = JsonConvert.SerializeObject(employees);

                StreamWriter writer = new StreamWriter("D:\\Project\\AssurantAssignment\\Employee.txt", false);
                writer.Write(contentsToWriteToFile);

                //using (StreamWriter stream = File.WriteAllLines("D:\\Project\\AssurantAssignment\\Employee.txt", FileMode.))
                //{
                //    // var bformatter = new System.Runtime.Serialization.;
                //    // contentsToWriteToFile.Serialize(stream, employees);
                //    stream.Write(contentsToWriteToFile);


                //}

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }
        }
    }
}
