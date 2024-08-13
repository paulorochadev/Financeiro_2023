﻿using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class DespesaServico : IDespesaServico
    {
        private readonly InterfaceDespesa _interfaceDespesa;

        public DespesaServico(InterfaceDespesa interfaceDespesa)
        {
            _interfaceDespesa = interfaceDespesa;
        }

        public async Task AdicionarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;
            despesa.Ano = data.Year;
            despesa.Mes = data.Month;

            var valido = despesa.ValidaPropriedadeString(despesa.Nome, "Nome");

            if (valido)
            {
                await _interfaceDespesa.Add(despesa);
            }
        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataAlteracao = data;

            if (despesa.Pago)
            {
                despesa.DataPagamento = data;
            }

            var valido = despesa.ValidaPropriedadeString(despesa.Nome, "Nome");

            if (valido)
            {
                await _interfaceDespesa.Update(despesa);
            }
        }

        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            var despesasUsuario = await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
            var despesasAnterior = await _interfaceDespesa.ListarDespesasUsuarioNaoPagasMesesAnterior(emailUsuario);

            var despesas_naoPagasMesesAnteriores = despesasAnterior.Any() ? despesasAnterior.ToList().Sum(x => x.Valor) : 0;

            var despesas_pagas = despesasUsuario.Where(d => d.Pago && d.TipoDespesa == EnumTipoDespesa.Contas).Sum(x => x.Valor);

            var despesas_pendentes = despesasUsuario.Where(d => !d.Pago && d.TipoDespesa == EnumTipoDespesa.Contas).Sum(x => x.Valor);

            var investimentos = despesasUsuario.Where(d => d.TipoDespesa == EnumTipoDespesa.Investimento).Sum(x => x.Valor);

            return new
            {
                sucesso = "OK",
                despesas_pagas = despesas_pagas,
                despesas_pendentes = despesas_pendentes,
                despesas_naoPagasMesesAnteriores = despesas_naoPagasMesesAnteriores,
                investimentos = investimentos,
            };
        }
    }
}