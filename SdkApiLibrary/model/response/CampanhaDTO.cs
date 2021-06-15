using Newtonsoft.Json;
using SdkApiB2b.model.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2b.model.Response
{
    class CampanhaDTO
    {

        [JsonProperty("data")]
        public List<Campanha> Data { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
