using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Services
{
    public class CardValidator : ICardValidator
    {
        public bool IsValid(int passId, int locationId)//assume right now
        {
            //TODO: you know, maybe implement this
            if (passId % 2 == 1)
                return true;
            return false;
        }
    }
}
