using DiagnosticoComportamental;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var questionario = new Questionario();
			return View(questionario);
		}

		[HttpPost]
		public ActionResult Index(FormCollection collection)
		{
			try
			{
				var questionario = new Questionario() { Nome = collection["Nome"],  EMail= collection["EMail"] };
				foreach (var pergunta in Formulario.Ativo.Perguntas)
				{
					var value = Convert.ToInt64(Convert.ToString(collection["p" + pergunta.Id]));
					var resposta = pergunta.Respostas.FirstOrDefault(r => r.Id == value);
					if (resposta != null)
						questionario.Respostas.Adicionar(resposta);
				}
				Questionario.Ativo.Add(questionario);
				return View("Resultado", questionario);
			}
			catch (Exception)
			{
				ViewBag.Info = "Preencha Todo o Formulário";
				return View();
			}
		}

		public ActionResult Resultado()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}