using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfTagsBE.Dto;

public class ProductDTO
{
    
    public  int Id {get;set;}
    
    [Required]
     public  required string SKU {get;set;}

    [Required]
    public required string Name {get;set;}

    [Required]
    public required string Brand {get;set;}

    [Required]
    public required string Category {get;set;}

    [Required]
    public required double CurrentPrice {get;set;}

    [Required]
    public required BarcodeType DefaultBarcodeType {get;set;}

    [Required]
    public required DateTime CreatedAt {get;set;} 
    [Required]
    public required DateTime UpdatedAt {get;set;} 


}
    public enum BarcodeType
{
    Code128,
    QRCode,
    UPC
}
