using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.mvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace mvcRegistrations.Controllers
{

   
    public class PagesController : Controller
    {

        private readonly RegestrationDbcontext _dbContext;

        public PagesController(RegestrationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }


        // to show us the create page 

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult create(Intern Application)
        {

            if (ModelState.IsValid)
            {
                _dbContext.Interns.Add(Application);       // data migration
                _dbContext.SaveChanges();      // update database 
                return RedirectToAction("List");
            }
            else
            {
                return View(Application);
            }
        }


        [HttpGet]
        public IActionResult List()
        {
            List<Intern> interns = _dbContext.Interns.ToList(); // Fetch all interns from the database
            return View(interns); // Pass the list of interns to the view
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var intern = _dbContext.Interns.Find(id);

            if (intern == null)
            {
                Console.WriteLine( "Inside not found");
                return NotFound();
            }

            Console.WriteLine( intern.Name);
            Console.WriteLine("Inside found and working");
            return View(intern);
        }

        [HttpPost]
        public IActionResult Edit(Intern updatedIntern)
        {
            if (ModelState.IsValid)
            {
                var existingIntern = _dbContext.Interns.Find(updatedIntern.Id);

                if (existingIntern == null)
                {
                    return NotFound();
                }

                existingIntern.Name = updatedIntern.Name;
                existingIntern.Email = updatedIntern.Email;
                existingIntern.Phone = updatedIntern.Phone;
                existingIntern.Address = updatedIntern.Address;


                _dbContext.SaveChanges();

                return RedirectToAction("List");
            }

            return View(updatedIntern);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var intern = _dbContext.Interns.Find(id);

            if (intern == null)
            {
                return NotFound();
            }

      

            return View(intern);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var intern = _dbContext.Interns.Find(id);

            if (intern == null)
            {
                return NotFound();
            }

            _dbContext.Interns.Remove(intern);
            _dbContext.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
