using AlpineSkiHouse.Data;
using AlpineSkiHouse.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Services
{
    public class PassValidityChecker : IPassValidityChecker
    {
        PassContext _passContext;
        PassTypeContext _passTypeContext;
        IDateService _dateService;
        public PassValidityChecker(IDateService dateService,
            PassContext passContext,
            PassTypeContext passTypeContext)
        {
            _passTypeContext = passTypeContext;
            _passContext = passContext;
            _dateService = dateService;
        }

        public bool IsValid(int passId)
        {
            var pass = _passContext.Passes.FirstOrDefault(x => x.Id == passId);
            if (pass == null)
                return false;
            var passType = _passTypeContext.PassTypes.FirstOrDefault(x => x.Id == pass.PassTypeId);
            if (passType == null)
                return false;
            if (IsOutsideOfDateRange(passType))
            {
                return false;
            }
            if (pass.Activations.Any())
            {
                if (pass.Activations.Count() > passType.MaxActivations)
                {
                    return false;
                }

                if (HasBeenPreviouslyActivatedToday(pass))
                {
                    return true;
                }
                if (pass.Activations.Count() == passType.MaxActivations)
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasBeenPreviouslyActivatedToday(AlpineSkiHouse.Models.Pass pass)
        {
            return pass.Activations.OrderByDescending(x => x.Scan.DateTime).FirstOrDefault().Scan.DateTime > _dateService.Today();
        }

        private bool IsOutsideOfDateRange(AlpineSkiHouse.Models.PassType passType)
        {
            return passType.ValidFrom > _dateService.Now() || passType.ValidTo < _dateService.Now();
        }
    }
}
