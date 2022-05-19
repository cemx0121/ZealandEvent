﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZealandEventLib.Models
{
    public enum Location { Spiseteltet, Musikteltet, Tribunen}
    public partial class Event
    {
        [Key]
        [Column("Event_ID")]
        public int EventId { get; set; }
        [Column("Arrangement_ID")]
        public int ArrangementId { get; set; }
        [Required(ErrorMessage = "Begivenhed titel skal udfyldes")]
        [StringLength(30)]
        [MinLength(3, ErrorMessage = "Begivenhed titel skal bestå af mindst 3 bogstaver")]
        public string Title { get; set; }
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Start tid skal udfyldes")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Slut tid skal udfyldes")]
        public DateTime End { get; set; }
        public Location Location { get; set; }

        [ForeignKey(nameof(ArrangementId))]
        [InverseProperty("Events")]
        public virtual Arrangement Arrangement { get; set; }
    }
}