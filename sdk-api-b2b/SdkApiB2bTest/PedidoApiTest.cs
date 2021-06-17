using Microsoft.VisualStudio.TestTools.UnitTesting;
using SdkApiB2bLibrary.api;
using SdkApiB2bLibrary.api.client;
using System;

namespace SdkApiB2bTest
{
    /// <summary>
    /// Classe de testes para as URI's dos Pedidos do B2B.</br>
    /// É importante que os metodos sejam executados na ordem estabelecida, pois</br>
    /// alguns metodos de testes possuem dependencia dos resultados dos anteriores.
    /// <summary>

    [TestClass]
    public class PedidoApiTest
    {
        /** Instancia do client API. */
        private static PedidoApi pedidoApi;

        /** Token. */
        private static string AUTHORIZATION_TOKEN = "H9xO4+R8GUy+18nUCgPOlg==";

        /** Host do servico do Extra. */
        private static string HOST_EXTRA = "http://api-integracao-extra.hlg-b2b.net";

        /** Host do servico das Casas Bahia. */
        // private static final String HOST_CASAS_BAHIA = "";

        /** Host do servico do Ponto Frio. */
        // private static final String HOST_PONTO = "";

        /** CEP padrao dos testes */
        private static string CEP = "01525000";

        /** Id Lojista padrao dos testes. */
        private static int ID_LOJISTA = 15;

        /** CPF FICTICIO PARA TESTES */
        private static string CPF_DESTINATARIO = "435.375.660-50";

        /** CNPJ padrao dos testes. */
        private static string CNPJ = "57.822.975/0001-12";

        /** Id Campanha padrao dos testes. */
        private static int ID_CAMPANHA = 5940;
        /** Atributo do Id Sku para criacao do primeiro Pedido. */
        private static int ID_SKU_CRIACAO_PEDIDO = 8935731;

        /** Atributo do Id Sku para criacao do segundo Pedido com cartao de credito. */
        private static int ID_SKU_CRIACAO_PEDIDO_COM_CARTAO = 9342200;

        /** Tipo de Forma de pagamento cartão Visa. */
        //private static final int CARTAO_VISA = 2;

        /** Tipo de Forma de pagamento cartão Master. */
        private static int CARTAO_MASTER = 3;

        /** Numero de cartao de credito Master mascarado. */
        private static string NUMERO_CARTAO_MASTER_MASCARADO = "515590XXXXXX0001";

        /** Numero de cartao de credito Master ficticio. */
        private static string NUMERO_CARTAO_MASTER = "5155901222280001";

        /**
         * Atributo global utilizado para guardar o primeiro pedido criado para ser
         * utilizado nos demais testes.
         */
        private static DadosPedidoHelper pedidoHelper;

        /**
		 * Atributo global utilizado para guardar o segundo pedido criado com Cartao
		 * Credito para ser utilizado nos demais testes.
		 */
        private static DadosPedidoHelper pedidoHelperComCartao;

        /**
		 * Chave pública 2048 bits utilizada para criptografia dos dados do cartão.</br>
		 * Pode ser obtida pelo URI Rest abaixo.
		 * 
		 * http://api-integracao-casasbahia.hlg-b2b.net/swagger/ui/index#!/Seguranca/Seguranca_ObterChave
		 * 
		 */
        private static string CHAVE_PUBLICA = "MIIENTCCAx2gAwIBAgIJAJ5ApEGl2oaIMA0GCSqGSIb3DQEBBQUAMIGwMQswCQYDVQQGEwJCUjELMAkGA1UECAwCU1AxFDASBgNVBAcMC1NBTyBDQUVUQU5PMRMwEQYDVQQKDApWSUEgVkFSRUpPMSAwHgYDVQQLDBdTRUdVUkFOQ0EgREEgSU5GT1JNQUNBTzEOMAwGA1UEAwwFUFJPWFkxNzA1BgkqhkiG9w0BCQEWKHRpLnNlZ3VyYW5jYS5pbmZvcm1hY2FvQHZpYXZhcmVqby5jb20uYnIwHhcNMTgwODE2MTIzNjQ2WhcNMjEwODE1MTIzNjQ2WjCBsDELMAkGA1UEBhMCQlIxCzAJBgNVBAgMAlNQMRQwEgYDVQQHDAtTQU8gQ0FFVEFOTzETMBEGA1UECgwKVklBIFZBUkVKTzEgMB4GA1UECwwXU0VHVVJBTkNBIERBIElORk9STUFDQU8xDjAMBgNVBAMMBVBST1hZMTcwNQYJKoZIhvcNAQkBFih0aS5zZWd1cmFuY2EuaW5mb3JtYWNhb0B2aWF2YXJlam8uY29tLmJyMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqObNb7KAP09WsV9h76Dw3tj2qa3l97K+slfzLkOBvi0xjacuKCnvsMSGEBosvWY/qNmSLE1YaoyFt7ZaeOiALKh2AFckJRM+/zvQzqi6cPnW0cGsEE/9WO48Fgh894pKjHpukATFb9tBYGTBEW46AH2WiAR735KEnDfFAHG//pkLKriPWEZBr9tf4gdNvyJ/ybs5JrBRU1RKE9MM7qnMkCouKTPwY/lS/2Xb1IYkyZulCf3Uyl7zpB6hQUhprS1R5meRocpGgHJCFfiWD/uXa5nREuGuQxcImwzvf+enwT6CooRoM2rN6IQWSY+uQ64dhSt4FMajZFmHVpLfUIOjEwIDAQABo1AwTjAdBgNVHQ4EFgQUZ22K62aMm/lI5LfblgINPvz8ae8wHwYDVR0jBBgwFoAUZ22K62aMm/lI5LfblgINPvz8ae8wDAYDVR0TBAUwAwEB/zANBgkqhkiG9w0BAQUFAAOCAQEAj23IDXLPkQpFDbgAtgKuO9N66o61edbJ1+BMjdSsfO0vMVpmBDlKdinxlh509/qJm/WLYswKkKOi7VHojBSV5HyrO5YGCSJFvVGJqF4JUxy7GrWTHqgwcylmX5B5lNd5aMIxwG6AF4o2cp6IPe+Uwaroa8kLTrtM0eRgAInHbQA7MXbvOZY+pzE4s6jFbA1O321zVg4C4Y3C4e30yf9YJNK5XjUP26duvwGqQrZg49ZU3W/t6GYY1kQhSeBG0FPg2GOIHX03WPZpaJ7i1uCv6Ial07pxDxqcT8oCJalY9tW9sv7zBJRaJgTIf5oz5jElb9kWd2D6XwaGB5PJfD6CTQ==";

        /** Atributo auxiliar para os testes de criacao de pedido. */
        private static DadosCartaoHelper dadosCartaoHelper;



        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Console.WriteLine("Init");
        }

        [TestMethod]
        public void AtestPostCalcularCarrinhoParaCriacaoPedido()
        {
            Console.WriteLine("AtestPostCalcularCarrinhoParaCriacaoPedido");
        }

        [TestMethod]
        public void BtestPostCalcularCarrinhoParaCriacaoPedidoComCartao()
        {
            Console.WriteLine("BtestPostCalcularCarrinhoParaCriacaoPedidoComCartao");
        }


        [TestMethod]
        public void CtestPostCriarPedido()
        {
            Console.WriteLine("CtestPostCriarPedido");
        }

        [TestMethod]
        public void DtestPostCriarPedidoPagCartao()
        {
            Console.WriteLine("DtestPostCriarPedidoPagCartao");
        }

        [TestMethod]
        public void EtestPatchPedidosCancelamento()
        {
            Console.WriteLine("EtestPatchPedidosCancelamento");
        }

        [TestMethod]
        public void FtestPatchPedidosConfirmacao()
        {
            Console.WriteLine("FtestPatchPedidosConfirmacao");
        }

        [TestMethod]
        public void GtestGetDadosPedidoParceiro()
        {
            Console.WriteLine("GtestGetDadosPedidoParceiro");
        }

        [TestMethod]
        public void HtestGetNotaFiscalPedidoPdf()
        {
            Console.WriteLine("HtestGetNotaFiscalPedidoPdf");

        }
    }

    class DadosPedidoHelper
    {
        public int IdPedido { get; set; }
        public int IdPedidoParceiro { get; set; }
        public int IdSku { get; set; }
        public double ValorFrete { get; set; }
        public double PrecoVenda { get; set; }
    }

    /**
	 * Classe auxiliar para dados do cartao credito.
	 * 
	 * @author Marcos P. da Rocha
	 *
	 */
    class DadosCartaoHelper
    {

        public Encryptor Encryptor { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string CodigoVerificador { get; set; }
        public string AnoValidade { get; set; }
        public string MesValidade { get; set; }

        DadosCartaoHelper(Encryptor encryptor, String nome, String numero, String codigoVerificador,
               String anoValidade,
               String mesValidade)
        {
            this.Encryptor = encryptor;
            this.Nome = nome;
            this.Numero = numero;
            this.CodigoVerificador = codigoVerificador;
            this.AnoValidade = anoValidade;
            this.MesValidade = mesValidade;
        }

        public String GetEncryptedName()
        {
            return Encryptor.Encript(Nome);
        }

        public String getEncryptedNumber()
        {
            return Encryptor.Encript(Numero);
        }

        public String getEncryptedVerifyCode()
        {
            return Encryptor.Encript(CodigoVerificador);
        }

        public String getEncryptedValidateYear()
        {
            return Encryptor.Encript(AnoValidade);
        }

        public String getEncryptedValidateMonth()
        {
            return Encryptor.Encript(MesValidade);
        }
    }
}
