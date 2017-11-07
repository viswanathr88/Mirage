using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mirage.ViewModel
{
    public class VoidType
    {
        public static VoidType Empty
        {
            get
            {
                return new VoidType();
            }
        }
    }
}
