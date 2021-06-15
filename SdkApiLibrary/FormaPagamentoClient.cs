﻿using SdkApiB2b.model.response;
using SdkApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibrary
{
    class FormaPagamentoClient
    {
        private RequestUtil<String, OpcoesParcelamentoDTO> requestCampanha = new RequestUtil<String, OpcoesParcelamentoDTO>();

        public async Task<OpcoesParcelamentoDTO> GetOpcoesParcelamentoAsync(String idFormaPagamento, String idCampanha, String cnpj, String valorParcelar)
        {
            Dictionary<String, String> queryParams = new Dictionary<string, string>();
            queryParams.Add("idCampanha", idCampanha);
            queryParams.Add("cnpj", cnpj);
            queryParams.Add("valorParcelar", valorParcelar);
            OpcoesParcelamentoDTO response = await requestCampanha.DoGetAsync("/formas-pagamento/" + idFormaPagamento + "/opcoes-parcelamento", queryParams);
            return response;
        }

    }
}
