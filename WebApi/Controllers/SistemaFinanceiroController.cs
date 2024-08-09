using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaFinanceiroController : ControllerBase
    {
        private readonly InterfaceSistemaFinanceiro _interfaceSistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _iSistemaFinanceiroServico;

        public SistemaFinanceiroController(InterfaceSistemaFinanceiro interfaceSistemaFinanceiro, ISistemaFinanceiroServico iSistemaFinanceiroServico)
        {
            _interfaceSistemaFinanceiro = interfaceSistemaFinanceiro;
            _iSistemaFinanceiroServico = iSistemaFinanceiroServico;
        }

        [HttpGet("/api/ListaSistemasUusario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUusario(string emailUsuario)
        {
            return await _interfaceSistemaFinanceiro.ListaSistemasUusario(emailUsuario);
        }

        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _iSistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpPut("/api/AtualizarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _iSistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpGet("/api/ObterSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ObterSistemaFinanceiro(int id)
        {
            return await _interfaceSistemaFinanceiro.GetEntityById(id);
        }

        [HttpDelete("/api/DeleteSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeleteSistemaFinanceiro(int id)
        {
            try
            {
                var sistemaFinanceiro = await _interfaceSistemaFinanceiro.GetEntityById(id);

                await _interfaceSistemaFinanceiro.Delete(sistemaFinanceiro);
            }

            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}