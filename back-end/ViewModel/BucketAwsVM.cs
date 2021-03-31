using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class BucketAwsVM
    {
        public string NomeBucket { get; set; }
        public string Key { get; set; } //nome artefato
        public string LocalPath { get; set; } = @"C:\Users\SUFRAMA\Desktop\APKS\";
    }
}
