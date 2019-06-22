using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class SlideDao
    {
        DatabaseSellEntities db = null;
        public SlideDao()
        {
            db = new DatabaseSellEntities();
        }
    }
}
