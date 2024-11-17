using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
  public class Student
  {
    
      public int StudentID { get; set; }
      public string FullName { get; set; }
      public DateTime DateOfBirth { get; set; }
      public string RollNumber { get; set; }
      public string Address { get; set; }
    }
  
}
