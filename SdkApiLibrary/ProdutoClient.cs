using SdkApiB2b.model.response;
using SdkApiLibrary.model.response;
using SdkApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibrary
{
    class ProdutoClient
    {
        private RequestUtil<String, ProdutoDTO> requestProduto = new RequestUtil<String, ProdutoDTO>();
        private RequestUtil<String, ProdutosDTO> requestProdutos = new RequestUtil<String, ProdutosDTO>();

        public async Task<ProdutoDTO> GetDadosProduto(String idLogista, String idSKu)
        {
            ProdutoDTO dto = await requestProduto.DoGetAsync("/lojistas/" + idLogista + "/produtos/" + idSKu, null);
            return dto;
        }

        public async Task<ProdutosDTO> GetListaProdutos(String idLogista, List<String> idSKu)
        {
            var queryParams = ArrayQueryParamBuilder(idSKu, "idSKu");
            ProdutosDTO dto = await requestProdutos.DoGetAsync("/lojistas/" + idLogista + "/produtos", queryParams);
            return dto;
        }

        public async Task<ProdutoDTO> GetDadosProdutoCampanha(String idCampanha, String idSKu, String cnpj, String idLojista)
        {
            Dictionary<String, String> queryParams = new Dictionary<string, string>();
            queryParams.Add("idLojista", idLojista);
            queryParams.Add("cnpj", cnpj);
            ProdutoDTO dto = await requestProduto.DoGetAsync("/campanhas/" + idCampanha + "/produtos/" + idSKu, null);
            return null;
        }


        private Dictionary<String, String> ArrayQueryParamBuilder(List<String> idSKu, String paramName)
        {
            Dictionary<String, String> queryParams = new Dictionary<string, string>();
            foreach (var param in idSKu)
            {
                if(param != null)
                {
                    queryParams.Add(paramName, param);
                }
            }
            return queryParams;
        }


    }
}
