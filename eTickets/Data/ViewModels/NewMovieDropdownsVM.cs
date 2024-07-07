﻿using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
        }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }

        public List<Actor> Actors { get; set; }
    }
}
