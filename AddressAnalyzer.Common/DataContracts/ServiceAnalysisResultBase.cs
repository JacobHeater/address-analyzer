using System;
using System.Collections.Generic;

namespace AddressAnalyzer.Common.DataContracts
{
    public class ServiceAnalysisResultBase
    {
        public bool IsSuccessful { get; set; }
        public string ResultText { get; set; }
        public List<string> FailureReasons { get; } = new List<string>();
    }
}
