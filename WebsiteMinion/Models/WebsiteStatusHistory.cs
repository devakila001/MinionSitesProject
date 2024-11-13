using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteMinion.Models;

public class WebsiteStatusHistory : BaseEntity
{
    [Required]
    [Column(name: "request_sent_at")]
    public required DateTime RequestSentAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(name: "website_info_id")]
    public required long WebsiteInfoId { get; set; }

    [Required]
    [Column(name:"http_status_code")]
    public int HttpStatusCode { get; set; }

    [Required]
    [Column(name: "status_message")]
    public string? StatusMessage { get; set; }

    [Required]
    [Column(name: "is_up")]
    public bool IsUp { get; set; }
}
