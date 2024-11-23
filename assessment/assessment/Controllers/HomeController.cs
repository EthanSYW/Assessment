using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class HomeController : Controller
{
    private readonly ContactContext _context;

    public HomeController(ContactContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Message"] = "Hello world";
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        ViewData["Message"] = "Hello world";
        return View();
    }

    [HttpPost]
    public IActionResult Contact(Contact contact)
    {
        if (ModelState.IsValid)
        {
            // Save the data to database
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            // navigate to submission page
            return RedirectToAction("Submissions");
        }

        
        return View(contact);
    }

    public IActionResult Submissions(string nameFilter)
    {
        // Retrieve Data
        var contacts = _context.Contacts.AsQueryable();

        // Filter
        if (!string.IsNullOrEmpty(nameFilter))
        {
            contacts = contacts.Where(c => c.Name.Contains(nameFilter));
        }

        ViewData["Message"] = "Hello world";
        return View(contacts.ToList());
    }
}
