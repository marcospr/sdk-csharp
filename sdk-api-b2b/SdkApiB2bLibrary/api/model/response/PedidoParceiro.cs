using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdkApiB2bLibrary.api.model.response
{
    public class PedidoParceiro
    {
		public int CodigoPedido { get; set; }
		public string DataHora { get; set; }
		public int IdPedidoMktplc { get; set; }
		[JsonProperty("PedidoParceiro")]
		public long pedidoParceiro { get; set; }
		public string UrlBoleto { get; set; }
		public double ValorAdicional { get; set; }
		public double ValorFrete { get; set; }
		public double ValorProduto { get; set; }
		public double ValorTotalPedido { get; set; }
	}
}
