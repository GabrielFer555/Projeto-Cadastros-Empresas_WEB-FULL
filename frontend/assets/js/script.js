var url = "http://localhost:5143";

const atualizaDadosFornecedor = () => { /* Atualiza número do HUD do fornecedor*/
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
    const textNumb = document.getElementsByClassName("nullTable")[0];
    if (colunas.length > 0) {
        textNumb.setAttribute("class", "d-none");
    }
}

<<<<<<< Updated upstream
const atualizaDadosEmpresa = async () => { /* Atualiza número do HUD da empresa e faz Request pra rota de empresas*/
    try {
        const response = await fetch(`${url}/v1/Company`);
        const dados = await response.json();
        console.log(dados) //testes
        const tabela = document.getElementsByTagName("tbody")[0];
        let data = '';
        for (ct of dados) {
            data += `
=======
const atualizaDadosEmpresa = async () => { /* Atualiza número do HUD da empresa*/
try{
    const response = await fetch("http://localhost:5143/v1/Company");
    const dados = await response.json();
    console.log(dados) //testes
    const tabela = document.getElementsByTagName("tbody")[0];
    let data='';
    for(ct of dados) {
        data += `
>>>>>>> Stashed changes
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
                    <a href="editCompany.html?id=${ct.empresaId}">Editar</a>
                    </th>

                </tr>
    `;
        }
        $("#tableRegisters").DataTable({
            "search": {
                "smart": true,
                "regex": true
            },
            language: {
                "searchPlaceholder": "Pesquise por CNPJ, Nome da Empresa, etc...",
                "search": "Search:",
                "info": "Exibindo _START_ até _END_ de _TOTAL_ Linhas",

<<<<<<< Updated upstream
            },
            "data": dados,
            "columns": [
                { "data": "empresaId" },
                { "data": "name" },
                { "data": "document" },
                { "data": "uf" },
            ]
        })
        tabela.innerHTML = data;
        const fornCadastradas = document.getElementsByClassName("span-num")[0];
        const colunas = document.getElementsByClassName("columns");
        fornCadastradas.textContent = colunas.length.toString();
    } catch (err) {
        mensagem("Ocorreu um erro! Recarregue a página!", 2, 2000)
    }

=======
        },
        "data": dados,
        "columns":[
            {"data": "empresaId"},
            {"data": "name"},
            {"data":"document"},
            {"data": "uf"},
                

        ]
    })
    tabela.innerHTML = data;
    const fornCadastradas = document.getElementsByClassName("span-num")[0];
    const colunas = document.getElementsByClassName("columns");
    fornCadastradas.textContent = colunas.length.toString();
}catch(error){
    mensagem("Ocorreu um erro! Recarregue a página!",2);
>>>>>>> Stashed changes
}
}

   

const validaSeInvisivel = (elementoHTML) => { /*Valida se algum campo está invisivel no Input */
    const validacao = elementoHTML.classList.contains("noValidation") ? true : false;
    return validacao
}

const carregarPara = (pagina, time = 1000) => {
    $.LoadingOverlay("show", {
        background: "rgba(0, 0, 0, 0.8)",
        image: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: none; display: block; shape-rendering: auto;" width="200px" height="200px" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
        <g transform="translate(50 50)">
            <g>
        <animateTransform attributeName="transform" type="rotate" values="0;45" keyTimes="0;1" dur="0.2s" repeatCount="indefinite"></animateTransform><path d="M29.491524206117255 -5.5 L37.491524206117255 -5.5 L37.491524206117255 5.5 L29.491524206117255 5.5 A30 30 0 0 1 24.742744050198738 16.964569457146712 L24.742744050198738 16.964569457146712 L30.399598299691117 22.621423706639092 L22.621423706639096 30.399598299691114 L16.964569457146716 24.742744050198734 A30 30 0 0 1 5.5 29.491524206117255 L5.5 29.491524206117255 L5.5 37.491524206117255 L-5.499999999999997 37.491524206117255 L-5.499999999999997 29.491524206117255 A30 30 0 0 1 -16.964569457146705 24.742744050198738 L-16.964569457146705 24.742744050198738 L-22.621423706639085 30.399598299691117 L-30.399598299691117 22.621423706639092 L-24.742744050198738 16.964569457146712 A30 30 0 0 1 -29.491524206117255 5.500000000000009 L-29.491524206117255 5.500000000000009 L-37.491524206117255 5.50000000000001 L-37.491524206117255 -5.500000000000001 L-29.491524206117255 -5.500000000000002 A30 30 0 0 1 -24.742744050198738 -16.964569457146705 L-24.742744050198738 -16.964569457146705 L-30.399598299691117 -22.621423706639085 L-22.621423706639092 -30.399598299691117 L-16.964569457146712 -24.742744050198738 A30 30 0 0 1 -5.500000000000011 -29.491524206117255 L-5.500000000000011 -29.491524206117255 L-5.500000000000012 -37.491524206117255 L5.499999999999998 -37.491524206117255 L5.5 -29.491524206117255 A30 30 0 0 1 16.964569457146702 -24.74274405019874 L16.964569457146702 -24.74274405019874 L22.62142370663908 -30.39959829969112 L30.399598299691117 -22.6214237066391 L24.742744050198738 -16.964569457146716 A30 30 0 0 1 29.491524206117255 -5.500000000000013 M0 -20A20 20 0 1 0 0 20 A20 20 0 1 0 0 -20" fill="#e15b64"></path></g></g>
        <!-- [ldio] generated by https://loading.io/ --></svg>`,
        imageColor: "#2cc415"
    })
    setTimeout(() => {
        location.href = pagina;
        $.LoadingOverlay("hide");
    }, time)

}
const mensagem = (msg, type = 1, duration = 1000, x = "left", y = "bottom") => {
    console.log($(".form-control"))
    const i = new Notyf({
        duration: duration,
        position: {
            x: x,
            y: y
        },
    });
    switch (type) {
        case 1:
            i.success(msg);
            break;
        case 2:
            i.error(msg);
            break;
    }
}