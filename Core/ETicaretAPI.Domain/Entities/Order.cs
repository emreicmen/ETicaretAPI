using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        //Bu ifadeyi Customer ve Order arasında n to 1 ilişkisi olduğu için koyduk. Normalde EntityFramework kendisi koyuyor lakin yazdığımız bu ifadeyi bulup kendisi koyması için biz yazıyoruz
        //Biz kendimiz yönetmek istediğimiz için yazıyoruz
        public Guid CustomerId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        //order ve product arasındaki n to n ilişkisi (çoka çok ilişki) olduğu için bunu yapıyoruz

        //Bir order'ın birden fazla product'ı olabilir demek bu.
        //Ancak bunu yazdığımız için Product'a da aynısını yapmalıyız.Yapmazsak bire çok ilişki yerine geçer bu ifade
        public ICollection<Product> Products { get; set; }

        //Customer ve Order arasında n to 1 ilişkisi olduğu için burada Customer tanımlı özellik ekliyoruz
        public Customer Customer { get; set; }
    }
}
