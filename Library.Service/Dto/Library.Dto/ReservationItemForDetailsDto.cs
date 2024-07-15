﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public class ReservationItemForDetailsDto
    {
        public string? BookTitle { get; set; }
        public string? Edition {  get; set; }   
        public string? PublisherName { get; set; }
        public int Quantity { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}