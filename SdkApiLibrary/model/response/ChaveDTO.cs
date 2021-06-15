﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibraries.model.response
{
    class ChaveDTO
    {

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
