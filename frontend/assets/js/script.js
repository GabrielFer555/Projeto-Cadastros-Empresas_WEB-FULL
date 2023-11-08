const atualizaDadosFornecedor= () => { /* Atualiza número do HUD do fornecedor*/
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
    const textNumb = document.getElementsByClassName("nullTable")[0];
    if (colunas.length > 0) {
        textNumb.setAttribute("class", "d-none");
    }
}

const atualizaDadosEmpresa = async () => { /* Atualiza número do HUD da empresa*/
    const response = await fetch("http://localhost:5143/v1/Company");
    const dados = await response.json();
    const tabela = document.getElementsByTagName("tbody")[0];
    let data='';
    for(ct of dados) {
        data += `
        <tr class="col columns">
        <a href="companyCadScreen.html?id=${ct.empresaId}">
                    <th>
                        ${ct.empresaId}
                    </th>
                    <th>
                        ${ct.name}
                    </th>
                    <th>
                        ${ct.document}
                    </th>
                    <th>
                        ${ct.uf}
                    </th>
                    <th>
                    <a href="editCompany.html?id='${ct.empresaId}'">Editar</a>
                    </th>

                </tr>
    `}
    $("#tableRegisters").DataTable({
        "search": {
            "smart": true,
            "regex": true
        },
        language: {
            "searchPlaceholder": "Pesquise por CNPJ, Nome da Empresa, etc...",
            "search": "Search:",
            "info": "Exibindo _START_ até _END_ de _TOTAL_ Linhas",

        },
        "data": dados,
        "columns":[
            {"data": "empresaId"},
            {"data": "name"},
            {"data":"document"},
            {"data": "uf"},
            {
                "data": null,
                "render": function (data, type, row) {
                    return '<a href="editCompany.html?id=' + data + '">Editar</a>';
                }}
                

        ]
    })
    tabela.innerHTML = data;
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
}

const validaSeInvisivel = (elementoHTML) => {
    const validacao = elementoHTML.classList.contains("noValidation")? true:false;
    return validacao
}