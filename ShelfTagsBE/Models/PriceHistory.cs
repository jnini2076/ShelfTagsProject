using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShelfTagsBE.Models;
namespace ShelfTagsBE.Models;

public class PriceHistory
{
    
    public  int Id {get;set;}


    [ForeignKey("Product")]
    public  int ProductId {get;set;}
    public Product Product {get;set;}

    [Required]
    public required double OldPrice {get;set;}

    [Required]
    public required double NewPrice {get;set;}

  
    [Required]
    public required DateTime ChangedAT {get;set;}

    

    
     

}
