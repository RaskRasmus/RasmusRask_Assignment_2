using System;

namespace Assignment_2_Rask.Models
{
    class Trying_To_Open_File_That_Does_Not_Exist : ApplicationException
    {
        public Trying_To_Open_File_That_Does_Not_Exist(String problem) : base(problem)
        {
        }
    }
}
