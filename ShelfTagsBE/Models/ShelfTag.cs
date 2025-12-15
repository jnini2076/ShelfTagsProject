using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShelfTagsBE.Models;

public class ShelfTag
{
    [Required]
    public required int Id {get;set;}
    [ForeignKey("Product")]
    public required int ProductId {get;set;}
    public required Product Product {get;set;}
    [ForeignKey("PrintTemplate")]
    public required int PrintTemplateId {get;set;}
    public required PrintTemplate PrintTemplate {get;set;}

    [Required]
    public required string BarCodeValue {get;set;}

    [Required]
    public required DateTime PrintedAt {get;set;}


    [Required]
    public required string PrintedBy {get;set;}
    [Required]
    public required int PrintCount {get;set;}
    [Required]
    public required StatusType Status {get;set;}

}
    public enum StatusType
{
    Started,
    InProgress,
    Completed,
    Failed
}
