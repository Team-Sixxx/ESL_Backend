using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ESLBackend.Models
{
    public class Templates
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("template")]
        public string? TemplateContent { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public string? LastUpdatedBy { get; set; }

        [JsonPropertyName("lastUpdatedTime")]
        public DateTime? LastUpdatedTime { get; set; }

        [JsonPropertyName("shopCode")]
        public string? ShopCode { get; set; }

        [JsonPropertyName("goodsCode")]
        public string? GoodsCode { get; set; }

        [JsonPropertyName("goodsName")]
        public string? GoodsName { get; set; }

        [JsonPropertyName("templateType")]
        public string? TemplateType { get; set; }

        [JsonPropertyName("upcs")]
        public List<Upc> Upcs { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("hashCode")]
        public string? HashCode { get; set; }

        internal static PostTemplates MappedTemplate(Templates template)
        {
            throw new NotImplementedException();
        }

        public class Item
        {
            [Key]
            public int Id { get; set; }
            public string? ShopCode { get; set; }
            public string? GoodsCode { get; set; }
            public string GoodsName { get; set; }
            public string Upc1 { get; set; }
            public string Upc2 { get; set; }
            public string Upc3 { get; set; }
            public string Price1 { get; set; }
            public string Price2 { get; set; }
            public string Price3 { get; set; }
            public string Origin { get; set; }
            public string Spec { get; set; }
            public string Unit { get; set; }
            public string Raid { get; set; }
            public string SalTimeStart { get; set; }
            public string SalTimeEnd { get; set; }
            public string PriceClerk { get; set; }
            public string TemplatesId { get; set; }
            public Templates Templates { get; set; }
        }

        public class Upc
        {
            [Key]
            public int Id { get; set; }
            public string GoodsCode { get; set; }
            public string TemplatesId { get; set; }
            public Templates Templates { get; set; }
        }
    }

    public class PostTemplates
    {
        [Key]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("template")]
        public string? TemplateContent { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public string? LastUpdatedBy { get; set; }

        [JsonPropertyName("lastUpdatedTime")]
        public DateTime? LastUpdatedTime { get; set; }

        [JsonPropertyName("shopCode")]
        public string? ShopCode { get; set; }

        [JsonPropertyName("goodsCode")]
        public string? GoodsCode { get; set; }

        [JsonPropertyName("goodsName")]
        public string? GoodsName { get; set; }

        [JsonPropertyName("templateType")]
        public string? TemplateType { get; set; }

        [JsonPropertyName("upc")]
        public List<string> Upc { get; set; }

        [JsonPropertyName("items")]
        public List<string>? Items { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("hashCode")]
        public string? HashCode { get; set; }
    }
}
