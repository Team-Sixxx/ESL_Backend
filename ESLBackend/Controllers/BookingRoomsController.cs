using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ESLBackend.models;
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

        [HttpGet("{id}")]
        public IActionResult GetBookingRoom(int id)
        {
            var bookingRoom = _context.BookingRooms.FirstOrDefault(r => r.Id == id);
            if (bookingRoom == null)
            {
                return NotFound();
            }
            return Ok(bookingRoom);
        }

        [HttpPost]
        public IActionResult CreateBookingRoom(BookingRoom room)
        {
            _context.BookingRooms.Add(room);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookingRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookingRoom(int id, BookingRoom room)
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
            existingRoom.Date = room.Date;
            existingRoom.Time = room.Time;
            existingRoom.Duration = room.Duration;


            _context.SaveChanges();

            return NoContent();
        }

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
