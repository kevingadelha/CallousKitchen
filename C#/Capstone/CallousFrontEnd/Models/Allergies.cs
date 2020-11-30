using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallousFrontEnd.Models
{
    public static class Allergies
    {
        public static string[] GetAllergies()
        {
            string[] allergies = {
                "peanut", "tree nut", "dairy", "glutten", "shellfish", "fish", "egg", "soy" };
            return allergies;
        }

        public static string[] GetAllergiesLabels()
        {
            string[] allergies = {
                "Peanuts", "Tree Nuts", "Dairy", "Glutten", "Shellfish", "Fish", "Eggs", "Soy" };
            return allergies;
        }
    }
}
