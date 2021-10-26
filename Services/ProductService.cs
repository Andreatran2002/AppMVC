using System.Collections.Generic;
using App.Models;

namespace App.Services{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[] {
                new ProductModel(){Id = 1, Name ="Iphone X",Price = 1340},
                new ProductModel(){Id = 2, Name ="Iphone Y",Price = 1304},
                new ProductModel(){Id = 3, Name ="Iphone Z",Price = 1514},
                new ProductModel(){Id = 4, Name ="Iphone T",Price = 1004}, 
                });
        }
    }
}