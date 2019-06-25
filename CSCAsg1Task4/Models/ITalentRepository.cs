using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCAsg1Task4.Models
{
    interface ITalentRepository
    {
        IEnumerable<Talent> GetAll();
        Talent Get(int id);
        Talent Add(Talent item);
        void Remove(int id);
        bool Update(Talent item);
    }
}
