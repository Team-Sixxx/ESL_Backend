using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESLBackend.Models
{
    public class Templates
    {
        [Key]
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string ShopCode { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public string TemplateType { get; set; }
        public List<string> Upc { get; set; }
        public List<string> Items { get; set; }
        public int Version { get; set; }
        public string HashCode { get; set; }
    }
}
