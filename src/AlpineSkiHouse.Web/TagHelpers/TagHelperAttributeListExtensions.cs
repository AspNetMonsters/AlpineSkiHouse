using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AlpineSkiHouse.TagHelpers
{
    public static class TagHelperAttributeListExtensions
    {
        public static void MergeClassAttributeValue(this TagHelperAttributeList attributes, string newClassValue)
        {
            string classValue = newClassValue;
            if (attributes.ContainsName("class"))
            {
                classValue = $"{attributes["class"].Value} {classValue}";
            }
            attributes.SetAttribute("class", classValue);
        }
    }
}
