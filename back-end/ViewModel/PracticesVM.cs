using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class PracticesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypesAiMlApplications { get; set; }
        public string OrganizationContext { get; set; }
        public string SeKnowLedge { get; set; }
        public string ContribuitionTypes { get; set; }
        public string Link { get; set; }
        public string ReferencesDescribing { get; set; }
        public int IdUser { get; set; }

        public List<PracticesAnexoVM> AnexosPractice { get; set; }
        public List<PracticesAuthorsVM> AuthorsPractice { get; set; }
    }
}
