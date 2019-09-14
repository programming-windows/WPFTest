using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity
{
    public class TblFgwj : BasicEntity
    {
        public string idKey   { get; set; }
        public string fileNo { get; set; }
        public string subject { get; set; }
        public string publishDate { get; set; }
        public string implementDate { get; set; }
        public string publishOrg { get; set; }
        public string writeName { get; set; }
        public string writeCode { get; set; }
        public string writeDate { get; set; }
        public string writeDept { get; set; }
        public string flag { get; set; }

        public string fileName { get; set; }
        public string fileType { get; set; }
    }
}
