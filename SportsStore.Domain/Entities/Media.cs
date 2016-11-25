using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SportsStore.Domain.Entities
{
    public class Media
    {
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public virtual Product Product { get; set; }
    
    }
}
