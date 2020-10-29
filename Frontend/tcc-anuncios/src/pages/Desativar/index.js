import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";

import '../../components/Cabecalho/cabecalho.css'
import '../Desativar/style.css'

import Logo from '../../assets/image/Capturar.PNG'

import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

import anuncioAPI from '../../services/anuncioAPI';
const api = new anuncioAPI();

export default function ExcluirAnuncio(props){
    const navegacao = useHistory();

    const [infosUser, setInfosUser] = useState(props.location.state);
    const [meusanuncios, setMeusAnuncios] = useState([]);
    console.log(infosUser);

    const inativarAnuncio = async () => {
        try{
            const resp = await api.inativarAnuncio(infosUser.item.idAnuncio)
            navegacao.goBack();
        }
        catch (e){

        }
    }

    const consultarMeusAnuncios = async () => {
        try{
            const resp = await api.consultarAnuncioDetalhado(infosUser.item.idAnuncio);
            setMeusAnuncios(resp);
        }
        catch (e){

        }
    }

    useEffect(() => {
        consultarMeusAnuncios();
      }, []);

    return(
        <div className="nin">
            <div className="cabecalho">
                <div>
                    <Link className="hihi" to={{pathname:"/Home", state: infosUser.infos}} ><img class="logo" src={Logo} width="150" height="27px" alt=''/></Link>
                </div> 

                <div className="barraPesquisa hihi">
                    <Link to={{pathname:"/Home", state: infosUser.infos}}>Fazer novas consultas</Link>
                </div>
                
                <div className="meio"> 
                    <Link class="hihi meio" to={{ pathname: "/MeuPerfil", state: infosUser.infos}}>Meu perfil</Link>
                    <Link class="hihi meio" to={{ pathname: "/MeusAnuncios", state: infosUser.infos }}>Meus Anuncios</Link>
                    <Link class="hihi meio" to={{ pathname: "/MeusFavoritos", state: infosUser.infos }}>Meus Favoritos</Link>
                </div>
                <div>
                    <Link  class="hihi" to="/Anunciar"><button class="botao">Anunciar</button></Link>
                </div>
            </div>
            
            <div className="nan">
                <h5>O anuncio: <b>{meusanuncios.titulo}</b> será inativado</h5>
                <h5>Tem certeza disso?</h5> 

                <div className="sahaha">
                    <Link to={{ pathname: "/MeusAnuncios", state: infosUser.infos}}><button className="botoxo">Não</button></Link>
                    <button className="botoxo" onClick={inativarAnuncio}>Sim</button>
                </div>
            </div>
        </div>
    )
}