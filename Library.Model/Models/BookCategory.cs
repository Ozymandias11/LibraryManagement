﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCategory : BaseModel
    {

        public Guid BookId { get; set; }
        public Guid CategoryId { get; set; }

        public Book? Book { get; set; }
        public Category? Category { get; set; } 
    }
}
