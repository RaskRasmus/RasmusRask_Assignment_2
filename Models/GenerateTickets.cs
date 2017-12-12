using System;
using System.Text;
using System.IO;

// Here are Classes to generate new valid ticket number.
namespace Assignment_2_Rask.Models
{
    [Serializable]
    class GenerateTickets
    {
        public void Generate(int TotalNumEntries)
        {
            //Generating random numbers
            Random random = new Random(); 
            //First ticket are never under 500
            int RandomTicketID = 500;
            
            string fileName_SerialNumbers = "TicketDATA/SerialNumbers.txt";
            //Checks directory excists
            Directory.CreateDirectory(Path.GetDirectoryName(fileName_SerialNumbers));

            //Findes file with vaild ID
            var SerialNumbersFile = new StreamWriter(new FileStream(fileName_SerialNumbers, FileMode.Create), Encoding.UTF8);
            SerialNumbersFile.WriteLine("EntryNumber \t TicketID");

            for (int i = 0; i < TotalNumEntries; i++)
            {
                //Adds randomnumber to last number
                RandomTicketID += random.Next(11, 22);
                //writes the ID to stream
                SerialNumbersFile.WriteLine(i + "\t\t" + RandomTicketID);
                //Create New object with input
                SubmissionDATA p = new SubmissionDATA(i, RandomTicketID); 
                //saves the object file
                p.SaveToFile();
            }
            //Close stream
            SerialNumbersFile.Close();
            Console.Out.WriteLine("There have been generated new tickets .");
        }
    }
}
