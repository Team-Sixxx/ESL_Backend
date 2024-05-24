using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESLBackend.Controllers
{
    [ApiController]
    [Route("api/bookingroom")]
    public class BookingRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBookingRooms()
        {
            var bookingRooms = _context.BookingRooms.ToList();
            return Ok(bookingRooms);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetBookingRoom(int id)
        {
            var bookingRooms = _context.BookingRooms
                                       .Where(r => r.MeetingRoomId == id)
                                       .ToList();
            if (bookingRooms == null || !bookingRooms.Any())
            {
                return NotFound();
            }
            return Ok(bookingRooms);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateBookingRoom(Models.Booking room)
        {
            // Ensure that the start time is before the end time
            if (room.StartTime >= room.EndTime)
            {
                return BadRequest(new { Message = "End time must be after start time." });
            }


            // Check for overlapping bookings in the same room
            var overlappingBooking = _context.BookingRooms
                .Where(r => r.MeetingRoomId == room.MeetingRoomId)
                .Where(r => r.StartTime < room.EndTime && room.StartTime < r.EndTime)
                .FirstOrDefault();

            // If any
            if (overlappingBooking != null)
            {
                return Conflict(new { Message = "The room is already booked for the specified time period." });
            }

            // no overlap, proceed with the booking
            _context.BookingRooms.Add(room);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookingRoom), new { id = room.Id }, room);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBookingRoom(int id, Models.Booking room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var existingRoom = _context.BookingRooms.FirstOrDefault(r => r.Id == id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Id = room.Id;
            existingRoom.EndTime = room.EndTime;
            existingRoom.StartTime = room.StartTime;
            


            _context.SaveChanges();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBookingRoom(int id)
        {
            var bookingRoom = _context.BookingRooms.FirstOrDefault(r => r.Id == id);
            if (bookingRoom == null)
            {
                return NotFound();
            }

            _context.BookingRooms.Remove(bookingRoom);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
