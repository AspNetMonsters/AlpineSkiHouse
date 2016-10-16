using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace AlpineSkiHouse.Conventions
{
    /// <summary>
    /// A custom convention that automatically adds the ValidateAntiForgeryToken filter
    /// to action methods with the HttpPost attribute
    /// </summary>
    public class AutoValidateAntiForgeryTokenModelConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (IsConventionApplicable(action))
            {
                action.Filters.Add(new ValidateAntiForgeryTokenAttribute());
            }
        }

        public bool IsConventionApplicable(ActionModel action)
        {
            if ( action.Attributes.Any(f => f.GetType() == typeof(HttpPostAttribute)) &&
                !action.Attributes.Any(f => f.GetType() == typeof(ValidateAntiForgeryTokenAttribute))){
                return true;
            }
            return false;
        }
    }
}
