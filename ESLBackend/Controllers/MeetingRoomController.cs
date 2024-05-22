using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ESLBackend.models;
using Microsoft.AspNetCore.Authorization;

namespace ESLBackend.Controllers
{
    [ApiController]
    [Route("api/meetingrooms")]
    public class MeetingRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeetingRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public IActionResult GetMeetingRooms()
        {
            var meetingRooms = _context.MeetingRooms.ToList();
            return Ok(meetingRooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetMeetingRoom(int id)
        {
            var meetingRoom = _context.MeetingRooms.FirstOrDefault(r => r.Id == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }
            return Ok(meetingRoom);
        }

        [HttpPost]
        public IActionResult CreateMeetingRoom(MeetingRoom room)
        {
            _context.MeetingRooms.Add(room);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMeetingRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMeetingRoom(int id, MeetingRoom room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var existingRoom = _context.MeetingRooms.FirstOrDefault(r => r.Id == id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Name = room.Name;
            existingRoom.Capacity = room.Capacity;
            existingRoom.Location = room.Location;
            existingRoom.templateId = room.templateId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeetingRoom(int id)
        {
            var meetingRoom = _context.MeetingRooms.FirstOrDefault(r => r.Id == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            _context.MeetingRooms.Remove(meetingRoom);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
