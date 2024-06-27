using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CityWasteManagement.Services;
using CityWasteManagement.Models;
using Startup.Models;

namespace CityWasteManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var notifications = _notificationService.GetAll(pageNumber, pageSize);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var notification = _notificationService.GetById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdNotification = _notificationService.Add(notification);
            return CreatedAtAction(nameof(GetById), new { id = createdNotification.Id }, createdNotification);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            notification.Id = id;
            _notificationService.Update(notification);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _notificationService.Delete(id);
            return NoContent();
        }
    }
}
