﻿namespace TravelPics.Locations.Abstraction.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
