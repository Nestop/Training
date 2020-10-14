using System;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

namespace JSONdata
{
    public class CompanyData
    {
        public string Name;
        public string Location;
        public Dictionary<string, EmployeeData> Employees; 
        public Dictionary<string, ProjectData> Projects;
        public Dictionary<string, WorkplaceData> Workplaces;

        public CompanyData()
        {
        }
        
        public CompanyData(string filePath)
        {
            ReadCompanyData(filePath);
        }
        
        public void ReadCompanyData(string filePath)
        {
            var jsonStr = FileReader.ReadToString(filePath);
            var json = JSON.Parse(jsonStr);
            Name = json["companyName"];
            Location = json["location"];
            Employees = new Dictionary<string, EmployeeData>();
            var employeesData = json["employees"].Linq;
            foreach (var keyValuePair in employeesData)
            {
                var key = keyValuePair.Key;
                var emp = keyValuePair.Value;
                var name = emp["name"];
                var position = emp["position"];
                var workplaceNumber = emp["workplaceNumber"];
                var phone = emp["phone"];
                var employee = new EmployeeData(name, position, workplaceNumber, phone);
                Employees[key] = employee;
            }
            
            Projects = new Dictionary<string, ProjectData>();
            var projectsData = json["projects"].Linq;
            foreach (var keyValuePair in projectsData)
            {
                var key = keyValuePair.Key;
                var proj = keyValuePair.Value;
                var genre = proj["genre"].AsStringArray;
                var releaseBuild = proj["releaseBuild"];
                var developersID = proj["developersID"].AsStringArray;
                var project = new ProjectData(genre, releaseBuild, developersID);
                Projects[key] = project;
            }
            
            Workplaces = new Dictionary<string, WorkplaceData>();
            var workplacesData = json["workplaces"].Linq;
            foreach (var keyValuePair in workplacesData)
            {
                var key = keyValuePair.Key;
                var wrkplc = keyValuePair.Value;
                var type = wrkplc["type"];
                var workplace = new WorkplaceData(type);
                Workplaces[key] = workplace;
            }
        }

        public void PrintShortCompanyInfo()
        {
            Console.WriteLine("COMPANY INFO");
            
            Console.WriteLine($"Company name: {Name}");
            Console.WriteLine($"Location: {Location}");
            Console.WriteLine($"Count of employees: {Employees.Count}");
            Console.WriteLine("\n");
        }
        
        public void PrintEmployeesInfo()
        {
            Console.WriteLine("EMPLOYEES INFO");
            
            foreach (var employee in Employees)
            {
                Console.WriteLine($"ID: {employee.Key}");
                Console.WriteLine($"Name: {employee.Value.Name}");
                Console.WriteLine($"Position: {employee.Value.Position}");
                Console.WriteLine($"WorkplaceNumber: {employee.Value.WorkplaceNumber}");
                Console.WriteLine($"Phone: {employee.Value.Phone}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintFullEmployeeInfo(string id)
        {
            Console.WriteLine($"FULL INFO: {Employees[id].Name} X");
            
            Console.WriteLine($"Position: {Employees[id].Position}");
            Console.WriteLine($"WorkplaceNumber: {Employees[id].WorkplaceNumber}");
            Console.WriteLine($"WorkplaceType: {Workplaces[Employees[id].WorkplaceNumber].Type}");
            Console.WriteLine($"Phone: {Employees[id].Phone}");
            Console.WriteLine("Work in projects: ");
            foreach (var project in Projects)
            {
                if (project.Value.DevelopersID.Contains(id))
                {
                    Console.Write($"{project.Key}; ");
                }
            }
            
            Console.WriteLine("\n");
        }
    }
}