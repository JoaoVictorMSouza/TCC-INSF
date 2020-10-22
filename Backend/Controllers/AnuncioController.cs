﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnuncioController : ControllerBase
    {
        Business.AnuncioBusiness businessAnuncio = new Business.AnuncioBusiness();
        Utils.AnuncioConversor conversorAnuncio = new Utils.AnuncioConversor();
        Business.GerenciadorImagem gerenciadorImagem = new Business.GerenciadorImagem();
        [HttpGet("foto/{nome}")]
        public ActionResult BuscarFoto(string nome)
        {
            try 
            {
                byte[] foto = gerenciadorImagem.LerFoto(nome);
                string contentType = gerenciadorImagem.GerarContentType(nome);
                return File(foto, contentType);
            }
            catch (System.Exception ex)
            {
                return BadRequest(
                    new Models.Response.Erro(404, ex.Message)
                );
            }

        }
        [HttpGet("{BarraPesquisa}/{Estado}/{Cidade}/{Tamanho}/{Genero}/{Condicao}")]
        public ActionResult<List<Models.Response.AnuncioRoupasResponse.Anuncio>> ConsultarAnuncios(string BarraPesquisa, string Estado, string Cidade, string Tamanho, string Genero, string Condicao)
        {
            try
            {
                List<Models.TbAnuncio> anuncios = businessAnuncio.ConsultarAnuncios(BarraPesquisa, Estado, Cidade, Tamanho, Genero, Condicao);
                return conversorAnuncio.ConversorAnuncioListaResponse(anuncios);
            }
            catch (System.Exception ex)
            {
                return NotFound(new Models.Response.Erro(404, ex.Message));
            }
        }
        [HttpGet("AnuncioDetalhado/{IdAnuncio}")]
        public ActionResult<Models.Response.AnuncioRoupasResponse.AnuncioDetalhado> ConsultarAnuncioDetalhado(int IdAnuncio)
        {
            try
            {
                Models.TbAnuncio resp = businessAnuncio.ConsultadoAnuncioDetalhado(IdAnuncio);
                return conversorAnuncio.AnuncioDetalhadoResponse(resp);
            }
            catch (System.Exception ex)
            {
                return NotFound(new Models.Response.Erro(404, ex.Message));
            }
        }
        [HttpPost("Perguntar/")]
        public ActionResult<Models.Response.AnuncioRoupasResponse.PerguntaEResposta> Perguntar(Models.Request.AnuncioRoupasRequest.Pergunta req)
        {
            try
            {
                Models.TbPerguntaResposta a = conversorAnuncio.PerguntarParaTabela(req);
                Models.TbPerguntaResposta b = businessAnuncio.Perguntar(a);
                return conversorAnuncio.PerguntarParaResponse(b);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Models.Response.Erro(400, ex.Message));
            }
        }
        [HttpPut("Responder/")]
        public ActionResult<Models.Response.AnuncioRoupasResponse.PerguntaEResposta> Responder(Models.Request.AnuncioRoupasRequest.Resposta req)
        {
            try
            {
                Models.TbPerguntaResposta a = conversorAnuncio.ResponderParaTabela(req);
                Models.TbPerguntaResposta b = businessAnuncio.Responder(a);
                return conversorAnuncio.ResponderParaResponse(b);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Models.Response.Erro(400, ex.Message));
            }
        }
        [HttpPost("Anunciar/")]
        public ActionResult<Models.Response.AnuncioRoupasResponse.Anuncio> Anunciar([FromForm] Models.Request.AnuncioRoupasRequest.Anunciar anuncio)
        {
            try
            {
                Models.TbAnuncio anu1 = conversorAnuncio.AnuncioParaTabela(anuncio);
                anu1.TbImagem = gerenciadorImagem.GerarMuitosNomes(anuncio.Imagens);
                Models.TbAnuncio resp = businessAnuncio.Anunciar(anu1);
                return conversorAnuncio.AnuncioParaResponse(resp);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Models.Response.Erro(400, ex.Message));
            }
        }

        
        [HttpPost("test/")]
        public ActionResult<Models.Response.AnuncioRoupasResponse.Anuncio> testando([FromForm] Models.Request.AnuncioRoupasRequest.test anuncio)
        {
            try
            {
                return null;
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Models.Response.Erro(400, ex.Message));
            }
        }



        [HttpGet("MeusAnuncios/{IdUsuario}")]
        public ActionResult<List<Models.Response.AnuncioRoupasResponse.MeusAnuncios>> ConsultarMeusAnuncios(int IdUsuario)
        {
            try
            {
                List<Models.TbAnuncio> resp = businessAnuncio.ConsultarMeusAnuncios(IdUsuario);
                return conversorAnuncio.ConversorVariosMeusAnunciosParaResponse(resp);
            }
            catch (System.Exception ex)
            {
                return NotFound(new Models.Response.Erro(400, ex.Message));
            }
        }
        [HttpDelete("{IdAnuncio}")]
        public ActionResult<Models.TbAnuncio> DeletarAnuncio (int? IdAnuncio)
        {
            try     
            {
                Models.TbAnuncio anuncios = businessAnuncio.DeletarAnuncio(IdAnuncio);
                
                return anuncios;
            }
            catch (System.Exception )
            {
                return NotFound(null);
            }
        }      
    }
}
