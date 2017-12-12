using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assignment_2_Rask.Models
{
    [Serializable]
    public class SubmissionDATA
    {
        //constructors for class
        public SubmissionDATA() { }
        public SubmissionDATA(int NewEntryNumber, int NewTicketID) { EntryNumber = NewEntryNumber; TicketID = NewTicketID; }
        public SubmissionDATA(int NewEntryNumber) { EntryNumber = NewEntryNumber; }

        //Functions


        //Saves a object to a .dat file
        public void SaveToFile()
        {
            using (FileStream strm = new FileStream("TicketDATA/Entry" + EntryNumber + ".dat", FileMode.Create))
            {
                IFormatter fmt = new BinaryFormatter();
                fmt.Serialize(strm, this);
            }
        }



        //Gets object from a file.
        public void LoadFromFile()
        {
            try
            {
                SubmissionDATA SubmissionFromFile = new SubmissionDATA();

                //Opens a file stream for entry number
                using (FileStream strm = new FileStream("TicketDATA/Entry" + EntryNumber + ".dat", FileMode.OpenOrCreate)) 
                {
                    //loads the object from the file.
                    IFormatter fmt = new BinaryFormatter();
                    SubmissionFromFile = fmt.Deserialize(strm) as SubmissionDATA;

                    //Overwrites information from the file to obejct
                    FirstName = SubmissionFromFile.FirstName;
                    LastName = SubmissionFromFile.LastName;
                    Phone = SubmissionFromFile.Phone;
                    Email = SubmissionFromFile.Email;
                    AreTaken = SubmissionFromFile.AreTaken;
                    TicketID = SubmissionFromFile.TicketID;
                }
            }
            //if the file doesn't excist a new version will be created. 
            catch (System.Runtime.Serialization.SerializationException) 
            {
                if(TicketID == 0)
                {
                    Random random = new Random();
                    TicketID = random.Next(1, 2000);
                }
                SaveToFile();
                Console.Error.WriteLine("ERROR!!! File not fund: Entry{0].dat, new version has been created", EntryNumber);
            }
        }



        //Variables
        [Required]
        [Display(Name = "Ticket ID")]
        public int TicketID { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [RegularExpression(@"^[0-9]{8,10}$")]
        [Required]
        [Display(Name = "Mobile")]
        public string Phone { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public int EntryNumber { get; set; }
        public bool AreTaken { get; set; } 
    }
}
