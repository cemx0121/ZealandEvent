﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZealandEventLib.Models
{
    public enum VIP { NejTak, JaTak, JaTakPlusN, JaTakPlusV, JaTakPlusNOgC, JaTakPlusVOgC }
    public partial class Booking
    {
        [Key]
        [Column("Booking_ID")]
        public int BookingId { get; set; }
        [Column("Arrangement_ID")]
        public int ArrangementId { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(8)]
        public string Phone { get; set; }
        [Column("VIP")]
        public VIP Vip { get; set; }
        public bool Parking { get; set; }
        [Column("User_ID")]
        public int UserId { get; set; }

        [ForeignKey(nameof(ArrangementId))]
        [InverseProperty("Bookings")]
        public virtual Arrangement Arrangement { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bookings")]
        public virtual User User { get; set; }
    }
}