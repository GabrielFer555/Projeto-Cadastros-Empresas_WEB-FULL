const notyf = new Notyf();


const fornCadastradas = document.getElementsByClassName("span-num")[0];
const colunas = document.getElementsByClassName("columns");
fornCadastradas.textContent = colunas.length.toString();
const textNumb =  document.getElementsByClassName("nullTable")[0];
if(colunas.length > 0){
    textNumb.setAttribute("class","d-none");
}

function mensagem(msg,type=1, duration = 1000, x ="left", y ="bottom"){
    const i = new Notyf({
        duration:duration,
        position:{
            x:x,
            y:y
        },
    });
switch(type){
    case 1:
        i.success(msg);
        break;
    case 2:
        i.error(msg);
        break;
}}