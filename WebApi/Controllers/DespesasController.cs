﻿using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Domain.Servicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DespesasController : ControllerBase
    {
        private readonly InterfaceDespesa _interfaceDespesa;
        private readonly IDespesaServico _iDespesaServico;

        public DespesasController(InterfaceDespesa interfaceDespesa, IDespesaServico iDespesaServico)
        {
            _interfaceDespesa = interfaceDespesa;
            _iDespesaServico = iDespesaServico;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Despesa despesa)
        {
            await _iDespesaServico.AdicionarDespesa(despesa);

            return Ok(despesa);
        }

        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Despesa despesa)
        {
            await _iDespesaServico.AtualizarDespesa(despesa);

            return Ok(despesa);
        }

        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _interfaceDespesa.GetEntityById(id);
        }

        [HttpDelete("/api/DeleteDespesa")]
        [Produces("application/json")]
        public async Task<object> DeleteDespesa(int id)
        {
            try
            {
                var categoria = await _interfaceDespesa.GetEntityById(id);

                await _interfaceDespesa.Delete(categoria);
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
