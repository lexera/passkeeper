using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPass
{
    class ValidationClass
    {
        public bool CheckIfNotEmpty(params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(values[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
