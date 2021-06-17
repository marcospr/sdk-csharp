using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2bLibrary.api.model.response
{
    public class DadosPagamentoComplementar
    {
        public List<Pagamento> Pagamentos { get; set; }
        public int ValorTotalComplementar { get; set; }
        public int ValorTotalComplementarComJuros { get; set; }
    }
}
