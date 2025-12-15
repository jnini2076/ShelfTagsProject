using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfTagsBE.Models;

public class AuditLog
{
    public int Id { get; set; }
    
    [Required]
    public int EntityId { get; set; }  // ID of the entity being audited (ProductId, ShelfTagId, etc.)
    
    [Required]
    [MaxLength(50)]
    public required string EntityType { get; set; }  // "Product", "ShelfTag", "PriceHistory", etc.
    
    [Required]
    [MaxLength(50)]
    public required string Action { get; set; }  // "PriceChanged", "TagPrinted", "Created", "Updated", "Deleted"
    
    public int? UserId { get; set; }  // Who performed the action (nullable if system-generated)
    
    public string? Metadata { get; set; }  // JSON string with additional details (old/new values, etc.)
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
