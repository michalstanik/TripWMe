using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWMe.Models.GeoEntities
{
    public class CountryModelWithAssesments : CountryModel
    {
        public double AreaLevelAssessment { get; set; }
    }
}
