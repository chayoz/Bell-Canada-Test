using Bell_Canada_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Bell_Canada_Test.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ITSupportContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ITSupportContext>>()))
            {
                context.Database.EnsureCreated();

                //Check if database is already seeded
                if (context.Employees.Any())
                {
                    return;
                }

                var employees = new Employee[]
                {
                new Employee{EmployeeName="Roma Marcell",Department="Division of Telecommunications Extranet Development"},
                new Employee{EmployeeName="Hugo Wess",Department="Extranet Multimedia Connectivity and Security Division"},
                new Employee{EmployeeName="Kelvin Lahr",Department="Branch of Extranet Implementation"},
                new Employee{EmployeeName="Janelle Newberg",Department="Division of Telecommunications Extranet Development"},
                new Employee{EmployeeName="Mellie Lombard",Department="Branch of Intranet Computer Maintenance and E-Commerce PC Programming"},
                new Employee{EmployeeName="Reita Abshire",Department="Wireless Extranet Backup Team"},
                new Employee{EmployeeName="Dalila Vickrey",Department="Database Programming Branch"},
                new Employee{EmployeeName="Idella Dallman",Department="Branch of Extranet Implementation"},
                new Employee{EmployeeName="Farah Eldridge",Department="Hardware Backup Department"},
                new Employee{EmployeeName="Lana Montes",Department="Hardware Backup Department"},
                new Employee{EmployeeName="Leola Thornburg",Department="Extranet Multimedia Connectivity and Security Division"},
                new Employee{EmployeeName="Marcelo Paris",Department="Database Programming Branch"},
                new Employee{EmployeeName="Ione Tomlin",Department="Multimedia Troubleshooting and Maintenance Team"},
                new Employee{EmployeeName="Hilario Masterson",Department="Multimedia Troubleshooting and Maintenance Team"},
                new Employee{EmployeeName="Janice Skipper",Department="Wireless Extranet Backup Team"},
                new Employee{EmployeeName="Keren Gillespi",Department="Office of Statistical Data Connectivity"},
                new Employee{EmployeeName="Linh Leitzel",Department="Division of Telecommunications Extranet Development"},
                new Employee{EmployeeName="Rosalia Ayoub",Department="Division of Application Security"},
                new Employee{EmployeeName="Shawna Hood",Department="Branch of Intranet Computer Maintenance and E-Commerce PC Programming"},
                new Employee{EmployeeName="Wilfredo Stumpf",Department="Network Maintenance and Multimedia Implementation Committee"},
                new Employee{EmployeeName="Alexandra Brendle",Department="Office of Statistical Data Connectivity"},
                new Employee{EmployeeName="Luciano Riddell",Department="Mainframe PC Development and Practical Source Code Acquisition Team"},
                new Employee{EmployeeName="Boyce Perales",Department="Network Maintenance and Multimedia Implementation Committee"},
                new Employee{EmployeeName="Alissa Perlman",Department="Division of Telecommunications Extranet Development"},
                new Employee{EmployeeName="Latoyia Kremer",Department="Network Maintenance and Multimedia Implementation Committee"},
                new Employee{EmployeeName="Tawna Blackmore",Department="Wireless Extranet Backup Team"},
                new Employee{EmployeeName="Claudine Valderrama",Department="Hardware Backup Department"},
                new Employee{EmployeeName="Katheryn Lepak",Department="Network Maintenance and Multimedia Implementation Committee"},
                new Employee{EmployeeName="Sage Bow",Department="Multimedia Troubleshooting and Maintenance Team"},
                new Employee{EmployeeName="Altha Rudisill",Department="Hardware Backup Department"},
                new Employee{EmployeeName="Olympia Vien",Department="Division of Application Security"},
                new Employee{EmployeeName="Olene Pyron",Department="PC Maintenance Department"},
                new Employee{EmployeeName="Delorse Searle",Department="Extranet Multimedia Connectivity and Security Division"},
                new Employee{EmployeeName="Greta Quigg",Department="Bureau of Business-Oriented PC Backup and Wireless Telecommunications Control"},
                new Employee{EmployeeName="Kenneth Bowie",Department="Branch of Intranet Computer Maintenance and E-Commerce PC Programming"},
                new Employee{EmployeeName="Colton Kranz",Department="Hardware Backup Department"},
                new Employee{EmployeeName="Kasie Barclay",Department="Multimedia Troubleshooting and Maintenance Team"},
                new Employee{EmployeeName="Thresa Levins",Department="Extranet Multimedia Connectivity and Security Division"},
                new Employee{EmployeeName="Diego Hasbrouck",Department="Software Technology and Networking Department"},
                new Employee{EmployeeName="Tomoko Gale",Department="Database Programming Branch"},
                };

                foreach (Employee e in employees)
                {
                    context.Employees.Add(e);
                }

                context.SaveChanges();
            }
        }
    }
}
