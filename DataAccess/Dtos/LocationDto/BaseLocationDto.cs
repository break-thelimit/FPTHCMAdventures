﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.LocationDto
{
    public abstract class BaseLocationDto
    {
        private double x;
        private double y;
        private double z;
        private string locationName;
        private string status;

        [Required]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        [Required]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        [Required]
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        [Required]
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

        [Required]
        [RegularExpression("^(INACTIVE|ACTIVE)$", ErrorMessage = "Status must be 'INACTIVE' or 'ACTIVE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
