using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi_WithBlazor_SignalR.DBFolder;
using WebApi_WithBlazor_SignalR.Hubs;
using WebApi_WithBlazor_SignalR.Models;

namespace WebApi_WithBlazor_SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IHubContext<SignalHub> _hubContext;
        private readonly AppDbContext _appDbContext;

        public EmployeeController(IHubContext<SignalHub> hubContext , AppDbContext appDbContext )
        {
            _hubContext = hubContext;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("PushEmployee")]
        public IActionResult PushEmployee(int id , string name , string address)
        {
            Employee employee = new Employee();
            employee.Id = id;
            employee.Name = name;
            employee.Address = address;

            _appDbContext.employees.Add(employee);
            _hubContext.Clients.All.SendAsync("ReceiveEmployee", employee);
            _appDbContext.SaveChanges();
            return Ok("Done Employee");


        }
        [HttpGet]
        [Route("PushMessage")]

        public IActionResult PushMessage (string Message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", Message);
            return Ok("DoneMessage");
        }
    }
}
