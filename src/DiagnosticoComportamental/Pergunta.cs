using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiagnosticoComportamental
{
	public class Questionario
	{
		public Int64 Id { get; } = Util.NewId();
		public static readonly List<Questionario> Ativo = new List<Questionario>();
		public Formulario Formulario { get; } = Formulario.Ativo;

		public String Nome { get; set; }
		public String EMail { get; set; }

		public Lista<Resposta> Respostas { get; set; }

		public Questionario()
		{
			Respostas = new Lista<Resposta>(r => { });
		}

		public enum Habilidade
		{
			Comunicacao = 0,
			Lideranca = 1,
			Pensamento_Critico = 2,
			Resolucao_Problemas = 3,
			Racionalidade = 4
		}


		public String[] ObterPerfil()
		{
			var total_Comunicacao = Formulario.Ativo.Perguntas.Select(p => p.Respostas.Max(r => r.Peso.Skip(0).FirstOrDefault())).ToArray().Sum();
			var total_Lideranca = Formulario.Ativo.Perguntas.Select(p => p.Respostas.Max(r => r.Peso.Skip(1).FirstOrDefault())).ToArray().Sum();
			var total_Pensamento_Critico = Formulario.Ativo.Perguntas.Select(p => p.Respostas.Max(r => r.Peso.Skip(2).FirstOrDefault())).ToArray().Sum();
			var total_Resolucao_Problemas = Formulario.Ativo.Perguntas.Select(p => p.Respostas.Max(r => r.Peso.Skip(3).FirstOrDefault())).ToArray().Sum();
			var total_Racionalidade = Formulario.Ativo.Perguntas.Select(p => p.Respostas.Max(r => r.Peso.Skip(4).FirstOrDefault())).ToArray().Sum();

			var Comunicacao = Respostas.Sum(r => r.Peso.Skip(0).FirstOrDefault());
			var Lideranca = Respostas.Sum(r => r.Peso.Skip(1).FirstOrDefault());
			var Pensamento_Critico = Respostas.Sum(r => r.Peso.Skip(2).FirstOrDefault());
			var Resolucao_Problemas = Respostas.Sum(r => r.Peso.Skip(3).FirstOrDefault());
			var Racionalidade = Respostas.Sum(r => r.Peso.Skip(4).FirstOrDefault());

			var resultado = new[] {
				$"Comunicação: {Decimal.Round(Comunicacao / total_Comunicacao * 100, 2)} %",
				$"Liderança: {Decimal.Round(Lideranca / total_Lideranca * 100, 2)} %",
				$"Pensamento Crítico: {Decimal.Round(Pensamento_Critico / total_Pensamento_Critico * 100, 2)} %",
				$"Resolução de Problemas: {Decimal.Round(Resolucao_Problemas / total_Resolucao_Problemas * 100, 2)} %",
				$"Racionalidade: {Decimal.Round(Racionalidade / total_Racionalidade * 100, 2)} %"
			};

			var arquivo = new FileInfo(@"D:\JH\" + Id + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt");
			if (arquivo.Exists) arquivo.Delete();
			else if (!arquivo.Directory.Exists) arquivo.Directory.Create();

			File.WriteAllText(arquivo.FullName,
				Nome + "\r\n" +
				EMail + "\r\n" +
				String.Join("\r\n", resultado) + "\r\n" +
				String.Join("\r\n", Respostas.Select(r => r.Descricao))
			);

			return resultado;
		}
	}


	public class Formulario
	{
		public Int64 Id { get; } = Util.NewId();
		public static readonly Formulario Ativo = new Formulario();

		public Lista<Pergunta> Perguntas { get; set; }

		public Formulario()
		{
			Perguntas = new Lista<Pergunta>(p => p.Formulario = this);
			Setup();
		}

		public void Setup()
		{
			Perguntas.Adicionar(Pergunta1());
			Perguntas.Adicionar(Pergunta2());
			Perguntas.Adicionar(Pergunta3());
			Perguntas.Adicionar(Pergunta4());
			Perguntas.Adicionar(Pergunta5());
		}

		private static Pergunta Pergunta1()
		{
			var pergunta = new Pergunta()
			{
				Descricao = "Você está voltando sozinho pra casa depois de uma festa. Já é madrugada, você está a pé e, de repente, as luzes da rua se apagam. Você se depara repentinamente com uma figura sombria do outro lado da rua que para na sua frente e fica te encarando. O que você faria?"
			};

			pergunta.Respostas.Adicionar(new[] {
				new Resposta { Descricao = "a) saio correndo pro lado oposto ao da figura sombria.", Perfil = "rapidez", Peso = new []{0M, 0M, 0M, 10M, 0M, 10M } },
				new Resposta { Descricao = "b) enfrento a figura sombria, pois nada me assusta.", Perfil = "coragem", Peso = new []{0M, 0M, 0M, 10M, 0M, 10M } },
				new Resposta { Descricao = "c) fico pensando durante alguns minutos antes de tomar a decisão.", Perfil = "insegurança", Peso = new []{0M, 0M, 1M, 1M, 0M, 2M } },
				new Resposta { Descricao = "d) ajo naturalmente, afinal de contas é uma situação normal.", Perfil = "apatia", Peso = new []{0M, 0M, 1M, 1M, 0M, 2M } },
				new Resposta { Descricao = "e) faço amizade com a figura sombria. Posso até descobrir se ela é mesmo sombria ou não.", Perfil = "sociável", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } }
			});

			return pergunta;
		}

		private static Pergunta Pergunta2()
		{
			var pergunta = new Pergunta()
			{
				Descricao = "Vai haver uma reunião importante na próxima semana e seu líder conta com você para representar a empresa. Quais ações vão acontecer?"
			};

			pergunta.Respostas.Adicionar(new[] {
				new Resposta { Descricao = "a) senta com seu líder e pede todas as informaçoes necessárias para você particiar da reunião.", Perfil = "bom ouvinte", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "b) fica tenso (a) pois sabe que só vai receber informações na véspera da reunião.", Perfil = "insegurança", Peso = new []{1M, 0M, 0M, 0M, 1M, 2M } },
				new Resposta { Descricao = "c) conversa com todas as pessoas envolvidas no assunto da reunião para se atualizar.", Perfil = "dinâmica", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "d) não se preocupa, pois mesmo que você não conheça o assunto, vai usar sua habilidade de comunicação para se sair bem na reunião.", Perfil = "pouco comprometido", Peso = new []{0M, 0M, 1M, 0M, 1M, 2M } },
				new Resposta { Descricao = "e) busca atas das reuniões anteriores para se atualizar sobre o tema.", Perfil = "preocupado", Peso = new []{0M, 2M, 5M, 3M, 0M, 10M } }
			});

			return pergunta;
		}

		private static Pergunta Pergunta3()
		{
			var pergunta = new Pergunta()
			{
				Descricao = "Você é convocado para apresentar sua proposta de novo negócio para um mega investidor que ficou interessado. Quais são seus próximos?"
			};

			pergunta.Respostas.Adicionar(new[] {
				new Resposta { Descricao = "a) treino em frente ao espelho tudo aquilo que já estava pronto há muito tempo, só esperando essa oportunidade.", Perfil = "ansiedade", Peso = new []{0M, 0M, 0M, 5M, 0M, 5M } },
				new Resposta { Descricao = "b) nem durmo para preparar o material todo de um dia para o outro. Mesmo sabendo que a apresentação é semana que vem, eu prefiro estar preparado com antecedência.", Perfil = "extremamente ansioso", Peso = new []{0M, 0M, 1M, 1M, 0M, 2M } },
				new Resposta { Descricao = "c) dou mais uma ohada no material que já está pronto. Releio e pronto.", Perfil = "seguro", Peso = new []{5M, 0M, 2M, 2M, 1M, 10M } },
				new Resposta { Descricao = "d) pergunta para o investidor o que ele quer saber exatamente. Você não vai falar nada que ele não queira ouvir.", Perfil = "assertividade", Peso = new []{2M, 0M, 0M, 4M, 4M, 10M } },
				new Resposta { Descricao = "e) planejo tópicos e palavras-chave para colocar numa apresentação.", Perfil = "poder de síntese", Peso = new []{2M, 0M, 0M, 4M, 4M, 10M } }
			});

			return pergunta;
		}

		private static Pergunta Pergunta4()
		{
			var pergunta = new Pergunta()
			{
				Descricao = "Durante uma reunião de negócios um dos participantes se exalta e ameaça de agressão física um outro participante. Qual a sua atitude?"
			};

			pergunta.Respostas.Adicionar(new[] {
				new Resposta { Descricao = "a) interveria de forma ativa, separando os dois.", Perfil = "proatividade", Peso = new []{0M, 5M, 0M, 5M, 0M, 10M } },
				new Resposta { Descricao = "b) tentaria acalmá-los através do diálogo", Perfil = "apaziguador", Peso = new []{0M, 7M, 0M, 3M, 0M, 10M } },
				new Resposta { Descricao = "c) fico olhando assustado. Não sei o que fazer", Perfil = "passividade", Peso = new []{0M, 0M, 3M, 0M, 7M, 10M } },
				new Resposta { Descricao = "d) olho para os outros cobrando uma ação deles", Perfil = "falta de autorresponsabilidade", Peso = new []{0M, 0M, 1M, 0M, 1M, 2M } },
				new Resposta { Descricao = "e) Imposto a voz um pouco mais alto para que todos se acalmem", Perfil = "dominância", Peso = new []{10M, 0M, 0M, 0M, 0M, 10M } }
			});

			return pergunta;
		}

		private static Pergunta Pergunta5()
		{
			var pergunta = new Pergunta()
			{
				Descricao = "Uma grande instituição internacional selecionou você para escolher um destes prêmios. Qual você escolheria?"
			};

			pergunta.Respostas.Adicionar(new[] {
				new Resposta { Descricao = "a) ingressos exclusivos para a ópera dos seus sonhos", Perfil = "auditivo", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "b) entradas para os bastidores da fórmula 1", Perfil = "cinestésico", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "c) ferramentas e materiais para o 'faça você mesmo' que você sempre quis", Perfil = "cinestésico", Peso = new []{5M, 5M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "d) par de passagens para uma cidadezinha pacata do interior onde haverá uma festividade típica", Perfil = "visual", Peso = new []{10M, 0M, 0M, 0M, 0M, 10M } },
				new Resposta { Descricao = "e) acesso gratuito à biblioteca nacional, podendo emprestar qualquer livro gratuitamente", Perfil = "visual", Peso = new []{10M, 0M, 0M, 0M, 0M, 10M } }
			});

			return pergunta;
		}
	}


	public class Pergunta
	{
		public Int64 Id { get; } = Util.NewId();
		public String Descricao { get; set; }
		public Formulario Formulario { get; internal set; }
		public Lista<Resposta> Respostas { get; set; }
		public Pergunta()
		{
			Respostas = new Lista<Resposta>(r => r.Pergunta = this);
		}
	}

	public class Resposta
	{
		public Int64 Id { get; } = Util.NewId();
		public Pergunta Pergunta { get; set; }
		public String Descricao;

		public String Perfil { get; set; }
		public Decimal[] Peso { get; set; }
	}

	public static class Util
	{
		private static Int64 Id = 0;

		public static Int64 NewId()
		{
			return ++Id;
		}
	}

}
