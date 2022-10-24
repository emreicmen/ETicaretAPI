using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        //Bir order'ın bir tane Customer'ı olabilir.Bu yüzden ICollection tanımlı özelliği Orders'a yazmayacağız.Sadece Customer tanımlı özellik tanımlayacağız
        public ICollection<Order> Orders { get; set; }
    }
}
