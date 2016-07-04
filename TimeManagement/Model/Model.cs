using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagement.Model
{
    public abstract class Model
    {
        private bool newItem;
        abstract public void save();
        abstract public void delete();
        
        public bool isNew()
        {
            return newItem;
        }

    }
}
