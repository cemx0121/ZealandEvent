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
        [Required(ErrorMessage = "Fornavn skal udfyldes")]
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "Et fornavn skal bestå af mindst 2 bogstaver")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Fornavn kan ikke indeholde tal")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Efternavn skal udfyldes")]
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "Et efternavn skal bestå af mindst 2 bogstaver")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Efternavn kan ikke indeholde tal")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Telefon nummer skal udfyldes")]
        [StringLength(8)]
        [MinLength(8, ErrorMessage = "Et telefon nummer bestå af 8 tal")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Et telefon nummer kan ikke indeholde bogstaver")]
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