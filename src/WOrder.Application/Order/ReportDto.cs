using System;
using System.Collections.Generic;
using System.Text;
using WOrder.Domain.Entities;

namespace WOrder.Order
{
    public class ReportDto
    {
    }


    public class NameValueDto
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class GetDateInput
    {
        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }

        public TStatus? TStatus { get; set; }
    }
}
