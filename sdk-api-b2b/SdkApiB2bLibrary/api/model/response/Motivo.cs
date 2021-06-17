using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SdkApiB2bLibrary.api.model.response
{
    public class Motivo
    {		
	public string Categoria { get; set; }		
	public string Assunto { get; set; }
	[JsonProperty("Motivo")]
	public string motivo { get; set; }		
	public string Observacao { get; set; }
	}
}
