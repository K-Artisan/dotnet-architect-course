namespace Zhaoxi.EntityFrameworkCore31.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class JD_Commodity_003
    {
        public int Id { get; set; }

        public long? ProductId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public decimal? Price { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }

        [StringLength(1000)]
        public string ImageUrl { get; set; }
    }
}
