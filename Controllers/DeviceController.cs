using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using oahu_api.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace oahu_api.Controllers
{
    /// <summary>
    /// The class used as an API for devices.
    /// </summary>
    [Route("api/devices")]
    [Produces("application/json")]
    [EnableCors("AllowAllOrigins")]
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Device Controller for the device API service.
        /// </summary>
        /// <param name="deviceRepository"></param>
        /// <param name="eventRepository"></param>
        public DeviceController(IDeviceRepository deviceRepository, IEventRepository eventRepository)
        {
            _deviceRepository = deviceRepository;
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// This endpoint returns all the devices in the database. 
        /// </summary>
        /// <remarks>
        /// Get all devices
        /// </remarks>
        /// <returns>All the devices</returns>
        #region snippet_GetAll
        [HttpGet]
        [Produces(typeof(IEnumerable<Device>))]
        [SwaggerResponse(200, Type = typeof(IEnumerable<Device>))]
        public IEnumerable<Device> GetAll()
        {
            return _deviceRepository.GetAll();
        }
        #endregion

        /// <summary>
        /// This endpoint returns all the events for a given device 
        /// </summary>
        /// <remarks>
        /// Get all events for a device
        /// </remarks>
        /// <param name="id">The id of the device.</param>
        /// <returns>All events from the device</returns>
        [HttpGet("{id}/events")]
        [Produces(typeof(IEnumerable<Event>))]
        [SwaggerResponse(200, Type = typeof(IEnumerable<Event>))]
        public IEnumerable<Event> Events(long id)
        {
            return _eventRepository.GetAllFromDevice(id);
        }

        /// <summary>
        /// This endpoint returns a single device with the given id.
        /// </summary>
        /// <remarks>
        /// Get a device by ID
        /// </remarks>
        /// <param name="id">The id of the device to be returned.</param>
        /// <returns>A single device.</returns>
        [HttpGet("{id}", Name = "GetDevice")]
        [Produces(typeof(Device))]
        [SwaggerResponse(200, Type = typeof(Device))]
        public IActionResult GetById(long id)
        {
            var item = _deviceRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /// <summary>
        /// This endpoint saves a new device. 
        /// </summary>
        /// <remarks>
        /// Save a new device
        /// </remarks>
        /// <param name="device">The device to be saved.</param>
        /// <returns>The route of the newly created device.</returns>
        [HttpPost]
        public IActionResult CreateDevice([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest();
            }

            _deviceRepository.Add(device);

            return StatusCode(201);
        }

        /// <summary>
        /// This endpoint saves a new event. 
        /// </summary>
        /// <remarks>
        /// Save a new  event
        /// </remarks>
        /// <param name="item">The event to be saved.</param>
        /// <returns>The route of the newly created event.</returns>
        [HttpPost("event")]
        public IActionResult CreateEvent([FromBody] Event item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _eventRepository.Add(item);

            return StatusCode(201);
        }
    }
}