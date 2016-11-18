using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            CartLine cartline = lineCollection.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();
            if (cartline == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
                cartline.Quantity += quantity;
        }
        public void DecrementQuantity(Product product)
        {
            CartLine cartline = lineCollection.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();
            if(cartline.Quantity > 1)
            {
                cartline.Quantity--;
            }
        }
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Quantity * x.Product.Price);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public class CartLine
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }

    }
}
