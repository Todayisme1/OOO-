using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Восток.Classes
{

    public class ProductInOrder
    {
        //Подробные данные о товаре
        public ProductExtended ProductExtended { get; set; }
        //Количество этого товара в заказе
        public int CountProductInOrder { get; set; }
        //Конструктор c параметром - выбраный товар из каталога
        public ProductInOrder(ProductExtended productExtended)
        {
            //ProductInOrder productInOrder = new ProductInOrder(productExtended);
            ProductExtended = productExtended;
            CountProductInOrder = 1;
        }
        //Конструктор по умолчанию
        public ProductInOrder() { }

    }
}
