var url = "http://localhost:5143";

const carregamento = (status) =>{
    switch(status){
        case "mostrar":{
            $.LoadingOverlay("show", {
                background: "rgba(0, 0, 0, 0.8)",
                image: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: none; display: block; shape-rendering: auto;" width="200px" height="200px" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                <g transform="translate(50 50)">
                    <g>
                <animateTransform attributeName="transform" type="rotate" values="0;45" keyTimes="0;1" dur="0.2s" repeatCount="indefinite"></animateTransform><path d="M29.491524206117255 -5.5 L37.491524206117255 -5.5 L37.491524206117255 5.5 L29.491524206117255 5.5 A30 30 0 0 1 24.742744050198738 16.964569457146712 L24.742744050198738 16.964569457146712 L30.399598299691117 22.621423706639092 L22.621423706639096 30.399598299691114 L16.964569457146716 24.742744050198734 A30 30 0 0 1 5.5 29.491524206117255 L5.5 29.491524206117255 L5.5 37.491524206117255 L-5.499999999999997 37.491524206117255 L-5.499999999999997 29.491524206117255 A30 30 0 0 1 -16.964569457146705 24.742744050198738 L-16.964569457146705 24.742744050198738 L-22.621423706639085 30.399598299691117 L-30.399598299691117 22.621423706639092 L-24.742744050198738 16.964569457146712 A30 30 0 0 1 -29.491524206117255 5.500000000000009 L-29.491524206117255 5.500000000000009 L-37.491524206117255 5.50000000000001 L-37.491524206117255 -5.500000000000001 L-29.491524206117255 -5.500000000000002 A30 30 0 0 1 -24.742744050198738 -16.964569457146705 L-24.742744050198738 -16.964569457146705 L-30.399598299691117 -22.621423706639085 L-22.621423706639092 -30.399598299691117 L-16.964569457146712 -24.742744050198738 A30 30 0 0 1 -5.500000000000011 -29.491524206117255 L-5.500000000000011 -29.491524206117255 L-5.500000000000012 -37.491524206117255 L5.499999999999998 -37.491524206117255 L5.5 -29.491524206117255 A30 30 0 0 1 16.964569457146702 -24.74274405019874 L16.964569457146702 -24.74274405019874 L22.62142370663908 -30.39959829969112 L30.399598299691117 -22.6214237066391 L24.742744050198738 -16.964569457146716 A30 30 0 0 1 29.491524206117255 -5.500000000000013 M0 -20A20 20 0 1 0 0 20 A20 20 0 1 0 0 -20" fill="#e15b64"></path></g></g>
                <!-- [ldio] generated by https://loading.io/ --></svg>`,
                imageColor: "#2cc415"
            })
        }case "esconder": {
            $.LoadingOverlay("hide")
        }
    }

}


const toggleCheck = (value) => {
if(value == 1){
    if ($("#juridicas").is(":checked")) {
        $("#rgCase").val("");
        $("#bornDate").val("");
        $("#cpfLabel").text("CPNJ");
        $("#docInsertion").focus();
        $("#docInsertion").attr("placeholder", "Insira o CPNJ do Fornecedor..  (Required)");
        $("#docInsertion").mask('00.000.000/0000-00');
        $(".caseFisica").addClass("d-none");

    } else {
        $("#cpfLabel").text("CPF");
        $("#docInsertion").focus();
        $("#docInsertion").attr("placeholder", "Insira o CPF do Fornecedor..  (Required)");
        $("#docInsertion").mask('000.000.000-00');
        $("#rgCase").mask('0.000.000');
        let can = $(".caseFisica").hasClass("d-none") ? $(".caseFisica").removeClass("d-none") : null;
    }
}else{
    if ($("#juridicasEdit").is(":checked")) {
        $("#rgCaseEdit").val("");
        $("#bornDateEdit").val("");
        $("#cpfLabelEdit").text("CPNJ");
        $("#docEdition").focus();
        $("#docEdition").attr("placeholder", "Insira o CPNJ do Fornecedor..  (Required)");
        $("#docEdition").mask('00.000.000/0000-00');
        $(".caseFisica").addClass("d-none");

    } else {
        $("#cpfLabelEdit").text("CPF");
        $("#docEdition").focus();
        $("#docEdition").attr("placeholder", "Insira o CPF do Fornecedor..  (Required)");
        $("#docEdition").mask('000.000.000-00');
        $("#rgCaseEdit").mask('0.000.000');
        let can = $(".caseFisica").hasClass("d-none") ? $(".caseFisica").removeClass("d-none") : null;
    }
}
    
}

const fornecedorEspecifico = async (id) => {
    try {
        carregamento("mostrar");   
        const response = await fetch(`${url}/v1/Producer/Find/${id}`);
        switch (response.status) {
            case 200: {
                const data = await response.json(); 
                $("#modalEdit").modal("show");
                $("#codForn").val(data.id);
                $("#nomeFornEdit").val(data.name);
                $("#companiesSelectEdit").val(data.empresaVinculada);
                $("#selectFieldEdit").val(data.uf);
                $("#inputTel1Edit").val(data.telefone_1);
                $("#inputTel2Edit").val(data.telefone_2);
                $("#inputTel3Edit").val(data.celular);
                $("#docEdition").val(data.document);
                switch(data.tipoFornecedor){
                    case "f":{
                        $("#fisicasEdit").prop("checked",true);
                        $("#bornDateEdit").val((data.dataNasc).split("-").reverse().join("/"));
                        $("#rgCaseEdit").val(data.rg);
                        toggleCheck(2);
                        break;
                    }
                    case "j":{
                        $("#juridicasEdit").prop("checked", true);
                        toggleCheck(2);
                        break;
                    }
                }
                carregamento("esconder")


                break;
            }
            case 404: {
                carregamento("esconder")
                throw "Fornecedor não encontrado"
            }
        }
    } catch (err) {
        mensagem(err, 2);
    }

}

const atualizaDadosFornecedor = async () => {
    $.LoadingOverlay("show", {
        background: "rgba(0, 0, 0, 0.8)",
        image: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: none; display: block; shape-rendering: auto;" width="200px" height="200px" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
        <g transform="translate(50 50)">
            <g>
        <animateTransform attributeName="transform" type="rotate" values="0;45" keyTimes="0;1" dur="0.2s" repeatCount="indefinite"></animateTransform><path d="M29.491524206117255 -5.5 L37.491524206117255 -5.5 L37.491524206117255 5.5 L29.491524206117255 5.5 A30 30 0 0 1 24.742744050198738 16.964569457146712 L24.742744050198738 16.964569457146712 L30.399598299691117 22.621423706639092 L22.621423706639096 30.399598299691114 L16.964569457146716 24.742744050198734 A30 30 0 0 1 5.5 29.491524206117255 L5.5 29.491524206117255 L5.5 37.491524206117255 L-5.499999999999997 37.491524206117255 L-5.499999999999997 29.491524206117255 A30 30 0 0 1 -16.964569457146705 24.742744050198738 L-16.964569457146705 24.742744050198738 L-22.621423706639085 30.399598299691117 L-30.399598299691117 22.621423706639092 L-24.742744050198738 16.964569457146712 A30 30 0 0 1 -29.491524206117255 5.500000000000009 L-29.491524206117255 5.500000000000009 L-37.491524206117255 5.50000000000001 L-37.491524206117255 -5.500000000000001 L-29.491524206117255 -5.500000000000002 A30 30 0 0 1 -24.742744050198738 -16.964569457146705 L-24.742744050198738 -16.964569457146705 L-30.399598299691117 -22.621423706639085 L-22.621423706639092 -30.399598299691117 L-16.964569457146712 -24.742744050198738 A30 30 0 0 1 -5.500000000000011 -29.491524206117255 L-5.500000000000011 -29.491524206117255 L-5.500000000000012 -37.491524206117255 L5.499999999999998 -37.491524206117255 L5.5 -29.491524206117255 A30 30 0 0 1 16.964569457146702 -24.74274405019874 L16.964569457146702 -24.74274405019874 L22.62142370663908 -30.39959829969112 L30.399598299691117 -22.6214237066391 L24.742744050198738 -16.964569457146716 A30 30 0 0 1 29.491524206117255 -5.500000000000013 M0 -20A20 20 0 1 0 0 20 A20 20 0 1 0 0 -20" fill="#e15b64"></path></g></g>
        <!-- [ldio] generated by https://loading.io/ --></svg>`,
        imageColor: "#2cc415"
    })
    try {
        const response = await fetch(`${url}/v1/Producer/FindAll`);
        const dados = await response.json();
        const table = document.getElementById('tcorpo');
        $("#numbers").text(dados.length)
        console.log(dados);
        let data = '';
        for (ct of dados) {
            let dataformatada = new Date(ct.dataCad)
            data += `
            <tr class = "col columns">
            <th>
                ${ct.id}
            </th>
                <th>
                ${ct.name}
                </th>
                <th>
                    ${ct.document}
                </th>
                <th>
                    ${dataformatada.toLocaleString()}
                </th>
                <th>
                    <button class="btn" onclick="fornecedorEspecifico(${ct.id})">Editar</button>
                </th>
            </tr>
            
            `}
        table.innerHTML = data;

        $("#tableRegisters").DataTable({
            "search": {
                "smart": true,
                "regex": true,
            },
            "searchDelay": 350,
            "language": {
                "searchPlaceholder": "Pesquise por CPF, nome...",
                "search": "Search:",
                "info": "Exibindo _START_ até _END_ de _TOTAL_ Linhas",
                "zeroRecords": "Nada encontrado ;-;",
                "emptyTable": "Nenhum registro encontrado - Cadastre um!",
            },
            data: dados,
            columns: [
                { "data": "id" },
                { "data": "name" },
                { "data": "document" },
                {
                    "data": "dataCad",
                    "render": function (data, type) {
                        const dataformatada = new Date(data);
                        return dataformatada.toLocaleString();
                    }
                },
                { "data": "id",
                    "render":function(data, type){
                        return `<button class="btn" onclick="fornecedorEspecifico(${data})"> Editar </button>`;
                    }
                }
            ]
        });
        const selectCompany = document.getElementById("companiesSelect");
        const selectEditCompany = document.getElementById("companiesSelectEdit");
        const responseOptions = await fetch(`${url}/v1/Company`);
        const options = await responseOptions.json();
        let optionsData = '';
        for (let key of options) {
            optionsData += `<option value="${key.empresaId}">${key.name}</option>`
        }
        selectCompany.innerHTML = optionsData;
        selectEditCompany.innerHTML = optionsData;
        $.LoadingOverlay("hide");
    } catch (ERR) {
        $.LoadingOverlay("hide");
        mensagem("Algo deu errado!", 2)
    }

}

const atualizaDadosEmpresa = async () => { /* Atualiza número do HUD da empresa e faz Request pra rota de empresas*/
    try {
        carregamento("mostrar");
        const response = await fetch(`${url}/v1/Company`);
        let contador = 0;
        const dados = await response.json();
        const tabela = document.getElementsByTagName("tbody")[0];
        let data = '';
        for (ct of dados) {
            data += `
        <tr class="col columns">
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
                     <a class="editLink" onclick="carregarPara('editCompany.html?id=${ct.empresaId}')">Editar</a>
                    </th>

                </tr>
    `;        }
        tabela.innerHTML = data;
        $("#tableRegisters").DataTable({
            "search": {
                "smart": true,
                "regex": true
            },
            language: {
                "searchPlaceholder": "Pesquise por CNPJ, Nome da Empresa, etc...",
                "search": "Search:",
                "info": "Exibindo _START_ até _END_ de _TOTAL_ Linhas",
                "zeroRecords": "Nada encontrado ;-;",
                "emptyTable": "Nenhum registro encontrado - Cadastre um!"
            },
            "data": dados,
            "columns": [
                { "data": "empresaId" },
                { "data": "name" },
                { "data": "document" },
                { "data": "uf" },
                {
                    "data": "empresaId",
                    "render": function (data, type, row) {
                        return `<a class="editLink" onclick="carregarPara('editCompany.html?id=${data}')">Editar</a>`
                    }
                }

            ]
        })
        carregamento("esconder");
        const fornCadastradas = document.getElementsByClassName("span-num")[0];
        fornCadastradas.textContent = dados.length;
    } catch (err) {
        carregamento("esconder");
        mensagem("Ocorreu um erro! Recarregue a página!", 2, 2000)
    }
}

const empresaDados = async () => {
    try {
        const params = new URLSearchParams(document.location.search);
        const id = params.get("id");
        const response = await fetch(`${url}/v1/GetCompanyById/${id}`);
        const dados = await response.json();
        $("#codEmp").val(dados.empresaId);
        $("#empresaNome").val(dados.name);
        $("#cnpjEmp").val(dados.document);
        $("#estadosVal").val(dados.uf);

    } catch (err) {
        mensagem(err, 2)
    }

}

const carregarPara = (pagina, time = 250) => {
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