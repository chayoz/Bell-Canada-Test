using Bell_Canada_Test.Data;
using Bell_Canada_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace Bell_Canada_Test.Controllers
{
    public class ITSupportController : Controller
    {
        private readonly ITSupportContext _context;

        public ITSupportController(ITSupportContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Select unique department names of the employeers
            var departments = await _context.Employees.Select(e => e.Department).Distinct().ToListAsync();

            //format departments correctly to be displayed
            List<SelectListItem> dep_names = new List<SelectListItem>();
            foreach (string dep in departments)
            {
                dep_names.Add(new SelectListItem
                {
                    Text = dep,
                    Value = dep
                });
            }

            //Pass department names to a viewbag so they are accessible in the view page
            ViewBag.dep_names = dep_names;
            Ticket ticket = new Ticket { DateReceived = DateTime.Now};
            return View(ticket);
        }

        [HttpGet]
        public async Task<Array> GetEmployees(string department)
        {
            //Return an arrow of employees based on the department
            List<Employee> employees = await _context.Employees.Where(e => e.Department == department).ToListAsync();
            List<string> dep_employee = new List<string>();
            foreach (Employee employee in employees)
            {
                dep_employee.Add(employee.EmployeeName);
            }
            return dep_employee.ToArray();
        }

        [HttpGet]
        public async Task<string> GetTicketStats()
        {
            //Retrieve all tickets from the database and make a list of the distinct project names
            List<Ticket> tickets = await _context.Tickets.Distinct().ToListAsync();
            List<string> projects = tickets.Select(x => x.ProjectName).Distinct().ToList();
            List<Stats> stats = new List<Stats>();

            //Go through the distinct project names and count the amount of tickets for each.
            foreach (string project in projects)
            {
                Stats stat = new Stats();
                stat.project = project;
                stat.total = tickets.Where(x => x.ProjectName == project).Count();
                stats.Add(stat);
            }

            return JsonSerializer.Serialize(stats);
        }

        public IActionResult Chart()
        {
            return View();
        }

        public async Task<IActionResult> ViewTickets(string searchType = "", string filter = "", DateTime filter2 = new DateTime())
        {     
            //Based on filter option chosen, return the appropriate results.
            if (String.IsNullOrEmpty(searchType) || (String.IsNullOrEmpty(filter) && filter2==DateTime.MinValue))
            {
                return View(await _context.Tickets.ToListAsync());
            }
            
            if (searchType == "1")
            {
                return View(await _context.Tickets.Where(t => t.ProjectName.Contains(filter)).ToArrayAsync());
            }

            if (searchType == "2")
            {
                return View(await _context.Tickets.Where(t => t.DepartmentName.Contains(filter)).ToArrayAsync());
            }

            if (searchType == "3")
            {
                return View(await _context.Tickets.Where(t => t.RequestedBy.Contains(filter)).ToArrayAsync());
            }

            if (searchType == "4")
            {
                return View(await _context.Tickets.Where(t => t.Description.Contains(filter)).ToArrayAsync());
            }

            if (searchType == "5")
            {
                return View(await _context.Tickets.Where(t => t.DateReceived.Month == filter2.Month && t.DateReceived.Year == filter2.Year && t.DateReceived.Day == filter2.Day).ToArrayAsync());
            }

            List<Ticket> tickets = new List<Ticket>();
            return View(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([Bind("Id,ProjectName,DepartmentName,RequestedBy,Description,DateReceived")] Ticket ticket)
        {
            //Ensure the ticket is valid and add it to the database
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            catch (DbUpdateException)
            {
                //Log the error.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction("Index");
        }
    }

    public class Stats
    {
        public string? project { get; set; }
        public int total { get; set; }
    }
}
