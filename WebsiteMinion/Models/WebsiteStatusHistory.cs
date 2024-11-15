using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebsiteMinion.Common.Constants;

namespace WebsiteMinion.Models;

[Table(name: Tables.WebsiteStatusHistory, Schema = Schemas.Main)]
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
