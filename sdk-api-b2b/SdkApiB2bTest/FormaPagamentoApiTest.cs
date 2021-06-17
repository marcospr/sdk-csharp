using Microsoft.VisualStudio.TestTools.UnitTesting;
using SdkApiLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2bTest
{
    [TestClass]
    public class FormaPagamentoApiTest
    {

        FormaPagamentoApi api = new FormaPagamentoApi();

        [TestMethod]
        public async Task TestGetOpcoesParcelamentoSucess()
        {
            var dto = await api.GetOpcoesParcelamentoAsync("1", "5940", "57.822.975/0001-12", "1000");
            Assert.IsNotNull(dto);
            Assert.AreEqual(1000.0D, dto.Data[0].ValorParcela);
        }

        //Verificar o erro que esta dando para validar o teste
        [TestMethod]
        public async Task TestGetOpcoesParcelamentoFail()
        {
            var dto = await api.GetOpcoesParcelamentoAsync("8", "5940", "57.822.975/0001-12", "1000");
            Assert.IsNotNull(dto);
        //    Assert.AreEqual(1000.0D, dto.Data);
        }


    }
}
