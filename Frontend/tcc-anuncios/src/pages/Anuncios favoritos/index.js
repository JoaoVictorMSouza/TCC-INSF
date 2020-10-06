import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";

import '../../components/Cabecalho/cabecalho.css'
import '../Login/style.css'

import Logo from '../../assets/image/Capturar.PNG'

import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';


export default function MeusAnunciosFavoritos(props){
    const [infos, setInfos] = useState(props.location.state);

    return(
        <div>
            <div className="cabecalho">
                <div>
                    <Link className="hihi" to="/" ><img class="logo" src={Logo} width="150" height="27px"/></Link>
                </div>
                <div className="barraPesquisa">
                    barra de pesquisa
                </div>
                <div className="meio">
                    MeusafvoritosMeuanunsiosMueperfil
                </div>
                <div>
                    <Link  class="hihi" to="/Anunciar"><button class="botao">Anunciar</button></Link>
                </div>
            </div>


            <h5>MeusAnunciosFavoritos</h5>
        </div>
    )
}