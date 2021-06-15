using ConsoleApp1.Models.Response;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibrary
{
    class CampanhaClient
    {
        private RequestUtil<String, CampanhaDTO> requestCampanha = new RequestUtil<String, CampanhaDTO>();
        private RequestUtil<String, FormasPagamentoDTO> requestFormasPagamento = new RequestUtil<String, FormasPagamentoDTO>();

        public async Task<CampanhaDTO> GetCampanhasAsync(String dtInicio, String dtFim)
        {
            Dictionary<String, String> queryParams = new Dictionary<string, string>();
            queryParams.Add("dataInicio", dtInicio);
            queryParams.Add("dataFim", dtFim);
            CampanhaDTO response = await requestCampanha.DoGetAsync("/campanhas", queryParams);
            return response;
        }


        public async Task<FormasPagamentoDTO> GetOpcoesPagamentoAsync(String idCampanha, String cnpj)
        {
            Dictionary<String, String> queryParams = new Dictionary<string, string>();
            queryParams.Add("cnpj", cnpj);
            FormasPagamentoDTO response = await requestFormasPagamento.DoGetAsync("/campanhas/" + idCampanha + "/formas-pagamento/opcoes-parcelamento", queryParams);
            return response;
        }








    }
}
