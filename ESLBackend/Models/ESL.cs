using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ESLBackend.Models
{
    public class ESL
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StoreNumber { get; set; }
        public int TagId { get; set; }
        // You can add more properties as needed
    }



    public class BindESL
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonPropertyName("shopCode")]
        public string ShopCode { get; set; }

        [JsonPropertyName("binds")]
        public Bind Binds { get; set; }



        public static BindESL2 BindESLMapper(BindESL bindESL)
        {



            return new BindESL2
            {

                ShopCode = "0003",
                Binds = new List<Bind> { bindESL.Binds }


            };


        }


    }










    public class BindESL2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonPropertyName("shopCode")]
        public string ShopCode { get; set; }

        [JsonPropertyName("binds")]
        public List<Bind> Binds { get; set; }






    }











    public class Bind
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [JsonPropertyName("tagID")]
        public string TagID { get; set; }

        

        [JsonPropertyName("goodsCode")]
        public string GoodsCode { get; set; }
    }


   



}
