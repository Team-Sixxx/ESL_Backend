using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ESLBackend.Models
{
    public class ESL
    {
        public int Id { get; set; }
        public int StoreNumber { get; set; }
        public int TagId { get; set; }
        // You can add more properties as needed
    }



    public class BindESL
    {
        [JsonPropertyName("shopCode")]
        public string ShopCode { get; set; }

        [JsonPropertyName("binds")]
        public List<Bind> Binds { get; set; }



        public static BindESL BindESLMapper(BindESL bindESL)
        {



            return new BindESL
            {

                ShopCode = "0003",
                Binds = bindESL.Binds,


            };


        }


    }



    public class Bind
    {
        [JsonPropertyName("tagID")]
        public string TagID { get; set; }

        

        [JsonPropertyName("goodsCode")]
        public string GoodsCode { get; set; }
    }


   



}
