using DiagnosticoComportamental;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var formulario = Formulario.Ativo;
			var questionario = new Questionario();

			questionario.Nome = "Bruno";

			questionario.Respostas.Adicionar(formulario.Perguntas.Skip(0).FirstOrDefault().Respostas.Skip(0).FirstOrDefault());
			questionario.Respostas.Adicionar(formulario.Perguntas.Skip(1).FirstOrDefault().Respostas.Skip(1).FirstOrDefault());
			questionario.Respostas.Adicionar(formulario.Perguntas.Skip(2).FirstOrDefault().Respostas.Skip(2).FirstOrDefault());
			questionario.Respostas.Adicionar(formulario.Perguntas.Skip(3).FirstOrDefault().Respostas.Skip(3).FirstOrDefault());
			questionario.Respostas.Adicionar(formulario.Perguntas.Skip(4).FirstOrDefault().Respostas.Skip(4).FirstOrDefault());
		}
	}
}