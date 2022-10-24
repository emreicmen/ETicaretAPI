using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        //Order ile product arasında n to n ilişkisi olduğu için bu ifadeyi yazdık
        //Aynı ifade n to n iliş içerisinde olduğu Order'ın içerisinde de vardır
        public ICollection<Order> Orders{ get; set; }
    }
}
