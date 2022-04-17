﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A6QP.Models
{
    public class ArtistAddFormViewModel
    {
        public ArtistAddFormViewModel()
        {
            BirthOrStartDate = DateTime.Now;
        }

        // For an individual, can be birth name, or a stage/performer name
        // For a duo/band/group/orchestra, will typically be a stage/performer name
        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        // For an individual, a birth name
        [Display(Name = "If applicable, artist's birth name")]
        [StringLength(100)]
        public string BirthName { get; set; }

        // For an individual, a birth date
        // For all others, can be the date the artist started working together
        [DataType(DataType.Date)]
        [Display(Name = "Birth date, or Start Date")]
        public DateTime BirthOrStartDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Display(Name = "URL to artist photo")]
        [Required, StringLength(512)]
        public string UrlArtist { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Display(Name = "Primary Genre")]
        public SelectList GenreList { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? GenreId { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }

    }
}