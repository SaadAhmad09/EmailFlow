using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailAndFileProject.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string CV { get; set; }
        public string FullName
        {
            get
            {
                return this.Title + " " + this.FirstName + " " + this.LastName;
            }
        }
    }
}
