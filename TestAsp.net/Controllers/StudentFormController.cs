using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TestAsp.net.Models;

namespace TestAsp.net.Controllers
{
    public class StudentFormController : Controller
    {
        // GET: StudentForm
        public ActionResult Index()
        {
            StudentViewModel country = new StudentViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/api/Student/");
                client.DefaultRequestHeaders.Accept.Clear();
                //HTTP GET
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("GetCountries")).Result;
                if (response.IsSuccessStatusCode)
                {
                    country.Countries = JsonConvert.DeserializeObject<List<Country>>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View(country);
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            using (var client = new HttpClient())
            {
                List<Student> studentData = new List<Student>();
                client.BaseAddress = new Uri("https://localhost:44333/api/Student/");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress + string.Format("AddStudent"), student).Result;
            }


            return RedirectToAction("StudentDetails");
        }
        public ActionResult StudentDetails()
        {
            List<Student> student = new List<Student>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/api/Student/");
                client.DefaultRequestHeaders.Accept.Clear();
                //HTTP GET
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("GetStudents")).Result;
                if (response.IsSuccessStatusCode)
                {
                    student = JsonConvert.DeserializeObject<List<Student>>(response.Content.ReadAsStringAsync().Result);
                }

                return View(student);
            }

        }
    }
}
