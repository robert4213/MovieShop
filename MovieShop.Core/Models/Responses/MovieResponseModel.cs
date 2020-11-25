﻿using System;

namespace MovieShop.Core.Models.Responses
{
    public class MovieResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}