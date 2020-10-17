using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Database
{
    public class AnuncioDatabase
    {
        Models.anuncioRoupaContext ctx = new Models.anuncioRoupaContext();
        public List<Models.TbAnuncio> ConsultarAnuncios(string BarraPesquisa, string Estado, string Cidade, string Tamanho, string Genero, string Condicao)
        {
            List<Models.TbAnuncio> anuncios = ctx.TbAnuncio.Include(x => x.TbImagem)
                                                           .Where(x => x.BtVendido == false && x.DsSituacao == "Publicado" && x.DsTitulo.ToLower().Contains(BarraPesquisa.ToLower()) 
                                                                  && x.DsEstado.ToLower().Contains(Estado.ToLower()) && x.DsCidade.ToLower().Contains(Cidade.ToLower())
                                                                  && x.DsTamanho.ToLower().Contains(Tamanho.ToLower()) && x.DsGenero.ToLower().Contains(Genero.ToLower()) 
                                                                  && x.DsCondicao.ToLower().Contains(Condicao.ToLower()))
                                                          
                                                           .OrderByDescending(x => x.DtPublicacao).ToList();
            return anuncios;
        }
        public Models.TbAnuncio ConsultarAnuncioDetalhado(int IdAnuncio)
        {
            Models.TbAnuncio resp = ctx.TbAnuncio.Include(x => x.IdUsuarioNavigation).Include(x => x.TbImagem)
                                                                                     .Include(x => x.IdUsuarioNavigation)
                                                                                     .Include(x => x.TbPerguntaResposta)
                                                                                     .FirstOrDefault(x => x.IdAnuncio == IdAnuncio);
            return resp;
        }
    }
}