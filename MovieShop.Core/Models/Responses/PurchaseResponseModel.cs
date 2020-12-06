﻿using System;
using System.Collections.Generic;

namespace MovieShop.Core.Models.Responses
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public List<PurchasedMovieResponseModel> PurchasedMovies { get; set; }
        public class PurchasedMovieResponseModel:MovieResponseModel
        {
            public DateTime PurchaseDateTime { get; set; }
        }
    }
}