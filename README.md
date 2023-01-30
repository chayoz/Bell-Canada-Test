
## Bell Canada Test (IT Support)

As requested, I've finished and submitted the web application. Documentation, Tech stack and installation information can be found below.
## Features

The application provides the following features:
- Create Ticket Page: Includes a form with all the fields mendatory in order to submit the ticket. 
- View Tickets: Shows all the tickets saved in the database. Includes a filter at the top with all the requested filters.


## Tech Stack

**Client:** ASP.NET Core MVC 6.0

**Database:** EntityFramework 7.0

**Programming languages / libraries:** C#, Javascript, JQuery, Bootstrap


## Installation
**Requirements:**
- Microsoft Visual Studio (Application has been created and tested on the 2022 version)
- "ASP.NET and web development" Workload installed
- .NET 6.0
- EntityFramework 6.0+

Clone the project in your desired folder with:
```bash
  git clone https://github.com/chayoz/Bell-Canada-Test.git
```
Finally import the project solution from Visual Studio:
```bash
  Go to: File > Open > Project/Solution and select Bell-Canada-Test.sln
  Or simply run Bell-Canada-Test.sln and open with Visual Studio
```
The database should be automatically created the first time you run the project. In case it doesn't and the migrations folder is missing, navigate to Tools > NuGet Package Manager > Package Manager Console and run the following:
```bash
  Add-Migration InitialCreate
  Update-Database
```
## Documentation

In this section I will guide you through the project and mention the essensial functions and files.

**Models**

First of all, since we are using ASP.NET Core MVC we are working with the Model-View-Controller data stracture. So we first create the models for our tables which can be both found at */Models/* with the names *Employee.cs* and *Ticket.cs*

Code preview for tables:
```
public class Employee
{
    public int Id { get; set; }
    public string? EmployeeName { get; set; }
    public string? Department { get; set; }
}
```
```
public class Ticket
{
    public int Id { get; set; }
    public string? ProjectName { get; set; }
    public string? DepartmentName { get; set; }
    public string? RequestedBy { get; set; }
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateReceived { get; set; }

}
```

The Employee table will be used to store information about the employees and their departments.

The Ticket will be used to store the tickets submitted by the employees to IT Support.
* * *
\
**Context & Database**

Next we setup the Context class which is responsible for configuring and saving data to the database based on the models we created. The file can be found at */Data/ITSUpportContext.cs*

File preview:
```
public class ITSupportContext : DbContext
  {
      public ITSupportContext(DbContextOptions<ITSupportContext> options) : base(options)
      {
      }

      public DbSet<Employee> Employees { get; set; }
      public DbSet<Ticket> Tickets { get; set; }
  }
```
To setup and seed our database with data, we first create a file which can be found at */Data/DbInitializer.cs* and contains the following code:
```
context.Database.EnsureCreated();

//Check if database is already seeded
if (context.Employees.Any())
{
    return;
}

var employees = new Employee[]...;

foreach (Employee e in employees)
{
    context.Employees.Add(e);
}

context.SaveChanges();
```

The first line ensures that the database is created. If not, then it creates the database and tables based on the models we've created.

After that we check if the employees table has already been seeded, and if not we proceed to add the current employees.

Finally we modify the Program.cs file to add the our context (ITSupportContext) as a service by providing it with the connection string and proceed to call the DbInitializer before running the rest of the program.
* * *
\
**API's and View's**

Core MVC works with Controllers and Views. All of the Controllers can be found at */Controllers/* and All of the Views can be found at */Views/*.

The home page is very simple. It consists of the home controller and an index.cshtml(View). Each controller function corresponds to a View of the same name. By default the index function of the home controller is called when the application launches. This can be changed at *Program.cs*:
```
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

To organize our ticket pages and API's we have the ITSupportController. It provides 2 pages:
- The index page which is where the employee can fill the ticket form.
- The ViewTickets page which is where all the saved tickets in the database can be viewed.

In order to update the RequestedBy drop down list, we've created an API which can be called through */ITSupport/GetEmployees*, it takes a string parameters which is the department selected and returns an array of the department's employees.

To detect this change we are using a JQuery function called *.change* which detects whenever an element from the department drop down list is selected, makes a GET request to the API and fills the RequestedBy list with the results. The code can be seen below and is also located at the *ITSupport Index.cshtml* View.
```
<script type="text/javascript">
    $('#reqby').attr('disabled', true);
    $("#dep").change(function (){
        var department = $('#dep');
        //Make a get request to retrieve employees based on the selected department
        $.ajax({
            type: "GET",
            url: "ITSUpport/GetEmployees",
            data: {
                department: department.val()
            },
            success: function(data) {
                var result ="";
                $.each(data, function(id, employee) {
                    result += '<option value="' + employee + '">' + employee + '</option>';
                });
                $('#reqby').attr('disabled', false);
                $("#reqby").html('<option value="">--Select Employee--</option>'+result);
            },
            error: function(xhr){
                alert("request failed");
            }
        });
    });
</script>
```

Once the form is filled and submitted, we make a POST request at the *ITSupport/CreateTicket* API which takes a *Ticket* object as a parameters and saves it in the database.

Finally ViewTickets function takes optional parameters, which are the filters chosen, and based on them returns a list of the found results.


