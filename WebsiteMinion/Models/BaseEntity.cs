using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteMinion.Models;

public class BaseEntity
{
    [Key]
    [Required]
    [Column(name: "id", Order = 0)]
    public long Id { get; set; }
    [Required]
    [Column(name: "created_by")]
    public long CreatedBy { get; set; } = 0;
    [Required]
    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column(name: "modified_by")]
    public long? ModifiedBy { get; set; }

    [Column(name:"modified_at")]
    public DateTime? ModifiedAt { get; set; }
}
