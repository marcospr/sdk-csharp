using SdkApiLibraries.model.response;
using SdkApiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibraries
{
    class SegurancaClient
    {
        private RequestUtil<String, ChaveDTO> requestProduto = new RequestUtil<String, ChaveDTO>();



        public async Task<ChaveDTO> GetChave()
        { 
            return await requestProduto.DoGetAsync("/seguranca/chaves", null);
        }
    }
}
