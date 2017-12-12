using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment_2_Rask.Models;
using System.IO;
using System.Diagnostics;

namespace Assignment_2_Rask.Controllers
{
    public class SubmissionController : Controller
    {
        //Homepage
        public IActionResult Index()
        {
            return View();
        }


        //Generating Ticket numbers
        public IActionResult GenerateNewTickets()
        {
            return View();
        }


        //Show of all submissions
        public IActionResult ViewSubmission()
        {
            
            int TotalNumEntries = 100;
            List<SubmissionDATA> ListOfValidSubmissions = new List<SubmissionDATA>();
            string fileName = "TicketDATA/Test.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(fileName)); 
            for (int i = 0; i < TotalNumEntries; ++i)
            {
                SubmissionDATA Temp = new SubmissionDATA(i);
                Temp.LoadFromFile(); 
                if(Temp.AreTaken)
                    //If the variable AreTaken=true, the objects is added to the list 
                    ListOfValidSubmissions.Add(Temp); 
            }
            //Shows the list as input
            return View(ListOfValidSubmissions);
        }


        //Submit page
        public IActionResult Submit()
        {
            return View();
        }


        //Handling requrest from the pages
        [HttpPost]
        [ValidateAntiForgeryToken]
        //handles request for a new submission
        public IActionResult Submit(int id, [Bind("EntryNumber, TicketID, FirstName, LastName, Email, Phone")] SubmissionDATA InputSubmission)
        {
            //Checks if inputs are valid
            if (ModelState.IsValid)
            {
                int TotalNumEntries = 100;

                for (int i = 0; i < TotalNumEntries; ++i)
                {
                    SubmissionDATA SubmissionFromFile = new SubmissionDATA(i);
                    SubmissionFromFile.LoadFromFile();
                    if (SubmissionFromFile.TicketID == InputSubmission.TicketID)
                    {
                        //Check Are taken i objekt 
                        if (!SubmissionFromFile.AreTaken)
                        {
                            //if AreTaken is true data will be saved
                            InputSubmission.AreTaken = true;
                            InputSubmission.EntryNumber = SubmissionFromFile.EntryNumber;
                            InputSubmission.SaveToFile();
                            return RedirectToAction(nameof(Index));
                        }
                        break; //Stops loop the rest after it has been fund.
                    }
                }
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        //Creates new tickt numbers
        public IActionResult GenerateTickets()
        {
            if (ModelState.IsValid)
            {
                int TotalNumEntries = 100;
                GenerateTickets NewTickets = new GenerateTickets();
                NewTickets.Generate(TotalNumEntries);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}