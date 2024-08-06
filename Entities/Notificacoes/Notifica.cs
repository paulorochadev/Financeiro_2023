using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifica> Notificacoes;

        public bool ValidaPropriedadeString(string valor, string nomeProriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomeProriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório!",
                    NomePropriedade = nomeProriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadeInt(int valor, string nomeProriedade)
        {
            if (valor <1 || string.IsNullOrWhiteSpace(nomeProriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório!",
                    NomePropriedade = "nomeProriedade"
                });

                return false;
            }

            return true;
        }
    }
}