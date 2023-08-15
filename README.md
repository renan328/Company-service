# Company-service

Teste técnico full stack. Backend realizado em ASP.NET 6, Frontend em Angular e banco de dados em MySQL.
## Instruções de Instalação

Para a configuração do ambiente é separada em três partes, e execução do script do banco de dados, a execução da API e a execução da aplicação Angular.

### Banco de dados
Execute por completo o script na ID.

### API
Execute a aplicação utilizando a IDE Visual Studio 2022;
Ou pelo command prompt na pasta da aplicação.

```
dotnet run
```
Assim que compilar por completo, o swagger estará disponível para acesso no navegador em uma url parecida com a seguinte:
```
https://localhost:7287/swagger/index.html
```

### Angular
Na pasta do projeto, execute o seguinte comando para instalar todas as dependências.

```
npm install
```
Em seguida, execute o comando para iniciar a aplicação.
```
ng serve
```
A aplicação estará disponível em uma url como essa:

```
https://localhost:4200
```

## Exemplo de uso
Com todo o ambiente configurado, acesse a página de cadastro de empresas e cadastre uma, com os dados preenchidos corretamente, como nome, CNPJ e endereço. Ao cadastrar, já é possível visualizar a empresa na tela principal e 
assim, ver os detalhes dela, e fazer outras operações como editar e excluir.

## Endpoints

A aplicação possui 5 endpoints. Um para a busca de todas as empresas, um para listar uma em específico, um para cadastro, um para edição e um para remoção de empresa.

Assim, completando os verbos HTTP para a formação de um CRUD.
