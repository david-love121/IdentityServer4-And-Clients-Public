using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace IDSEmpty.sakila
{
      
    public partial class Idstable
    {
        [Key]
        public string Id { get; set; }
        
        public string Pword { get; set; }
       
        public int Balance { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }
        
        public string Salt { get; set; }
    }
}
