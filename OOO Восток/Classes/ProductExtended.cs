using OOO_Восток.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Восток.Classes
{
    public class ProductExtended
    {
        public Products Product { get; set; }

        public string ProductPathPhoto
        {
            get
            {
                string temp = "";
                if (Product.ProductPhoto != null)
                {
                    temp = Directory.GetCurrentDirectory() + "/Images/" + Product.ProductPhoto;
                }
                else
                {
                    temp = "C:\\Users\\Sonia\\Desktop\\OOO Восток\\OOO Восток\\Resurses\\no.png";
                }
                return temp;
            }
        }

        public double ProductCostWithDiscont
        {
            get
            {
                if (Product.Sale != null)
                {
                    double discount = Product.Price * ((double)Product.Sale / 100);
                    return Product.Price - discount;
                }
                else
                { 
                 return Product.Price;
                }

            }
        }




    }
}
