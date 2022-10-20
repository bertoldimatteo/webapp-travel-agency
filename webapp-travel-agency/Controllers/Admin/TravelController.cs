using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Controllers
{
    public class TravelController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Travel> travels = context.Travels.ToList();
                return View("Index", travels);
            }
        }

        public IActionResult Detail(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Travel travel = context.Travels.Where(travel => travel.Id == id).FirstOrDefault();

                if (travel == null)
                {
                    return NotFound("Il viaggio selezionato non esiste o è stato cancellato");
                }
                else
                {
                    return View("Detail", travel);
                }

            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Travel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Travel travel = new Travel();
                travel.Name = formData.Name;
                travel.Photo = formData.Photo;
                travel.Description = formData.Description;
                travel.Day = formData.Day;
                travel.Destination = formData.Destination;
                travel.Price = formData.Price;

                context.Travels.Add(travel);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update (int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Travel travel = context.Travels.Where(travel => travel.Id == id).FirstOrDefault();

            if(travel == null)
            {
                return NotFound("Viaggio non trovato");
            }

            return View(travel);
        }

        [HttpPost]
        public IActionResult Update (int id, Travel formData)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Travel travel = context.Travels.Where(travel => travel.Id == id).FirstOrDefault();

            if(travel != null)
            {
                travel.Name = formData.Name;
                travel.Photo = formData.Photo;
                travel.Description = formData.Description;
                travel.Day = formData.Day;
                travel.Destination = formData.Destination;
                travel.Price = formData.Price;
            }else
            {
                return NotFound("Viaggio non trovato");
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Travel travel = context.Travels.Where(travel => travel.Id == id).FirstOrDefault();

            if (travel == null)
            {
                return NotFound("Viaggio non trovato");
            }
            else
            {
                context.Travels.Remove(travel);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
