using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SdkApiB2bLibrary.model.request;
using SdkApiB2bLibrary.model.response;

namespace SdkApiB2bExemplo
{
    public class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {            
            RunAsync("GET").GetAwaiter().GetResult();
           // RunAsync("POST").GetAwaiter().GetResult();
           // RunAsync("PUT").GetAwaiter().GetResult();
           // RunAsync("PATCH").GetAwaiter().GetResult();
           // RunAsync("DELETE").GetAwaiter().GetResult();
        }

        static async Task<CalculoCarrinho> Post(PedidoCarrinho pedidoCarrinho)
        {
            CalculoCarrinho calculoCarrinho = null;

            string json = JsonConvert.SerializeObject(pedidoCarrinho,Formatting.Indented);
            
            Console.WriteLine($"body entrada: {json}");

           // HttpResponseMessage response = await client.PostAsJsonAsync(
            //    "/pedidos/carrinho", pedidoCarrinho);


            HttpResponseMessage response = await client.PostAsync("/pedidos/carrinho", new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                calculoCarrinho = await response.Content.ReadAsAsync<CalculoCarrinho>();
            }
            return calculoCarrinho;

            // return response.Headers.Location;
        }
        static async Task<ProdutoDTO> Get(string path)
        {
            ProdutoDTO product = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<ProdutoDTO>();
            }
            return product;
        }

        static async Task<OpcoesParcelamentoDTO> Get2(string path)
        {
            OpcoesParcelamentoDTO opcoesParcelamentoDTO = null;
        
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                opcoesParcelamentoDTO = await response.Content.ReadAsAsync<OpcoesParcelamentoDTO>();
            }
            return opcoesParcelamentoDTO;
        }

        static async Task<ProdutoDTO> Put(ProdutoDTO product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Data.Valor}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<ProdutoDTO>();
            return product;
        }

        static async Task<HttpStatusCode> Delete(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }



        static async Task RunAsync(string method)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://api-integracao-extra.hlg-b2b.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "H9xO4+R8GUy+18nUCgPOlg==");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            try
            {
                if (method == "POST")
                {
                    Produtos produto = new Produtos
                    {
                        Codigo = 8935731,
                        Quantidade = 1,
                        IdLojista = 15
                    };
                    List <Produtos>  produtosList = new List<Produtos> { produto };

                    PedidoCarrinho pedidoCarrinho = new PedidoCarrinho
                    {
                        IdCampanha = 5940,
                        Cnpj = "57.822.975/0001-12",
                        Cep = "01525000",
                        Produtos = produtosList
                    };
              
                    CalculoCarrinho calculoCarrinho = await Post(pedidoCarrinho);
                    Console.WriteLine($"retorno Valor frete: {calculoCarrinho.Data.ValorFrete}");
                    //Console.WriteLine($"Created at {url}");
                }

                if (method == "GET")
                {
                    // UriBuilder builder = new UriBuilder("http://localhost:6598/api/get");
                    // builder.Query = "name='abc'&password='cde'";
                    // var result = client.GetAsync(builder.Uri).Result;



                    //string.Format("ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", apiKey, builder.ToString())

                    //******************
                    // Com query params
                    //******************
                    UriBuilder builder = new UriBuilder(client.BaseAddress);
                    builder.Path = "/formas-pagamento/1/opcoes-parcelamento";
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["idCampanha"] = "5940";
                    query["cnpj"] = "57.822.975/0001-12";
                    query["valorParcelar"] = "1000";
                    builder.Query = query.ToString();
                    Console.WriteLine(builder.ToString());
                    OpcoesParcelamentoDTO opcoesParcelamentoDTO = await Get2(builder.ToString());
                    Console.WriteLine($"Valor parcela: {opcoesParcelamentoDTO.Data[0].ValorParcela}");

                    //************************
                    // Somente com path param
                    //************************
                    builder = new UriBuilder(client.BaseAddress);
                    builder.Path = "/lojistas/15/produtos/5880205";
                    ProdutoDTO produtoDTO = await Get(builder.ToString());
                    Console.WriteLine($"Name: {produtoDTO.Data.Nome}");
                }

                if (method == "PUT")
                {
                    // Update the product
                    // Console.WriteLine("Updating price...");
                    // product.Data.Valor = 80;
                    // await UpdateProductAsync(product);

                    // Get the updated product
                    //product = await GetProductAsync(url);
                    //ShowProduct(product);

                }

                if(method == "PATCH")
                {

                }

                if (method == "DELETE")
                {
                    // Delete the product
                    //var statusCode = await DeleteProductAsync(product.Data.Nome);
                    //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}

