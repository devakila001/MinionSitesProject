using System;   
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebsiteMinion.Common.Constants;

namespace WebsiteMinion.Models;
//[Table(name: "WbesiteInfo", Schema = "Main")]
[Table(name: Tables.WebsiteInfo, Schema = Schemas.Main)]


public class WebsiteInfo : BaseEntity
{
    [Required]
    [Column(name: "website_url")]
    public required string WebsiteUrl { get; set; }


    [Required]
    [Column(name: "registered_at")]
    public required DateTime RegisteredAt { get; set; } = DateTime.UtcNow;


    [Required]
    [Column(name: "monitoring_enabled")]
    public required bool MonitoringEnabled { get; set; } = true;


    [Required]
    [Column(name: "last_checked_at")]
    public DateTime? LastCheckedAt { get; set; }


    [Required]
    [Column(name: "monitoring_interval_seconds")]
    public int MonitoringIntervalSeconds { get; set; } = 180;

}
