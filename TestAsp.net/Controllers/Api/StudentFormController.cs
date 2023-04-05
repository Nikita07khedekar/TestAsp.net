using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAsp.net.Controllers.Api
{
    [RoutePrefix("api/Student")]
    public class StudentFormController : ApiController
    {

        private StudentFormEntities _context;

        StudentFormController()
        {
            _context = new StudentFormEntities();
        }


        [HttpGet]
        [Route("GetCountries")]
        public IHttpActionResult GetAllCountries()
        {
            return Ok(_context.Countries.ToList());
        }

        [HttpGet]
        [Route("GetStudents")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(_context.Students.ToList());
        }

        [HttpGet]
        [Route("GetStates/{id}")]
        public IHttpActionResult GetAllStates(int id )
        {
            var states = _context.States.Where(m => m.CountryId==id).Include(c => c.Country).Include(x => x.Country.States).Include(m => m.Students).ToList();
            if(states.Count > 0)
            return Ok(states);

            return BadRequest("There is no any state related to this country!");
        }

        [HttpGet]
        [Route("GetCities/{id}")]
        public IHttpActionResult GetAllCities(int id)
        {
            var cities = _context.Cities.Where(m => m.StateId == id).Include(x => x.State).Include(m => m.Students).ToList();
            if (cities.Count > 0)
                return Ok(cities);

            return BadRequest("There is no any City related to this State!");
        }


        [HttpPost]
        [Route("AddStudent")]
        public IHttpActionResult AddNewStudent(Student student)
        {
            if (student == null)
                return BadRequest("Can not add student!");
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
    }
}
