using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    [Serializable]
    public class Entity<E>
    {
        public E Id { get; set; }

        public Entity(E id)
        {
            this.Id = id;
        }


    }
}
