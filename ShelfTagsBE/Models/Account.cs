using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfTagsBE.Models;

public class Account
{


            public int Id {get;set;}

    
            public required string Username {get;set;}

            public required string Password {get;set;}

            

}
