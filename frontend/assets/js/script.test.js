var url = "http://localhost:5143";

describe("Testando rota de Empresa", () =>{
    test("Cadastro", async () =>{
        const response = await fetch(`${url}/v1/Company`,{
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                name:"Empresa Teste",
                document:"111.111.111/0001-80",
                uf: "PR"
            })
        })
        expect(response.status).toBe(201);
        })
    })
