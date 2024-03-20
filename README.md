# GestaoProdutosCrud

## Tecnologias e estruturas utilizadas
* .Net 5
* Estrutura em camadas com aplicação DDD(Domain-Driven Design)
* Banco de dados SQL Server local
* Dapper
* AutoMapper
* Teste Unitario XUnit

## Arquitetura proposta de camadas com aplicação DDD(Domain-Driven Design)

![image](https://github.com/mateus2810/GestaoProdutosCrud/assets/20459665/f446a1f6-8a69-480d-a6d6-6d3fc8c251e2)

## Modelo de Banco de dados, estrutura

Criando tabelas de uso do sistema
```
-- Criando a tabela Fornecedor
CREATE TABLE Fornecedor (
    Id INT AUTO_INCREMENT PRIMARY KEY, -- ID autoincremental
    Codigo VARCHAR(50), -- Código do fornecedor
    Descricao TEXT, -- Descrição do fornecedor (TEXT é equivalente ao NVARCHAR(MAX))
    CNPJ VARCHAR(14), -- CNPJ do fornecedor
    Nome TEXT -- Nome do fornecedor (TEXT é equivalente ao NVARCHAR(MAX))
);

-- Criando a tabela Produto
CREATE TABLE Produto (
    Id INT AUTO_INCREMENT PRIMARY KEY, -- ID autoincremental
    Codigo VARCHAR(50), -- Código sequencial e não nulo
    Descricao TEXT NOT NULL, -- Descrição não nula (TEXT é equivalente ao NVARCHAR(MAX))
    Situacao TINYINT(1), -- Situação booleana (usando TINYINT com tamanho 1)
    DataFabricacao DATE, -- Data de fabricação
    DataValidade DATE, -- Data de validade
    FornecedorId INT, -- Chave estrangeira para o Fornecedor
    FOREIGN KEY (FornecedorId) REFERENCES Fornecedor(Id) -- Definindo a chave estrangeira
);
``` 

#### Diagrama de classe criado
![image](https://github.com/mateus2810/GestaoProdutosCrud/assets/20459665/c4a93269-2673-407e-9351-8cf916f500f2)



### Tarega realizada:

![image](https://github.com/mateus2810/GestaoProdutosCrud/assets/20459665/f23418ae-65a1-49c9-a708-dc850541d45b)




