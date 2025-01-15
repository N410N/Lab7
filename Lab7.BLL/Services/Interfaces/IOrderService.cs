using Lab7.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        public void FinishReport(OrderDTO report);
    }
}
