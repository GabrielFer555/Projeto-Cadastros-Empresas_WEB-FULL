const atualizaDadosFornecedor= () => { /* Atualiza número do HUD do fornecedor*/
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
    const textNumb = document.getElementsByClassName("nullTable")[0];
    if (colunas.length > 0) {
        textNumb.setAttribute("class", "d-none");
    }
}

const atualizaDadosEmpresa = () => { /* Atualiza número do HUD da empresa*/
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
    const textNumb = document.getElementsByClassName("nullTable")[0];
    if (colunas.length > 0) {
        textNumb.setAttribute("class", "d-none");
    }
}

const validaSeInvisivel = (elementoHTML) => {
    const validacao = elementoHTML.classList.contains("noValidation")? true:false;
    return validacao
}