﻿using SdkApiB2bLibrary.model.response;
using SdkApiB2bLibrary.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiLibraries
{
    class SegurancaApi
    {
        private RequestUtil<String, ChaveDTO> requestProduto = new RequestUtil<String, ChaveDTO>();



        public async Task<ChaveDTO> GetChave()
        { 
            return await requestProduto.DoGetAsync("/seguranca/chaves", null);
        }
    }
}