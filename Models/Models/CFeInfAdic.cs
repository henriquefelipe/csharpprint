using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library.Models
{
    public class CFeInfAdic
    {
        public CFeInfAdic()
        {
            ObsFisco = new Collection<CFeObsFisco>();
        }

        public Collection<CFeObsFisco> ObsFisco { get; set; }
    }
}
