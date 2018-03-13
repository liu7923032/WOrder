using System;
using System.Collections.Generic;
using System.Text;

namespace WOrder.Extend
{
    public class PageRequestDto
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }

        public int SkipCount { get; set; }

        public PageRequestDto()
        {
            this.SkipCount = (Page - 1) * this.Limit;
        }

    }


}
