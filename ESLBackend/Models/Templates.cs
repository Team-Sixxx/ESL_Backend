using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using static ESLBackend.Models.Templates;


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
        public string? Template { get; set; }

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
        public List<Upc>? Upcs { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("hashCode")]
        public string? HashCode { get; set; }

        public class Item
        {
            [Key]

            public string? ShopCode { get; set; }
            [Key]
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
        }




        public class Upc
        {
            [Key]
            public string GoodsCode { get; set; }



        }


        public static PostTemplates MappedTemplate(Models.Templates t)
        {
            return new PostTemplates
            {
                Id = GenerateRandomId(),
                CreatedBy = "System",
                CreatedTime = DateTime.Now,
                Template = t.Template,
                LastUpdatedBy = "System",
                LastUpdatedTime = t.LastUpdatedTime,
                ShopCode = "0003",
                GoodsCode = t.GoodsCode,
                GoodsName = t.GoodsName,
                TemplateType = t.TemplateType,
                //Upc = new List<string> { , t.GoodsCode },
                Upc = ConvertUpcToList(t.Upcs),
                Items = ConvertItemsToList(t.Items),
                Version = t.Version + 1,
                HashCode = Guid.NewGuid().ToString("N").ToUpper()
            };
        }


        private static List<string> ConvertItemsToList(List<Item> items)
        {
            List<string> itemList = new List<string>();

            foreach (var item in items)
            {
                itemList.AddRange(new string[] {
            item.ShopCode, item.GoodsCode, item.GoodsName, item.Upc1, item.Upc2,
            item.Upc3, item.Price1, item.Price2, item.Price3, item.Origin,
            item.Spec, item.Unit, item.Raid, item.SalTimeStart, item.SalTimeEnd, item.PriceClerk
        });
            }

            return itemList;
        }




        private static List<string> ConvertUpcToList(List<Upc> upcs)
        {
            List<string> upcList = new List<string>();

            foreach (var item in upcs)
            {
                upcList.Add(item.GoodsCode);
            }

            return upcList;
        }







        private static string GenerateRandomId()
        {
            Random random = new Random();
            int[] parts = new int[3];
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = random.Next(0, 10000);
            }
            return string.Join("", parts.Select(p => p.ToString("D4")));
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
        public string? Template { get; set; }

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