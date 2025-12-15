using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfTagsBE.Dto;

public class UpdateProductRequest
{


    [Required]
    [Range(0.01, 10000)]
    public decimal NewPrice { get; set; }

   
}
