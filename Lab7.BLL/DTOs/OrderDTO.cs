﻿using Lab7.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.BLL.DTOs
{
    public class OrderDTO
    {

        public int Id { get; set; }
        public int AdminId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? OrderDescription { get; set; }
        public string? Status { get; set; }
        public string? CustomerFeedback { get; set; }

    }
}
