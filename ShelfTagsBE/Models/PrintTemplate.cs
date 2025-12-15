using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfTagsBE.Models;

public class PrintTemplate
{
    [Required]
    public required int Id {get;set;}

    [Required]
    public required string Name {get;set;}

    [Required]
    public required string LayoutHTML {get;set;}

    [Required]
    public required int WidthMm {get;set;}

    [Required]
    public required int HeightMm {get;set;}

}
