using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2b.model.Response
{
    class FormasPagamento
    {
        [JsonProperty("idFormaPagamento")]
        public int IdFormaPagamento { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

    }
}
