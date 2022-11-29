using System;

namespace Products
{
    public class Product 
    {
        public int ID  {get;set;}
        public int ProductId {get;set;}
        public string Name {get;set;}
        public string Title  {get;set;  }
        public string ImageUrl  {get;set; }
        public string Brand  {get;set; }
        public int MinBestBeforeDays {get;set; }
    }
}